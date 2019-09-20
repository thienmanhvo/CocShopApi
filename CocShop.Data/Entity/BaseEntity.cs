using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Data.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Created_By")]
        public string CreatedBy { get; set; }

        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }

        [Column("Updated_By")]
        public string UpdatedBy { get; set; }

        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }

        public virtual void SetDefaultInsertValue(string username)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            CreatedBy = username;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = username;
        }

        public virtual void SetDefaultUpdateValue(string username)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = username;
        }
    }
}
