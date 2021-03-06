﻿using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Query;
using CocShop.Core.Data.Repository;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.Service.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Service.Services
{
    public class MyUserService : IMyUserService
    {
        private readonly IMyUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<MyUser> _userManager;

        public MyUserService(IMyUserRepository repository, IUnitOfWork unitOfWork, IMapper mapper, UserManager<MyUser> userManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }


        public BaseViewModel<MyUserViewModel> GetMyUser(Guid id, string include = null)
        {
            var includeList = IncludeLinqHelper<MyUser>.StringToListInclude(include);

            var myUser = _repository.Get(_ => _.IsDelete == false && _.Id == id, includeList).FirstOrDefault();

            if (myUser == null)
            {
                return new BaseViewModel<MyUserViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }
            var role = _userManager.GetRolesAsync(myUser).Result;
            var data = _mapper.Map<MyUserViewModel>(myUser);
            data.RoleName = role;
            return new BaseViewModel<MyUserViewModel>
            {
                Data = data,
            };
        }

        public async Task<BaseViewModel<PagingResult<MyUserViewModel>>> GetAllMyUsers(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<MyUserViewModel>>();
            var currentId = _repository.GetCurrentUserId();
            string filter = SearchHelper<MyUser>.GenerateStringExpression(request.Filter, $"{Constants.DEAFAULT_DELETE_STATUS_EXPRESSION} && _.Id != new System.Guid(\"{currentId}\")");

            Expression<Func<MyUser, bool>> FilterExpression = await LinqHelper<MyUser>.StringToExpression(filter);

            QueryArgs<MyUser> queryArgs = new QueryArgs<MyUser>
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
                result.Data = new PagingResult<MyUserViewModel>
                {
                    Results = _mapper.Map<IEnumerable<MyUserViewModel>>(data),
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

    }
}
