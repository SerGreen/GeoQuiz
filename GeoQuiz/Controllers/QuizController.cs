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
        
        public static string Nameof => nameof(QuizController).Replace("Controller", "");

        [HttpGet]
        public ActionResult Index(GameSettings settings)
        {
            switch (settings.GameMode)
            {
                case GameMode.FlagByCountry:
                    return StartFlagByCountryGame(settings);
                case GameMode.CountryByFlag:
                    return StartCountryByFlagGame(settings);
                case GameMode.CapitalByCountry:
                default:
                    return RedirectToAction(nameof(MenuController.Index), MenuController.Nameof);
            }
        }
        
        private List<Country> GetSelectedCountries(GameSettings settings)
        {
            return db.Countries
                // select country if it is on allowed continent and if it is either sovereign or one of the allowed non-sovereigns
                .Where(x => settings.Continents.Contains(x.Continent) && (x.IsSovereign || (settings.AllowedNonSovereignIds.Contains(x.Id))))
                .Shuffle()
                .ToList();
        }

        [NonAction]
        private ActionResult StartFlagByCountryGame(GameSettings settings)
        {
            List<Country> countries = GetSelectedCountries(settings);

            // Assemble questions for each country
            int calculatedDistractorsAmount = Math.Max((int) settings.Difficulty, settings.DistractorsAmount);
            List<QuestionAnswerPair> questions = new List<QuestionAnswerPair>();
            foreach (Country c in countries)
            {
                string question = c.Name;
                string answer = c.Id.ToString();
                // Select distractors: 
                // 1. Select all similar flags and order by similarity
                // 2. Take N most similar and randomize (the greater difficulty, the smaller the N value is => the more similar distractors are selected)
                // 3. Select M entries, where M is number of distraction options
                string[] distractors = db.FlagNeighbours
                    .Where(fn => fn.CountryId1 == c.Id &&
                                 settings.Continents.Contains(fn.Country1.Continent) &&
                                (fn.Country1.IsSovereign || settings.AllowedNonSovereignIds.Contains(fn.CountryId2)))
                    .OrderBy(x => x.Distance)
                    .Take(calculatedDistractorsAmount)
                    .Shuffle()
                    .Take(settings.DistractorsAmount)
                    .Select(x => x.CountryId2.ToString())
                    .ToArray();

                questions.Add(new QuestionAnswerPair(question, answer, distractors));
            }

            QuestionsList questionsList = new QuestionsList(questions);
            Session["Questions"] = questionsList;
            return View(nameof(Quiz), GetQuestionViewModel(questionsList));
        }

        [NonAction]
        private ActionResult StartCountryByFlagGame(GameSettings settings)
        {
            List<Country> countries = GetSelectedCountries(settings);

            // Assemble questions for each country
            int calculatedDistractorsAmount = Math.Max((int) settings.Difficulty, settings.DistractorsAmount);
            List<QuestionAnswerPair> questions = new List<QuestionAnswerPair>();
            foreach (Country c in countries)
            {
                string question = c.Id.ToString();
                string answer = c.Name;
                // Everything as in flags mode, but now question is country ID (flag image selected by id) and answer and distractors are country names
                string[] distractors = db.FlagNeighbours
                    .Where(fn => fn.CountryId1 == c.Id &&
                                 settings.Continents.Contains(fn.Country1.Continent) &&
                                (fn.Country1.IsSovereign || settings.AllowedNonSovereignIds.Contains(fn.CountryId2)))
                    .OrderBy(x => x.Distance)
                    .Take(calculatedDistractorsAmount)
                    .Shuffle()
                    .Take(settings.DistractorsAmount)
                    .Select(x => x.Country1.Name)
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
            // Store correct answer
            string correctAnswer = questions.CorrectAnswer;

            // Check answer given by player and go to next question
            if (questions.TestAnswer(answer, true /* next question */) == false)
            {
                // If answer is wrong => set mistake message
                // No answer = timeout
                if (answer.Length == 0)
                    TempData["Mistake"] = "Timeout";
                // Answer was wrong
                else
                {
                    string mistakeMessage = null;

                    switch (settings.GameMode)
                    {
                        case GameMode.FlagByCountry:
                            mistakeMessage = db.Countries.FirstOrDefault(x => x.Id == int.Parse(answer)).Name; break;
                        case GameMode.CountryByFlag:
                        case GameMode.CapitalByCountry:
                            mistakeMessage = correctAnswer; break;
                    }
                    TempData["Mistake"] = mistakeMessage;
                }
            }

            // Return view
            // If this is not Ajax request => return whole page
            if (!Request.IsAjaxRequest())
                return View(GetQuestionViewModel(questions));
            // If Ajax => return only partial view with next question
            else
            {
                string partialViewName = null;
                switch (settings.GameMode)
                {
                    case GameMode.FlagByCountry:
                        partialViewName = "PartialFlagByCountry";
                        break;
                    case GameMode.CountryByFlag:
                        partialViewName = "PartialCountryByFlag";
                        break;
                    case GameMode.CapitalByCountry:
                        partialViewName = "PartialCapitalByCountry";
                        break;
                }

                return PartialView(partialViewName, GetQuestionViewModel(questions));
            }
        }

        private string GetMistakeMessage(string answer, GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.FlagByCountry:
                    return db.Countries.FirstOrDefault(x => x.Id == int.Parse(answer)).Name;
                case GameMode.CountryByFlag:
                    return db.Countries.FirstOrDefault(x => x.Id == int.Parse(answer)).Name;
                case GameMode.CapitalByCountry:
                    return db.Countries.FirstOrDefault(x => x.Id == int.Parse(answer)).Name;
                default:
                    return "Unknown";
            }
        }

        [HttpGet]
        public ActionResult Results(QuestionsList questions, GameSettings settings)
        {
            return View(new ResultsViewModel() { Questions = questions, GameSettings = settings });
        }
    }
}