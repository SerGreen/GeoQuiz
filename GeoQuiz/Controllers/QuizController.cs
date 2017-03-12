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
        Random rnd = new Random();
        
        public static string Nameof => nameof(QuizController).Replace("Controller", "");

        [HttpGet]
        public ActionResult Index(GameSettings settings)
        {
            switch (settings.GameMode)
            {
                case GameMode.FlagByCountry:
                    return StartFlagByCountryGame(settings);
                case GameMode.CountryByFlag:
                case GameMode.CapitalByCountry:
                default:
                    return RedirectToAction(nameof(MenuController.Index), MenuController.Nameof);
            }
        }
        
        private ActionResult StartFlagByCountryGame(GameSettings settings)
        {
            List<Country> countries = db.Countries
                // select country if it is on allowed continent and if it is either sovereign or one of the allowed non-sovereigns
                .Where(x => settings.Continents.Contains(x.Continent) && (x.IsSovereign || (settings.AllowedNonSovereignIds.Contains(x.Id))))
                .Shuffle(rnd)
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
                                 settings.Continents.Contains(fn.Country1.Continent) &&
                                (fn.Country1.IsSovereign || settings.AllowedNonSovereignIds.Contains(fn.CountryId2)))
                    .OrderBy(x => x.Distance)
                    .Take((int)settings.Difficulty)
                    .Shuffle(rnd)
                    .Take(settings.DistractorsAmount)
                    .Select(x => x.CountryId2.ToString())
                    .ToArray();

                questions.Add(new QuestionAnswerPair(question, answer, distractors));
            }

            QuestionsList questionsList = new QuestionsList(questions);
            Session["Questions"] = questionsList;
            return View(nameof(Quiz), GetQuestionViewModel(questionsList));
        }

        private QuestionViewModel GetQuestionViewModel(QuestionsList questions)
        {
            GameSettings settings = Session["Settings"] as GameSettings;
            return new QuestionViewModel()
            {
                Index = questions.CurrentQuestionIndex,
                TotalQuestionsCount = questions.Count,
                CorrectAnswers = questions.CorrectAnswersCount,
                WrongAnswers = questions.WrongAnswersCount,
                Question = questions.CurrentQuestion,
                GameMode = settings.GameMode,
                TimeLimit = settings.TimeLimit,
                CorrectStreak = questions.CurrentCorrectStreak,
                PointsReward = questions.PointsForAnswer,
                Score = questions.Score
            };
        }

        [HttpPost]
        public ActionResult Quiz(QuestionsList questions, GameSettings settings, string answer, int questionIndex)
        {
            if (questions.TestAnswer(answer) == false)
            {
                if (answer.Length == 0)
                    TempData["Mistake"] = "Timeout";
                else
                    TempData["Mistake"] = db.Countries.FirstOrDefault(x => x.Id == int.Parse(answer)).Name;
            }

            if (!Request.IsAjaxRequest())
                return View(GetQuestionViewModel(questions));
            else
                return PartialView("PartialFlagByCountry", GetQuestionViewModel(questions));
        }

        [HttpGet]
        public ActionResult Results(QuestionsList questions, GameSettings settings)
        {
            return View(new ResultsViewModel() { Questions = questions, GameSettings = settings });
        }
    }
}