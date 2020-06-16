using System.Collections.Generic;

namespace Subsystem.Question
{
   public class QuestionController
   {
      private readonly Dictionary<int, QuestionData> _questions = new Dictionary<int, QuestionData>();
      private int _lastId = 0;

      public QuestionData? GetQuestion(int id)
      {
         if (_questions.ContainsKey(id))
            return _questions[id];
         return null;
      }

      public int AddQuestion(QuestionData question)
      {
         _questions.Add(_lastId, question);
         return _lastId++;
      }
   }
}