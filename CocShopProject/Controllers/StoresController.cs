﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CocShop.WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {

        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IStoreService _storeService;

        #endregion


        public StoresController(IServiceProvider serviceProvider)
        {
            _storeService = serviceProvider.GetRequiredService<IStoreService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/stores
        [Route("GetNearest")]
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<StoreViewModel>>>> Getstore([FromQuery]GetNearestStoreRequestViewmovel request)
        {
            request.SetDefaultPage();
            //var a = request.Filters[0];

            var result = await _storeService.GetAllNearestStore(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<StoreViewModel>>>> GetAllStore([FromQuery]BasePagingRequestViewModel request)
        {
            request.SetDefaultPage();
            //var a = request.Filters[0];

            var result = await _storeService.GetAllStores(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }


    }
}