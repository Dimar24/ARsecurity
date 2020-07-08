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
#if UNITY_EDITOR
            // ToDo удалить тестовый костыль
            data.RemoveRange(2, data.Count - 2);
#endif
            foreach (var d in data)
                Questions.Add(d.Id, d.Data);
        }
    }
}