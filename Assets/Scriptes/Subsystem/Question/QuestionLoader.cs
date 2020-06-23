using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using Utility;

namespace Subsystem.Question
{
    public class QuestionLoader
    {
        private const string IdAtt = "id";
        private const string TextAtt = "text";
        private const string ValueAtt = "value";
        private const string CorrectValue = "1";

        private readonly string _path;
        public QuestionLoader(string path)
        {
            _path = path;
        }
        
        public void LoadAsync(Action<List<QuestionLoadedData>> complete)
        {
            MainThreadHelper.StartCoroutineMainThread(LoadRoutine(complete));
        }

        private IEnumerator LoadRoutine(Action<List<QuestionLoadedData>> complete)
        {
            
            var asyncOperation = Resources.LoadAsync(_path);

            while (!asyncOperation.isDone)
                yield return null;

            var file = asyncOperation.asset as TextAsset;
            var stringReader = new StringReader(file.text);
            var xmlReader = new XmlTextReader(stringReader);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            var root = xmlDoc.ChildNodes[1];
            var questions = new List<QuestionLoadedData>();
            
            foreach (XmlNode questionNode in root.ChildNodes)
            {
                var questionAttributes = questionNode.Attributes;
                if (questionAttributes == null)
                    continue;

                var id = int.Parse(questionAttributes[IdAtt].Value);
                var title = questionAttributes[TextAtt].Value;
                var answers = new List<AnswerData>();

                foreach (XmlNode answerNode in questionNode.ChildNodes)
                {
                    var answerText = answerNode.InnerText;
                    var isCorrect = IsCorrectAnswer(answerNode.Attributes);
                    var answer = new AnswerData(answerText, isCorrect);
                    answers.Add(answer);
                }
                
                var question = new QuestionData(title, answers);
                questions.Add(new QuestionLoadedData(id, question));
            }

            complete?.Invoke(questions);
        }
        
        private static bool IsCorrectAnswer(XmlAttributeCollection attributes = null)
        {
            if (attributes == null || attributes.Count < 1)
                return false;

            var value = attributes[ValueAtt];
            return value != null && value.Value == CorrectValue;
        }
    }
}