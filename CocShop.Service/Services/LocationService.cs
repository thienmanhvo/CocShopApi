using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Query;
using CocShop.Core.Data.Repository;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BaseViewModel<LocationViewModel> CreateLocation(CreateLocationRequestViewModel location)
        {
            var entity = _mapper.Map<Location>(location);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            _repository.Add(entity);

            var result = new BaseViewModel<LocationViewModel>()
            {
                Data = _mapper.Map<LocationViewModel>(entity),
            };

            Save();

            return result;
        }

        public BaseViewModel<string> DeleteLocation(Guid id)
        {
            //Find Location
            var location = _repository.GetById(id);
            //result to return
            BaseViewModel<string> result = null;
            //check Location exist
            if (location == null || location.IsDelete)
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
                location.IsDelete = true;
                _repository.Update(location);
                result = new BaseViewModel<string>();
                //save change
                Save();
            }
            return result;
        }

        public BaseViewModel<LocationViewModel> GetLocation(Guid id)
        {
            var location = _repository.GetById(id);

            if (location == null || location.IsDelete)
            {
                return new BaseViewModel<LocationViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            return new BaseViewModel<LocationViewModel>
            {
                Data = _mapper.Map<LocationViewModel>(location),
            };
        }

        public async Task<BaseViewModel<PagingResult<LocationViewModel>>> GetAllLocations(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<LocationViewModel>>();

            string filter = SearchHelper<Location>.GenerateStringExpression(request.Filter, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);

            Expression<Func<Location, bool>> FilterExpression = await LinqHelper<Location>.StringToExpression(filter);

            QueryArgs<Location> queryArgs = new QueryArgs<Location>
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
                result.Data = new PagingResult<LocationViewModel>
                {
                    Results = _mapper.Map<IEnumerable<LocationViewModel>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                    TotalRecords = _repository.Count(queryArgs.Filter)
                };
            }

            return result;
        }
        public async Task<BaseViewModel<PagingResult<LocationViewModel>>> GetAllLoctionsNoPaging(BaseRequestViewModel request)
        {
            var result = new BaseViewModel<PagingResult<LocationViewModel>>();

            string filter = SearchHelper<Location>.GenerateStringExpression(request.Filter, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);

            Expression<Func<Location, bool>> FilterExpression = await LinqHelper<Location>.StringToExpression(filter);

            QueryArgs<Location> queryArgs = new QueryArgs<Location>
            {
                Filter = FilterExpression,
                Sort = request.SortBy,
            };


            var data = _repository.Get(queryArgs.Filter, queryArgs.Sort).ToList();

            //var sql = data.ToSql();

            if (data == null || data.Count == 0)
            {
                result.Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD);
                result.Code = MessageConstants.NO_RECORD;
            }
            else
            {

                result.Data = new PagingResult<LocationViewModel>
                {
                    Results = _mapper.Map<IEnumerable<LocationViewModel>>(data),
                    TotalRecords = _repository.Count(queryArgs.Filter)
                };
            }

            return result;
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public BaseViewModel<LocationViewModel> UpdateLocation(UpdateLocationRequestViewModel location)
        {
            var entity = _repository.GetById(location.Id);
            if (entity == null || entity.IsDelete)
            {
                return new BaseViewModel<LocationViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            entity = _mapper.Map(location, entity);

            entity.SetDefaultUpdateValue(_repository.GetUsername());
            _repository.Update(entity);
            var result = new BaseViewModel<LocationViewModel>
            {
                Data = _mapper.Map<LocationViewModel>(entity),
            };

            Save();

            return result;
        }
    }
}

