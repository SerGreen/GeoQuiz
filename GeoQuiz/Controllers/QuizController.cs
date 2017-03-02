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
        GeoDBDataContext db = new GeoDBDataContext();

        // GET: Quiz
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(int amountOfDistractors = 3)
        {
            List<string> continents = new List<string>() { "AU" };
            List<int> allowedNonSovereignIds = new List<int>();
            Difficulty difficulty = Difficulty.Medium;

            List<Country> countries = db.Countries
                // select country if it is on allowed continent and if it is either sovereign or one of the allowed non-sovereigns
                .Where(x => continents.Contains(x.Continent) && (x.IsSovereign || (allowedNonSovereignIds.Contains(x.Id))))
                .Shuffle()
                .ToList();

            // Assemble questions for each country
            List<QuestionAnswerPair> questions = new List<QuestionAnswerPair>();
            foreach (Country c in countries)
            {
                string question = c.Name;
                string answer = c.Id.ToString();
                // Select distractors: 
                // 1. Select all similar flags and order by similarity
                // 2. Take N most similar and randomize (N value is smaller for greater difficulties => the more similar distractors are selected)
                // 3. Select M entries, where M is number of distraction options
                string[] distractors = db.FlagNeighbours
                    .Where(fn => fn.CountryId1 == c.Id &&
                                 continents.Contains(fn.Country1.Continent) &&
                                (fn.Country1.IsSovereign || allowedNonSovereignIds.Contains(fn.CountryId2)))
                    .OrderBy(x => x.Distance)
                    .Take((int)difficulty)
                    .Shuffle()
                    .Take(amountOfDistractors)
                    .Select(x => x.CountryId2.ToString())
                    .ToArray();

                questions.Add(new QuestionAnswerPair(question, answer, distractors));
            }

            Session["Questions"] = questions;
            return View("List", new QuestionViewModel() { Index = 0, Question = questions[0].Question });
        }

        [HttpPost]
        public ViewResult List(List<QuestionAnswerPair> questions, string answer, int questionIndex)
        {
            if (answer != questions[questionIndex].Answer)
                TempData["Mistake"] = $"Wrong! It was the flag of {db.Countries.FirstOrDefault(x => x.Id == int.Parse(answer)).Name}.";

            if (questions.Count - 1 > questionIndex++)
            {
                return View(new QuestionViewModel() { Index = questionIndex, Question = questions[questionIndex].Question });
            }
            else
                return View("Index");
        }
    }
}