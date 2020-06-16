namespace Subsystem.Question
{
    public struct AnswerData
    {
        public readonly string Text;
        public readonly bool IsCorrect;

        public AnswerData(string text, bool isCorrect = false)
        {
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}