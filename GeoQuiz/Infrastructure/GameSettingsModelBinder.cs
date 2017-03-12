using GeoQuiz.Models;
using GeoQuiz.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoQuiz.Infrastructure
{
    public class GameSettingsModelBinder : IModelBinder
    {
        private const string sessionKey = "Settings";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            GameSettings settings = null;

            // Try to retrieve from Session
            settings = controllerContext.HttpContext.Session?[sessionKey] as GameSettings;

            // If no success, then create new object and save it
            if (settings == null)
            {
                settings = new GameSettings();
                if (controllerContext.HttpContext.Session != null)
                    controllerContext.HttpContext.Session[sessionKey] = settings;
            }

            return settings;
        }
    }
}