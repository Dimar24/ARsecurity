using System.Collections.Generic;
using Core.Scenes;
using Subsystem.Question;

namespace UI.Menu.ExamGameCompleteUI
{
    public class ExamGameCompleteViewOptions
    {
        public readonly IReadOnlyCollection<(QuestionData question, int answerNumber, int questionNumber)> Results;
        public readonly IGameScene Scene;
        
        public ExamGameCompleteViewOptions(IReadOnlyCollection<(QuestionData question, int answerNumber, int questionNumber)> results, 
            IGameScene scene)
        {
            Results = results;
            Scene = scene;
        }
    }
}