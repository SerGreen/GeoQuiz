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
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(GameMode gameMode = GameMode.FlagByCountry)
        {
            switch (gameMode)
            {
                case GameMode.FlagByCountry:
                    return StartFlagByCountryGame();
                case GameMode.CountryByFlag:
                case GameMode.CapitalByCountry:
                default:
                    return RedirectToAction(nameof(Index));
            }
        }
        
        private ActionResult StartFlagByCountryGame(Difficulty difficulty = Difficulty.Medium, int amountOfDistractors = 3)
        {
            List<string> continents = new List<string>() { "AU" };
            List<int> allowedNonSovereignIds = new List<int>();

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

            QuestionsList questionsList = new QuestionsList(questions, GameMode.FlagByCountry);
            Session["Questions"] = questionsList;
            return View(nameof(Quiz), GetQuestionViewModel(questionsList));
        }

        private QuestionViewModel GetQuestionViewModel(QuestionsList questions)
        {
            return new QuestionViewModel()
            {
                Index = questions.CurrentQuestionIndex,
                TotalQuestionsCount = questions.Count,
                CorrectAnswers = questions.CorrectAnswersCount,
                WrongAnswers = questions.WrongAnswersCount,
                Question = questions.CurrentQuestion,
                GameMode = questions.GameMode
            };
        }

        [HttpPost]
        public ActionResult Quiz(QuestionsList questions, string answer, int questionIndex)
        {
            if (questions.TestAnswer(answer) == false)
                TempData["Mistake"] = db.Countries.FirstOrDefault(x => x.Id == int.Parse(answer)).Name;

            if (!questions.EndReached)
            {
                if (!Request.IsAjaxRequest())
                    return View(GetQuestionViewModel(questions));
                else
                    return PartialView("PartialFlagByCountry", GetQuestionViewModel(questions));
            }
            else
                return View("Results");
        }
    }
}