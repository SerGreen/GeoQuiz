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
        public ActionResult Index(GameSettings settings, string language = "EN")
        {
            Session["Language"] = language;
            return View(settings);
        }

        [HttpPost]
        [ActionName(nameof(Index))]
        public ActionResult IndexPost(GameSettings settings, GameMode gameMode = GameMode.FlagByCountry)
        {
            settings.GameMode = gameMode;
            return RedirectToAction(nameof(QuizController.Index), QuizController.Nameof);
        }

        [HttpPost]
        public void SaveGameSettings(Infrastructure.GameSettingsSave settings)
        {
            // GameSettingsSave is built by deafult model binder from data provided by ajax request, instead of retrieving from Session by custom GameSettings model binder
            Session["Settings"] = settings;
        }
    }
}