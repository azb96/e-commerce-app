using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace uyumsoft_ticaret_app.App_Classes
{
    public class Settings
    {
        public static Size ProductMiddleSize
        {
            get 
            {   
                Size size = new Size();
                size.Width = Convert.ToInt32(ConfigurationManager.AppSettings["ProductMiddleWidth"]);

                size.Height = Convert.ToInt32(ConfigurationManager.AppSettings["ProductMiddleHeight"]);

                return size;
            }       
        
        }

        public static Size ProductBigSize
        {
            get
            {
                Size size = new Size();
                size.Width = Convert.ToInt32(ConfigurationManager.AppSettings["ProductBigWidth"]);

                size.Height = Convert.ToInt32(ConfigurationManager.AppSettings["ProductBigHeight"]);

                return size;
            }

        }
        
        public static Size SliderSize
        {
            get
            {
                Size size = new Size();

                size.Width = Convert.ToInt32(ConfigurationManager.AppSettings["SliderWidth"]);

                size.Height = Convert.ToInt32(ConfigurationManager.AppSettings["SliderHeight"]);

                return size;
            }
        }
    }
}