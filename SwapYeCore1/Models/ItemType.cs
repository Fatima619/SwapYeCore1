namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public partial class ItemType
    {
        [Key]
        public int ItemTypeId { get; set; }
        [AllowNull]
        public string? Describtion { get; set; }
        [Required]
        public string TypeNme { get; set; }

        public virtual ICollection<Item> Items { get; set; }


    }
}
