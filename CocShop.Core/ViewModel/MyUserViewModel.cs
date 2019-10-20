using CocShop.Core.Attribute;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class MyUserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string AvatarPath { get; set; }
        public MyEnum.Gender Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<PaymentMethodViewModel> PaymentMethods { get; set; }
        public ICollection<string> RoleName { get; set; }
    }
    public class UpdateMyUserRequestViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        [CheckUrl]
        public string AvatarPath { get; set; }
        public MyEnum.Gender Gender { get; set; }
        [CheckDate]
        public string Birthday { get; set; }
    }   
}
