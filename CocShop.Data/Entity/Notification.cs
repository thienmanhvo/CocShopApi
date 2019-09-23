﻿using CocShop.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Data.Entity
{
    [Table("Notification")]
    public class Notification : BaseEntity
    {

        public String Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string NData { get; set; }
        public bool IsSeen { get; set; }
        public bool IsTouch { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual MyUser User { get; set; }
    }
}
