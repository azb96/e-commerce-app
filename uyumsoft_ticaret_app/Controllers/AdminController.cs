using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uyumsoft_ticaret_app.Models;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using uyumsoft_ticaret_app.ViewModel;
using System.Text;
using uyumsoft_ticaret_app.App_Classes;

namespace uyumsoft_ticaret_app.Controllers
{
    
    public class AdminController : Controller
    {
        uyumticaret2 utc = new uyumticaret2();

        // GET: Admin
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult Index()
        {
            return View();
        }

        //ürün sayfası
        [Authorize(Roles = "Admin")]
        public ActionResult Product()
        {
            var model = new ProductViewModel()
            {
                ProductList = utc.Products.ToList()
            };

            return View(model);
        }

        //ürün ekle
           
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult AddProduct()
       {
            var model = new ProductViewModel()
            {
                CategoryList = utc.Categories.ToList(),
                BrandList = utc.Brands.ToList(),
                SalesmanList = utc.UserRecords.Where(x=>x.RoleID == 2).ToList()
            };

                return View(model);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult AddProduct(Product product)
        {
            utc.Products.Add(product);
            utc.SaveChanges();
            return RedirectToAction("Product");
        }


        //kategori ekranı
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult Category()
        {
            List<Category> categories = utc.Categories.ToList();
            return View(categories);
        }


        //kategori ekle
        [Authorize(Roles = "Admin")]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCategory(Category category, HttpPostedFileBase fileUpload)
        {
            int pictureID = -1;

            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                int width = Convert.ToInt32(ConfigurationManager.AppSettings["BrandPictureWidth"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["BrandPictureHeight"].ToString());

                string name = Convert.ToString("/Content/BrandRelated/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName));

                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath(name));

                Picture pic = new Picture();
                pic.MiddleImg = name;
                utc.Pictures.Add(pic);
                utc.SaveChanges();
                if (pic.id != null)
                {
                    pictureID = pic.id;
                }
            }
            if (pictureID != -1)
            {
                category.PictureID = pictureID;
            }
            utc.Categories.Add(category);
            utc.SaveChanges();
            return RedirectToAction("Category");
        }



        //markalar
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult Brand()
        {
            List<Brand> brands = utc.Brands.ToList();
            return View(brands);
        }
        //marka ekle 
        [Authorize(Roles = "Admin")]
        public ActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddBrand(Brand brnd, HttpPostedFileBase fileUpload)
        {
            int pictureID = -1;

            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                int width = Convert.ToInt32(ConfigurationManager.AppSettings["BrandPictureWidth"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["BrandPictureHeight"].ToString());

                string name = Convert.ToString("/Content/BrandRelated/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName));

                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath(name));

                Picture pic = new Picture();
                pic.MiddleImg = name;
                utc.Pictures.Add(pic);
                utc.SaveChanges();
                if (pic.id != null)
                {
                    pictureID = pic.id;
                }
            }
            if (pictureID != -1)
            {
                brnd.PictureID = pictureID;
            }

            utc.Brands.Add(brnd);
            utc.SaveChanges();


            return RedirectToAction("Brand");
        }


        [Authorize(Roles = "Admin, Salesman")]
        //ürün resim ekle
        public ActionResult AddProductPicture(int id)
        {
            
            return View(id);
        }

        [Authorize(Roles = "Admin, Salesman")]
        [HttpPost]
        public ActionResult AddProductPicture(int productID, HttpPostedFileBase fileUpload)
        {
            if(fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                Bitmap midpic = new Bitmap(img, Settings.ProductMiddleSize);

                Bitmap bigpic = new Bitmap(img, Settings.ProductBigSize);

                string midPath = "/Content/ProductPicture/Medium/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                string bigPath = "/Content/ProductPicture/Big/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                midpic.Save(Server.MapPath(midPath));

                bigpic.Save(Server.MapPath(bigPath));

                Picture pic = new Picture();
                pic.BigImg = bigPath;
                pic.MiddleImg = midPath;
                pic.ProductID = productID;
                if(utc.Pictures.FirstOrDefault(x=>x.ProductID == productID && x.isThere == false) != null)
                {
                    pic.isThere = true;
                }
                else
                {
                    pic.isThere = false;
                }

                utc.Pictures.Add(pic);
                utc.SaveChanges();
                return View(productID);

            }

            return View(productID);

        }



        //özellik tipleri
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult FeatureType()
        {
            List<FeatureType> featureTypes = utc.FeatureTypes.ToList();
            return View(featureTypes);
        }
        //özellik tipi ekle 
        [Authorize(Roles = "Admin")]
        public ActionResult AddFeatureType()
        {
            List<Category> categories = utc.Categories.ToList();
            return View(categories);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddFeatureType(FeatureType featureType)
        {
            utc.FeatureTypes.Add(featureType);
            utc.SaveChanges();
            return RedirectToAction("FeatureType");
        }

        //özellik detayı
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult FeatureDetail()
        {
            List<FeatureDetail> featureDetails = utc.FeatureDetails.ToList();
            return View(featureDetails);
        }

        //özellik detayı ekle 
        [Authorize(Roles = "Admin")]
        public ActionResult AddFeatureDetail()
        {
            List<FeatureType> featureTypes = utc.FeatureTypes.ToList();
            return View(featureTypes);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddFeatureDetail(FeatureDetail featureDetail)
        {
            utc.FeatureDetails.Add(featureDetail);
            utc.SaveChanges();
            return RedirectToAction("FeatureDetail");
        }
        //ürün özelliği ekle
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult ProductFeature()
        {
            List<Product_Feature> feature = utc.Product_Feature.ToList();
            return View(feature);
        }

        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult AddProductFeature()
        {
            List<Product> products = utc.Products.ToList();
            return View(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult AddProductFeature(Product_Feature pf)
        {
            utc.Product_Feature.Add(pf);
            utc.SaveChanges();
            return RedirectToAction("ProductFeature");
        }

        public PartialViewResult ProductFeatureTypeWidget(int? catID)
        {
            if(catID != null)
            {
                var data = utc.FeatureTypes.Where(x => x.CategoryID == catID).ToList();
                return PartialView(data);
            }
            else
            {
                var data = utc.FeatureTypes.ToList();
                return PartialView(data);
            }
        }

        public PartialViewResult ProductFeatureDetailWidget(int? typeID)
        {
            if (typeID != null)
            {
                var data = utc.FeatureDetails.Where(x => x.FeatureTypeID == typeID).ToList();
                return PartialView(data);
            }
            else
            {
                var data = utc.FeatureDetails.ToList();
                return PartialView(data);
            }
        }

        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult DeleteProductFeature(int productID, int typeID, int featureID)
        {
            Product_Feature pf = utc.Product_Feature.FirstOrDefault(x => x.ProductID == productID &&
                                 x.FeatureTypeID == typeID && x.FeatureDetailID == featureID);
            utc.Product_Feature.Remove(pf);
            utc.SaveChanges();
            return RedirectToAction("ProductFeature");
        }


        //kullanıcı ekranı
        [Authorize(Roles = "Admin")]
        public ActionResult User()
        {
            List<UserRecord> userRecords = utc.UserRecords.ToList();
            return View(userRecords);
        }
        //kullanıcı ekle
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            List<RoleRecord> role = utc.RoleRecords.ToList();
            return View(role);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser(UserRecord user)
        {
            utc.UserRecords.Add(user);
            utc.SaveChanges();
            return RedirectToAction("User");
        }

        //slider
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult SliderPicture()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Salesman")]
        public ActionResult AddSliderPicture(HttpPostedFileBase fileUpload)
        {
            if(fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                Bitmap pic = new Bitmap(img, Settings.SliderSize);

                string picPath = "/Content/SliderPicture/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                pic.Save(Server.MapPath(picPath));

                Picture pic2 = new Picture();
                pic2.BigImg = picPath;
                utc.Pictures.Add(pic2);
                utc.SaveChanges();
               
            }
            return RedirectToAction("SliderPicture");
        }




    }
}