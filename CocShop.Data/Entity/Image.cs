using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Data.Entity
{
    [Table("Image")]
    public class Image : BaseEntity
    {

        [ForeignKey("Path")]
        public string Path { get; set; }
        [ForeignKey("Product")]
        [Column("Product_Id")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
