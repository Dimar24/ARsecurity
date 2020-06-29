using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Subsystem.Question;

namespace Core.GameModes.ExamMode
{
    public class ExamGameOptions : GameOptions
    {
        public readonly IReadOnlyDictionary<int, QuestionData> Questions;
        public readonly HashSet<int> ObjectIds;
        
        public ExamGameOptions(IReadOnlyDictionary<int, QuestionData> questions, params int[] ids) 
            : base(new HashSet<int>(questions.Keys.Concat(ids)))
        {
            Questions = questions;
            ObjectIds = new HashSet<int>(Ids.Except(questions.Keys));
        }
    }
}