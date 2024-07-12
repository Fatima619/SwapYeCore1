namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public partial class User
    {

        [Key]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [AllowNull]
        public string? Gender { get; set; }
        [AllowNull]
        public string? City { get; set; }
        [AllowNull]
        public string? street { get; set; }
        [AllowNull]
        public string? Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        [Required]
        public string Passwords { get; set; }
        public int UserTypeId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ReportComment> ReportComments { get; set; }
        public virtual ICollection<ReportItem> ReportItems { get; set; }
        public virtual UserType UserType { get; set; }

    }
}
