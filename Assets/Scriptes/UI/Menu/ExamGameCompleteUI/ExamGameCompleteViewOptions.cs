using System.Collections.Generic;
using Subsystem.Question;

namespace UI.Menu.ExamGameCompleteUI
{
    public class ExamGameCompleteViewOptions
    {
        public readonly IReadOnlyCollection<(QuestionData question, int answerNumber)> Results;

        public ExamGameCompleteViewOptions(IReadOnlyCollection<(QuestionData question, int answerNumber)> results)
        {
            Results = results;
        }
    }
}