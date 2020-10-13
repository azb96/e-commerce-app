using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uyumsoft_ticaret_app.Models;

namespace uyumsoft_ticaret_app.ViewModel
{
    public class ProductViewModel
    {

        public List<Product> ProductList { get; set; }

        public List<Category> CategoryList { get; set; }

        public List<Brand> BrandList { get; set; }

        public List<UserRecord> SalesmanList { get; set; }
    }
}