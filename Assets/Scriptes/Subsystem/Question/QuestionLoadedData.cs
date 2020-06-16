namespace Subsystem.Question
{
    public struct QuestionLoadedData
    {
        public readonly int Id;
        public readonly QuestionData Data;

        public QuestionLoadedData(int id, QuestionData data)
        {
            Id = id;
            Data = data;
        }
    }
}