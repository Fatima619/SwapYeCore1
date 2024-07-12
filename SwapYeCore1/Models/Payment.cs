namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public string File { get; set; }
        [Required]
        public string ApprovalState { get; set; }
        public int ItemID { get; set; }

        public virtual Item Item { get; set; }

    }
}
