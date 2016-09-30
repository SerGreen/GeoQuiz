using GeoQuiz.Database.DatabaseClasses;
using GeoQuiz.Models;
using GeoQuiz.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoQuiz.Controllers
{
    public class QuizController : Controller
    {
        // GET: Quiz
        public ViewResult Index()
        {
            List<string> continents = new List<string>() { "AU" };
            List<int> allowedNonSovereignIds = new List<int>();
            Difficulty difficulty = Difficulty.Medium;

            return Huindex(continents, allowedNonSovereignIds, difficulty);
        }

        public ViewResult Huindex(List<string> continents, List<int> allowedNonSovereignIds, Difficulty difficulty)
        {
            GeoDBDataContext db = new GeoDBDataContext();

            List<Country> countries = db.Countries
                // select country if it is on allowed continent and if it is either sovereign or one of the allowed non-sovereigns
                .Where(x => continents.Contains(x.Continent) && (x.IsSovereign || (allowedNonSovereignIds.Contains(x.Id))))
                .Shuffle()
                .ToList();

            List<QuestionAnswerPair> questions = new List<QuestionAnswerPair>();
            foreach(Country c in countries)
            {
                string question = c.Name;
                string answer = c.Id.ToString();
                string[] distractors = db.FlagNeighbours
                    .Where(fn => fn.CountryId1 == c.Id &&
                                 continents.Contains(fn.Country1.Continent) && 
                                (fn.Country1.IsSovereign || allowedNonSovereignIds.Contains(fn.CountryId2)))
                    .OrderBy(x => x.Distance)
                    .Take((int)difficulty)
                    .Shuffle()
                    .Take(3)
                    .Select(x => x.CountryId2.ToString())
                    .ToArray();

                questions.Add(new QuestionAnswerPair(question, answer, distractors));
            }

            Session["Questions"] = questions;   // не должно быть изменения в GET
            ViewBag.QuestionIndex = 0;
            return View(questions[0].Question);
        }

        [HttpPost]
        public ViewResult Index(string answer, int questionIndex = 0)
        {
            List<QuestionAnswerPair> questions = (List<QuestionAnswerPair>)Session["Questions"];
            if (questions.Count - 1 > questionIndex)
            {
                ViewBag.QuestionIndex = questionIndex + 1;
                return View(questions[questionIndex + 1].Question);
            }
            else
                return View();
        }
    }
}