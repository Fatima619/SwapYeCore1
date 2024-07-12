using PagedList.Core;
using SwapYeCore1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwapYeCore1.ViewModels
{
    public class UserItem
    {
        public IPagedList<Item> items { get; set; }
        public  User user { get; set; }
    }
}