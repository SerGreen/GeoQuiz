using GeoQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoQuiz.Infrastructure
{
    public class QuestionsModelBinder : IModelBinder
    {
        private const string sessionKey = "Questions";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            QuestionsList questions = null;
            if (controllerContext.HttpContext.Session != null)
            {
                questions = (QuestionsList) controllerContext.HttpContext.Session[sessionKey];
            }
            
            if (questions == null)
            {
                questions = new QuestionsList();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = questions;
                }
            }

            return questions;
        }
    }
}