using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CocShop.Core.Data.Entity;
using AutoMapper;
using CocShop.Core.Service;
using Microsoft.Extensions.DependencyInjection;
using CocShop.Core.ViewModel;
using System.Net;
using CocShop.Core.Constaint;
using CocShop.Core.MessageHandler;
using CocShop.WebAPi.Extentions;
using Microsoft.AspNetCore.Authorization;
using CocShop.Core.Attribute;

namespace CocShop.WebAPi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILocationService _locationService;

        #endregion


        public LocationsController(IServiceProvider serviceProvider)
        {
            _locationService = serviceProvider.GetRequiredService<ILocationService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<LocationViewModel>>>> GetLocation([FromQuery]BasePagingRequestViewModel request)
        {
            request.SetDefaultPage();
            //var a = request.Filters[0];

            var result = await _locationService.GetAllLocations(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<LocationViewModel>> GetLocation([CheckGuid(Property = "LocationId")]string id)
        {
            var result = _locationService.GetLocation(new Guid(id));

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }
        // GET: api/Product
        [Authorize(Roles = Role.Admin)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<LocationViewModel>>>> GetAllLocation([FromQuery]BaseRequestViewModel request)
        {
            //var a = request.Filters[0];

            var result = await _locationService.GetAllLoctionsNoPaging(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }
        // PUT: api/Locations/5
        [ValidateModel]
        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public ActionResult<BaseViewModel<LocationViewModel>> PutLocation([CheckGuid(Property = "LocationId")]string id, [FromBody]UpdateLocationRequestViewModel location)
        {
            var guidId = new Guid(id);
            location.Id = guidId;
            var result = _locationService.UpdateLocation(location);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }

        // POST: api/Locations
        [ValidateModel]
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public ActionResult<BaseViewModel<LocationViewModel>> PostLocation(CreateLocationRequestViewModel location)
        {
            var result = _locationService.CreateLocation(location);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // DELETE: api/Locations/5
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public ActionResult<BaseViewModel<string>> DeleteLocation([CheckGuid(Property = "LocationId")] string id)
        {
            var result = _locationService.DeleteLocation(new Guid(id));

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        //private bool LocationExists(Guid id)
        //{
        //    return _context.Location.Any(e => e.Id == id);
        //}
    }
}
