using CocShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Core.Entity
{
    [Table("Hub_User_Connection")]
    public class HubUserConnection
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Column("User_Id")]
        public string UserId { get; set; }
        [Column("Connection")]
        public string Connection { get; set; }
        [Column("Username")]
        public string Username { get; set; }

        [ForeignKey("UserId")]
        public virtual MyUser User { get; set; }
    }
}
