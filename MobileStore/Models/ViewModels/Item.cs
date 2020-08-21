using MobileStore.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileStore.Models.ViewModels
{
    public class Item
    {
        public Tbl_Product Product { get; set; }
        public int Quantity { get; set; }
    }
}