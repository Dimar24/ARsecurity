using System.Collections.Generic;
using System.Linq;
using Subsystem.Question;

namespace Core.GameModes.ExamMode
{
    public class ExamGameOptions : GameOptions
    {
        public readonly IReadOnlyDictionary<int, QuestionData> Questions;
        
        public ExamGameOptions(IReadOnlyDictionary<int, QuestionData> questions, params int[] ids) 
            : base(new HashSet<int>(questions.Keys.Concat(ids)))
        {
            Questions = questions;
        }
    }
}