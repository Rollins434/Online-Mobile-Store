using MobileStore.DatabaseFiles;
using MobileStore.Models.ViewModels;
using MobileStore.Repository;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Controllers
{
    
    public class HomeController : Controller
    {
        dbMobileStoreEntities context = new dbMobileStoreEntities();

        
        public ActionResult Index(string search, int ? page)
        {
            HomeIndexViewModel Home = new HomeIndexViewModel();

            return View(Home.CreateModel(search,4,page));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CheckOut()
        { 
            return View();
        }
        public ActionResult CheckOutDetails()
        {
            return View();
        }

        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = context.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("CheckOut");
        }



      
        public ActionResult AddToCart(int productId, String url)
        {
            if (Session["cart"] == null)
            {
                //url = "Checkout";

                List<Item> cart = new List<Item>();
                var product = context.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];


                var product = context.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        cart.Remove(item);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = 1
                        });
                    }
                }

                Session["cart"] = cart;
            }

            return RedirectToAction("Index");
            //return Redirect(url);


        }

    

    }
    }
