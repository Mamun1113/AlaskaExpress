using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AlaskaExpress
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
<<<<<<< Updated upstream
=======
            BundleConfig.RegisterBundles(BundleTable.Bundles);
>>>>>>> Stashed changes
        }
        public Boolean isValidPlace(string s)
        {
            if (s == "Dhaka") return true;
            if (s == "Chittagong") return true;
            if (s == "Rajshahi") return true;
            if (s == "Khulna") return true;
            if (s == "Barisal") return true;
            if (s == "Sylhet") return true;
            if (s == "Rangpur") return true;
            return false;
        }

        public Boolean isValidDate(string s)
        {
            if (s.Length != 10) return false;
            for (int i = 0; i < 2; i++)
            {
                if (!isNumber(s[i])) return false;
            }
            if (s[2] != '/') return false;
            for (int i = 3; i < 5; i++)
            {
                if (!isNumber(s[i])) return false;
            }
            if (s[5] != '/') return false;
            for (int i = 6; i < 10; i++)
            {
                if (!isNumber(s[i])) return false;
            }
            return true;
        }

        public Boolean isNumber(char c)
        {
            if (c < '0' || c > '9') return false;
            return true;
        }

    }
}
