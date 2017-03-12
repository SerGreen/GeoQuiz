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
    public class MenuController : Controller
    {
        GeoDBDataContext db = new GeoDBDataContext();

        public static string Nameof => nameof(MenuController).Replace("Controller", "");

        // GET: Menu
        [HttpGet]
        public ActionResult Index(GameSettings settings)
        {
            return View(settings);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(GameSettings settings, GameMode gameMode = GameMode.FlagByCountry)
        {
            settings.GameMode = gameMode;
            return RedirectToAction(nameof(QuizController.Index), QuizController.Nameof);
        }

        [HttpPost]
        public bool SaveGameSettings(GameSettings settings)
        {
            var boob = 8008;
            return true;
        }
    }
}