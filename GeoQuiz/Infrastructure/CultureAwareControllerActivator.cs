using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GeoQuiz.Infrastructure
{
    public class CultureAwareControllerActivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            //Get the {lang} parameter in the RouteData
            string language = requestContext.RouteData.Values["lang"] as string;
            language = language ?? "en";

            try
            {
                //Set the culture info
                Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            catch (Exception)
            { throw new NotSupportedException($"ERROR: Invalid language code '{language}'."); }

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}