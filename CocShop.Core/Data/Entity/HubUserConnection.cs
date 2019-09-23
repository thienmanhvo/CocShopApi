using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Data.Entity
{
    [Table("Hub_User_Connection")]
    public class HubUserConnection : BaseEntity
    {

        [Column("User_Id")]
        public Guid? UserId { get; set; }
        [Column("Connection")]
        public string Connection { get; set; }
        [Column("Username")]
        public string Username { get; set; }

        [ForeignKey("UserId")]
        public virtual MyUser User { get; set; }
    }
}
