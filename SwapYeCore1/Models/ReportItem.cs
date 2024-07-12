namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ReportItem
    {
        [Key]
        public int ReportItemId { get; set; }
        public int UserID { get; set; }
        [Required]
        public string Description_1 { get; set; }
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual User User { get; set; }
    }
}
