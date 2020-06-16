using System;
using System.Collections.Generic;
using System.IO;

namespace Subsystem.Question
{
    public static class QuestionManager
    {
        private static readonly string LocalizationPath = Path.Combine("Localization", "ru", "Questions");
        private static readonly QuestionController _controller = new QuestionController();

        public static void LoadQuestionsAsync(Action complete = null)
        {
            new QuestionLoader(LocalizationPath)
                .LoadAsync(result =>
            {
                OnLoaded(result);
                complete?.Invoke();
            });
        }

        public static QuestionData? GetQuestion(int id) => _controller.GetQuestion(id);
        
        private static void OnLoaded(List<QuestionLoadedData> data)
        {
            foreach (var d in data)
                _controller.AddQuestion(d.Id, d.Data);
        }
    }
}