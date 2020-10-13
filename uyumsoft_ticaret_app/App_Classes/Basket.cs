using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using uyumsoft_ticaret_app.Models;

namespace uyumsoft_ticaret_app.App_Classes
{
    public class Basket
    {
        public static Basket ActiveBasket
        {
            get
            {
                HttpContext hctx = HttpContext.Current;
                if(hctx.Session["ActiveBasket"] == null)
                {
                    hctx.Session["ActiveBasket"] = new Basket();

                }
                          
                return (Basket)hctx.Session["ActiveBasket"];
            }
        }

        private List<BasketItem> productList = new List<BasketItem>();

        public List<BasketItem> ProductsList
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
            }
        }

        public void AddToBasket(BasketItem basketItem)
        {
            if(HttpContext.Current.Session["ActiveBasket"] != null)
            {
                Basket b = (Basket)HttpContext.Current.Session["ActiveBasket"];

                if (b.ProductsList.Any(x => x.Product.id == basketItem.Product.id))
                    b.ProductsList.FirstOrDefault(x => x.Product.id == basketItem.Product.id).Quantity++;
                else
                {
                    b.ProductsList.Add(basketItem);
                }
            }
            else
            {
                Basket b2 = new Basket();
                b2.ProductsList.Add(basketItem);

                HttpContext.Current.Session["ActiveBasket"] = b2;
            }  
               
        }

        public decimal TotalCost
        {
            get
            {
                return ProductsList.Sum(x => x.TotalPrice);
            }
        }
    }

   
    public class BasketItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double Discount { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Product.Price * Quantity * (decimal)(1 - Discount);
            }
        }

        
    }
}