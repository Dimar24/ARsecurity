using System.Collections.Generic;

namespace Subsystem.Question
{
    public struct QuestionData
    {
        public readonly string QuestionText;
        public readonly List<AnswerData> Answers;

        public QuestionData(string questionText, List<AnswerData> answers)
        {
            QuestionText = questionText;
            Answers = answers;
        }
    }

}

