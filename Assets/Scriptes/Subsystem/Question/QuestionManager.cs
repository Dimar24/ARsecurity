using System;
using System.Collections.Generic;
using System.IO;

namespace Subsystem.Question
{
    public static class QuestionManager
    {
        private static readonly string LocalizationPath = Path.Combine("Localization", "ru", "Questions");
        private static readonly Dictionary<int, QuestionData> Questions = new Dictionary<int, QuestionData>();

        public static IReadOnlyDictionary<int, QuestionData> ReadOnlyQuestions => Questions;
        
        public static void LoadQuestionsAsync(Action complete = null)
        {
            new QuestionLoader(LocalizationPath)
                .LoadAsync(result =>
            {
                OnLoaded(result);
                complete?.Invoke();
            });
        }
        
        private static void OnLoaded(List<QuestionLoadedData> data)
        {
            foreach (var d in data)
                Questions.Add(d.Id, d.Data);
        }
    }
}