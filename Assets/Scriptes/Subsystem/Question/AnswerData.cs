namespace Subsystem.Question
{
    public struct AnswerData
    {
        public readonly string AnswerText;
        public readonly bool IsCorrect;

        public AnswerData(string answerText, bool isCorrect = false)
        {
            AnswerText = answerText;
            IsCorrect = isCorrect;
        }
    }
}