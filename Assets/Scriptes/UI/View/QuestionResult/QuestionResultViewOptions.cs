using System.Collections.Generic;
using Subsystem.Question;
using UI.View.Answer;

namespace UI.View.QuestionResult
{
    public class QuestionResultViewOptions
    {
        public readonly IReadOnlyCollection<AnswerData> Answers;
        public readonly int AnswerNumber;
        public readonly AnswerViewOptions CorrectOptions;
        public readonly AnswerViewOptions IncorrectOptions;

        public QuestionResultViewOptions(IReadOnlyCollection<AnswerData> answers, int answerNumber,
            in AnswerViewOptions correctOptions, in AnswerViewOptions incorrectOptions)
        {
            Answers = answers;
            AnswerNumber = answerNumber;
            CorrectOptions = correctOptions;
            IncorrectOptions = incorrectOptions;
        }
    }
}