using System;
using System.Collections;
using System.IO;
using System.Xml;
using Lazy.Generic;
using UnityEngine;

namespace Subsystem.Question
{
    public static class QuestionManager
    {
        private static readonly string LocalizationPath = Path.Combine("Localization", "ru");

        private static readonly QuestionController _controller = new QuestionController();

        public static void LoadQuestions(Action complete = null)
        {
            new LazyCoroutine(Load).OnComplete(() => complete?.Invoke()).Run();
        }

        private static IEnumerator Load()
        {
            var asyncOperation = Resources.LoadAsync(LocalizationPath);

            while (!asyncOperation.isDone)
                yield return null;

            var file = (TextAsset)asyncOperation.asset;
            var stringReader = new StringReader(file.text);
            var xmlReader = new XmlTextReader(stringReader);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            foreach (XmlNode node in xmlDoc.FirstChild)
            {
                Debug.Log(node.Attributes.Count);
            }
        }
    }
}