using MobileStore.DatabaseFiles;
using MobileStore.Models;
using MobileStore.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MobileStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }


        public ActionResult CategoryEdit(int categoryId)
        {
            ViewBag.Category = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(categoryId));

        }

        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Categories");
        }

        public ActionResult AddCategory()
        {
            //return UpdateCategory();
            return RedirectToAction("UpdateCategory");
        }

       [HttpPost]
        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;

            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }



            return View("UpdateCategory",cd);
        }

        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.Category = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(productId));

        }

        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), pic);
                file.SaveAs(path);
            }
            tbl.ProductImage = file!=null? pic :tbl.ProductImage;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Product");
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Category = GetCategory();
            return View();
        }

        [HttpPost]
        
        public ActionResult AddProduct(Tbl_Product tbl,HttpPostedFileBase file)
        {
            string pic = null;
            if (file!=null)
            {
                 pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"),pic);
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(tbl);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Product",tbl);

        }
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach( var i in cat)
            {
                list.Add(new SelectListItem { Value = i.CategoryId.ToString(), Text = i.CategoryName });
            }
            return (list);
        }


    }
}