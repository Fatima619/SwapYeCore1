using SwapYeCore1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwapYeCore1.ViewModels
{
    public class Report
    {
        public List<ReportItem> reitem { get; set; }
        public List<ReportComment> recomment { get; set; }
    }
}