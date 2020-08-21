using MobileStore.DatabaseFiles;
using MobileStore.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using PagedList.Mvc;

namespace MobileStore.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        dbMobileStoreEntities context = new dbMobileStoreEntities();
        UnitOfWork unit = new UnitOfWork();
        public IPagedList<Tbl_Product> Products { get; set; }
       public  HomeIndexViewModel CreateModel(string search,int pageSize,int? page)
        {

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search",search??(object)DBNull.Value)
            };
            IPagedList<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1,pageSize);


            return new HomeIndexViewModel()
            {
                Products = data
                //Products = unit.GetRepositoryInstance<Tbl_Product>().GetAllRecords()
            };
                
            
        }
    }
}