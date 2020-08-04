using System.Collections.Generic;
using Subsystem.Question;
using UI.View.Answer;

namespace UI.View.QuestionResult
{
    public class QuestionResultViewOptions
    {
        public readonly QuestionData QuestionData;
        public readonly int QuestionNumber;
        public readonly int AnswerNumber;
        public readonly AnswerViewOptions CorrectOptions;
        public readonly AnswerViewOptions IncorrectOptions;

        public QuestionResultViewOptions(
            in QuestionData questionData, 
            int questionNumber, 
            int answerNumber,
            in AnswerViewOptions correctOptions, 
            in AnswerViewOptions incorrectOptions)
        {
            QuestionData = questionData;
            QuestionNumber = questionNumber;
            AnswerNumber = answerNumber;
            CorrectOptions = correctOptions;
            IncorrectOptions = incorrectOptions;
        }
    }
}