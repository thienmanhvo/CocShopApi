using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Entity
{
    [Table("Image")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("Path")]
        public string Path { get; set; }
        [ForeignKey("Product")]
        [Column("Product_Id")]
        public string ProductId { get; set; }
        [Column("Created_By")]
        public string CreatedBy { get; set; }
        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }
        [Column("Updated_By")]
        public string UpdatedBy { get; set; }
        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Product Product { get; set; }
    }
}
