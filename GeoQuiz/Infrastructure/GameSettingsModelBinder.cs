using GeoQuiz.Models;
using GeoQuiz.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoQuiz.Infrastructure
{
    public class GameSettingsModelBinder : DefaultModelBinder
    {
        private const string sessionKey = "Settings";

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            GameSettings settings = null;

            // Try extract necessary values from context
            string gameMode = bindingContext.ValueProvider.GetValue(nameof(GameSettings.GameMode))?.RawValue as string;
            string difficulty = bindingContext.ValueProvider.GetValue(nameof(GameSettings.Difficulty))?.RawValue as string;
            string distractors = bindingContext.ValueProvider.GetValue(nameof(GameSettings.DistractorsAmount))?.RawValue as string;
            string timeLimit = bindingContext.ValueProvider.GetValue(nameof(GameSettings.TimeLimit))?.RawValue as string;
            string[] continents = bindingContext.ValueProvider.GetValue(nameof(GameSettings.Continents))?.RawValue as string[];
            string[] nonSovereigns = bindingContext.ValueProvider.GetValue(nameof(GameSettings.AllowedNonSovereignIds))?.RawValue as string[];

            // If all are present, then assemble GameSettings object
            if (gameMode != null && difficulty != null && distractors != null && timeLimit != null && continents != null)
            {
                try
                {
                    settings = new GameSettings()
                    {
                        AllowedNonSovereignIds = nonSovereigns.Select(int.Parse).ToList(),
                        GameMode = (GameMode) Enum.Parse(typeof(GameMode), gameMode),
                        Difficulty = (Difficulty) Enum.Parse(typeof(Difficulty), difficulty),
                        DistractorsAmount = int.Parse(distractors),
                        TimeLimit = int.Parse(timeLimit),
                        Continents = continents.ToList()
                    };
                }
                catch (FormatException) { /* Bad input */ }
            }

            if (settings == null)
            {
                // If we were not able to compile object, then try to retrieve it from Session
                settings = controllerContext.HttpContext.Session?[sessionKey] as GameSettings;

                // If still no success, then create and save new object
                if (settings == null)
                {
                    settings = new GameSettings();
                    SaveToSession(settings, controllerContext);
                }
            }
            // If we assembled object from binding context, then update settings in Session
            else
                SaveToSession(settings, controllerContext);

            return settings;
        }

        private void SaveToSession(GameSettings settings, ControllerContext controllerContext)
        {
            if (controllerContext.HttpContext.Session != null)
                controllerContext.HttpContext.Session[sessionKey] = settings;
        }
    }
}