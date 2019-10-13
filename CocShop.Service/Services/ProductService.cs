using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using CocShop.Core.Data.Entity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection;
using CocShop.Core.Data.Query;
using System.ComponentModel;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;
using CocShop.Core.Extentions;
using CocShop.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CocShop.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }

        public BaseViewModel<ProductViewModel> CreateProduct(CreateProductRequestViewModel Product)
        {
            var entity = _mapper.Map<Product>(Product);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            _repository.Add(entity);

            var result = new BaseViewModel<ProductViewModel>()
            {
                Data = _mapper.Map<ProductViewModel>(entity),
            };

            Save();

            return result;
        }

        public BaseViewModel<string> DeleteProduct(Guid id)
        {
            //Find product
            var product = _repository.GetById(id);
            //result to return
            BaseViewModel<string> result = null;
            //check product exist
            if (product == null || product.IsDelete)
            {
                result = new BaseViewModel<string>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = ErrMessageConstants.NOTFOUND,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND)
                };
            }
            else
            {
                //update column isDelete = true
                product.IsDelete = true;
                _repository.Update(product);
                result = new BaseViewModel<string>();
                //save change
                Save();
            }
            return result;
        }

        public BaseViewModel<ProductViewModel> GetProduct(Guid id)
        {
            var product = _repository.GetById(id);

            if (product == null || product.IsDelete)
            {
                return new BaseViewModel<ProductViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            return new BaseViewModel<ProductViewModel>
            {
                Data = _mapper.Map<ProductViewModel>(product),
            };
        }
        //private Func<Product, bool> GetDynamicQueryWithExpresionTrees(string propertyName, string val)
        //{
        //    var param = Expression.Parameter(typeof(Product), "x");

        //    #region Convert to specific data type
        //    MemberExpression member = Expression.Property(param, propertyName);
        //    UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);
        //    #endregion

        //    Expression body = Expression.Equal(member, valueExpression);
        //    var final = Expression.Lambda<Func<Product, bool>>(body: body, parameters: param);
        //    return final.Compile();
        //}

        //private  UnaryExpression GetValueExpression(string propertyName, string val, ParameterExpression param)
        //{
        //    var member = Expression.Property(param, propertyName);
        //    var propertyType = ((PropertyInfo)member.Member).PropertyType;
        //    var converter = TypeDescriptor.GetConverter(propertyType);

        //    if (!converter.CanConvertFrom(typeof(string)))
        //        throw new NotSupportedException();

        //    var propertyValue = converter.ConvertFromInvariantString(val);
        //    var constant = Expression.Constant(propertyValue);
        //    return Expression.Convert(constant, propertyType);
        //}

        public async Task<BaseViewModel<PagingResult<ProductViewModel>>> GetAllProducts(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<ProductViewModel>>();

            string filter = SearchHelper<Product>.GenerateStringExpression(request.Filter, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);

            Expression<Func<Product, bool>> FilterExpression = await LinqHelper<Product>.StringToExpression(filter);

            QueryArgs<Product> queryArgs = new QueryArgs<Product>
            {
                Offset = pageSize * (pageIndex - 1),
                Limit = pageSize,
                Filter = FilterExpression,
                Sort = request.SortBy,
            };


            var data = _repository.Get(queryArgs.Filter, queryArgs.Sort, queryArgs.Offset, queryArgs.Limit).ToList();

            //var sql = data.ToSql();

            if (data == null || data.Count == 0)
            {
                result.Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD);
                result.Code = MessageConstants.NO_RECORD;
            }
            else
            {
                var pageSizeReturn = pageSize;
                if (data.Count < pageSize)
                {
                    pageSizeReturn = data.Count;
                }
                result.Data = new PagingResult<ProductViewModel>
                {
                    Results = _mapper.Map<IEnumerable<ProductViewModel>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                    TotalRecords = _repository.Count(queryArgs.Filter)
                };
            }

            return result;
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public BaseViewModel<ProductViewModel> UpdateProduct(Guid id, UpdateProductRequestViewModel product)
        {
            var entity = _repository.GetById(id);
            if (entity == null || entity.IsDelete)
            {
                return new BaseViewModel<ProductViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            entity = _mapper.Map(product, entity);
            entity.SetDefaultUpdateValue(_repository.GetUsername());
            _repository.Update(entity);
            var result = new BaseViewModel<ProductViewModel>
            {
                Data = _mapper.Map<ProductViewModel>(entity),
            };

            Save();

            return result;
        }

        public BaseViewModel<IEnumerable<ProductViewModel>> GetProductByCategoryID(Guid cateId)
        {
            var cate = _productCategoryRepository.GetById(cateId);
            if (cate == null || cate.IsDelete)
            {
                return new BaseViewModel<IEnumerable<ProductViewModel>>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }
            var listProduct = _repository.GetMany(_ => _.IsDelete == false && _.CateId == cateId);
            if (listProduct == null || !listProduct.Any())
            {
                return new BaseViewModel<IEnumerable<ProductViewModel>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD),
                    Code = MessageConstants.NO_RECORD
                };
            }
            return new BaseViewModel<IEnumerable<ProductViewModel>>(_mapper.Map<IEnumerable<ProductViewModel>>(listProduct));
        }
    }
}
