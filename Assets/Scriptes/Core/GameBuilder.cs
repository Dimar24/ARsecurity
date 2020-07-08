using System.Collections.Generic;
using Core.GameModes.ExamMode;
using Core.GameModes.TourMode;
using Subsystem.Question;

namespace Core
{
    public static class GameBuilder
    {
        public static TourGame CreateTourGame()
        {
            var ids = new HashSet<int>(QuestionManager.ReadOnlyQuestions.Keys);
            var options = new TourGameOptions(ids);
            return new TourGame(options);
        }
        
        public static ExamGame CreateExamGame()
        {
            var options = new ExamGameOptions(QuestionManager.ReadOnlyQuestions);
            return new ExamGame(options);
        }
    }
}