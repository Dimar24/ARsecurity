using System.Collections.Generic;

namespace Subsystem.Question
{
   public class QuestionController
   {
      private readonly Dictionary<int, QuestionData> _questions = new Dictionary<int, QuestionData>();
      public int Count => _questions.Count;

      public QuestionData? GetQuestion(int id)
      {
         if (_questions.ContainsKey(id))
            return _questions[id];
         return null;
      }

      public void AddQuestion(int id, QuestionData question)
      {
         _questions.Add(id, question);
      }
   }
}