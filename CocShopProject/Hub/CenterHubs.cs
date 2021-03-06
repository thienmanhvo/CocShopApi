﻿using CocShop.Core.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace CocShop.WebAPi.Hubs
{
    public class CenterHub : Hub
    {
        private IHttpContextAccessor _contextAccessor;
        private readonly UserManager<MyUser> _userManager;
        //private readonly IHubUserConnectionService _hubService;
        private HttpContext _context { get { return _contextAccessor.HttpContext; } }
    }
}
