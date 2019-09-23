using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Entity
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
