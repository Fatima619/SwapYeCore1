namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public partial class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        [AllowNull]
        public string? Description { get; set; }
        [Required]
        public string TypeName { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
