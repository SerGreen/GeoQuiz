using GeoQuiz.Models;
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
            if (controllerContext.HttpContext.Session != null)
            {
                settings = controllerContext.HttpContext.Session[sessionKey] as GameSettings;
            }

            if (settings == null)
            {
                settings = new GameSettings();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = settings;
                }
            }

            return settings;
        }
    }
}