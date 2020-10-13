using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using uyumsoft_ticaret_app.App_Classes;
using uyumsoft_ticaret_app.Models;

namespace uyumsoft_ticaret_app.Controllers
{
    public class HomeController : Controller
    {
        uyumticaret2 utc = new uyumticaret2();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Basket()
        {
            return PartialView();
        }

        public PartialViewResult Slider()
        {
            var data = utc.Pictures.Where(x => x.BigImg.Contains("Slider")).ToList();


            return PartialView(data);
        }

        public PartialViewResult NewProducts()
        {
            var data = utc.Products.ToList();
            return PartialView(data);
        }

        public PartialViewResult Services()
        {
            return PartialView();
        }


        public PartialViewResult Brands()
        {
            var data = utc.Brands.ToList();
            return PartialView(data);
        }

        public void AddToCart(int id)
        {

            BasketItem b = new BasketItem();
            Product p = utc.Products.FirstOrDefault(x => x.id == id);

            b.Product = p;
            b.Quantity = 1;
            b.Discount = 0;

            Basket k = new Basket();
            k.AddToBasket(b);

        }

        public void CartProductDropOne(int id)
        {
            if(HttpContext.Session["ActiveBasket"] != null)
            {
                Basket b = (Basket)HttpContext.Session["ActiveBasket"];

                if(b.ProductsList.FirstOrDefault(x=>x.Product.id == id).Quantity > 1)
                {
                    b.ProductsList.FirstOrDefault(x => x.Product.id == id).Quantity--;
                }
                else
                {
                    BasketItem c = b.ProductsList.FirstOrDefault(x => x.Product.id == id);
                    b.ProductsList.Remove(c);
                }
            }
        }

        public PartialViewResult MiniCartWidget()
        {
            if(HttpContext.Session["ActiveBasket"] != null)
            {
                return PartialView((Basket)HttpContext.Session["ActiveBasket"]);
            }
            else
            {
                return PartialView();
            }
        }

        public ActionResult ProductDetail(string id)
        {
            Product product = utc.Products.FirstOrDefault(x => x.ProductName == id);

            List<Product_Feature> pf = utc.Product_Feature.Where(x => x.ProductID == product.id).ToList();

            Dictionary<string, List<FeatureDetail>> value = new Dictionary<string, List<FeatureDetail>>();

            List<FeatureDetail> fd = new List<FeatureDetail>();

            foreach(Product_Feature p in pf)
            {
                FeatureType featureType = utc.FeatureTypes.FirstOrDefault(x => x.id == p.FeatureTypeID);

                foreach(FeatureDetail detail in featureType.FeatureDetails) {
                    FeatureDetail featureDetail = utc.FeatureDetails.FirstOrDefault(x => x.FeatureTypeID == featureType.id && x.id == p.FeatureDetailID);
                    if(!fd.Any(x => x.id == featureDetail.id))
                    {
                        fd.Add(featureDetail);
                    } 
                    
                }
                value.Add(featureType.FeatureTypeName, fd);
                fd = new List<FeatureDetail>();
            }

            ViewBag.Features  = value;

            return View(product);
        }
       

        public ActionResult SalesmanProfile(int ID)
        {
            UserRecord usr = utc.UserRecords.FirstOrDefault(x => x.id == ID);
            return View(usr);
        }

        public ActionResult Category()
        {
            List<Category> data = utc.Categories.ToList();
            return View(data);
        }

        public ActionResult ProductsByCategory(int id)
        {
            List<Product> data = utc.Products.Where(x => x.CategoryID == id).ToList();
            return View(data);
        }

        public ActionResult ProductsByBrand(int id)
        {
            List<Product> data = utc.Products.Where(x => x.BrandID == id).ToList();
            return View(data);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult CartCheckout()
        {
            if (HttpContext.Session["ActiveBasket"] != null)
            {
                Basket basket = (Basket)HttpContext.Session["ActiveBasket"];
                return View(basket);
            }
            return View();
            
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Payment()
        {
            if (HttpContext.Session["ActiveBasket"] != null)
            {
                Basket basket = (Basket)HttpContext.Session["ActiveBasket"];
                foreach(BasketItem item in basket.ProductsList)
                {
                    utc.Products.FirstOrDefault(x => x.id == item.Product.id).Stock = 
                        utc.Products.FirstOrDefault(x => x.id == item.Product.id).Stock - item.Quantity;
                    utc.SaveChanges();
                }       
               basket.ProductsList.Clear();              
            }
            return RedirectToAction("Index");          
        }
        
    }
}