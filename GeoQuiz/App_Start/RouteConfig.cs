using GeoQuiz.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GeoQuiz
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                url: "",
                defaults: new { controller = MenuController.Nameof, action = nameof(MenuController.Index), lang = "en" }
            );
            
            routes.MapRoute(
                null,
                url: "Quiz",
                defaults: new { controller = QuizController.Nameof, action = nameof(QuizController.Index), lang = "en" }
            );

            routes.MapRoute(
                null,
                url: "Results",
                defaults: new { controller = QuizController.Nameof, action = nameof(QuizController.Results), lang = "en" }
            );

            routes.MapRoute(
                null,
                url: "{lang}",
                defaults: new { controller = MenuController.Nameof, action = nameof(MenuController.Index) }
            );

            routes.MapRoute(
                null,
                url: "{lang}/Quiz",
                defaults: new { controller = QuizController.Nameof, action = nameof(QuizController.Index) }
            );
                        
            routes.MapRoute(
                null,
                url: "{lang}/Results",
                defaults: new { controller = QuizController.Nameof, action = nameof(QuizController.Results) }
            );

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}"
            );
        }
    }
}
