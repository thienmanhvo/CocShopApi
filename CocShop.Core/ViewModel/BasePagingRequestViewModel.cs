﻿using CocShop.Core.Constaint;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CocShop.Core.ViewModel
{
    public class BasePagingRequestViewModel
    {
        //public Paging Paging { get; set; }
        //public string SortField { get; set; }
        public string SortBy { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? PageSize { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? PageIndex { get; set; }
        public string SearchField { get; set; }
        public string SearchValue { get; set; }
        public IDictionary<string, string> SearchRange { get; set; }
        public void SetDefaultPage()
        {
            PageSize = PageSize ?? Constants.DEFAULT_PAGE_SIZE;
            PageIndex = PageIndex ?? Constants.DEFAULT_PAGE_INDEX;
            PageSize = PageSize > Constants.MAX_PAGE_SIZE ? Constants.MAX_PAGE_SIZE : PageSize;
        }

    }
    public class Paging
    {

    }
}
