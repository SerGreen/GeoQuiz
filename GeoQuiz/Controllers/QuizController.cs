using GeoQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoQuiz.Controllers
{
    public class QuizController : Controller
    {
        List<QuestionAnswerPair> questions;

        // GET: Quiz
        public ActionResult Index(List<string> continents, List<int> allowedNonSovereigns)
        {
            

            return View();
        }
    }
}