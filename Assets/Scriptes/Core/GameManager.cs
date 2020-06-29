using System;
using System.Collections.Generic;
using Core.GameModes;
using Core.GameModes.ExamMode;
using Core.GameModes.TourMode;
using Core.Vuforia;
using Lazy.Generic;
using Subsystem.Question;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public static class GameManager
    {
        private const string SceneName = "Game";
        
        private static readonly List<TrackableEventHandler> EventHandlers = new List<TrackableEventHandler>();

        public static Game Game { get; private set; }

        public static void AddTrackableEventHandler(TrackableEventHandler handler)
        {
            EventHandlers.Add(handler);
            handler.Found += OnTrackableEventHandlerFound;
            handler.Lost += OnTrackableEventHandlerLost;
        }

        public static void Load(SessionOptions options, Action complete = null)
        {
            Game = CreateGame(options.GameModeType);
            new LazyCoroutine(() => SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive))
                .OnComplete(complete)
                .Run();
        }

        public static void Unload(Action complete = null)
        {
            Game = null;
            foreach (var handler in EventHandlers)
            {
                handler.Found -= OnTrackableEventHandlerFound; 
                handler.Lost -= OnTrackableEventHandlerLost;
            }
            EventHandlers.Clear();
            new LazyCoroutine(() => SceneManager.UnloadSceneAsync(SceneName))
                .OnComplete(complete)
                .Run();
        }
        
        private static Game CreateGame(GameModeType gameModeType)
        {
            switch (gameModeType)
            {
                case GameModeType.Tour: return CreateTourGame();
                case GameModeType.Exam: return CreateExamGame();
            }

            Debug.LogError($"Raw enum element [{nameof(GameModeType)}] [{gameModeType}]");
            return CreateTourGame();
        }

        private static TourGame CreateTourGame()
        {
            var ids = new HashSet<int>(QuestionManager.ReadOnlyQuestions.Keys);
            var options = new TourGameOptions(ids);
            return new TourGame(options);
        }
        private static ExamGame CreateExamGame()
        {
            var options = new ExamGameOptions(QuestionManager.ReadOnlyQuestions);
            return new ExamGame(options);
        }
        
        private static void OnTrackableEventHandlerFound(TrackableEventHandler handler)
        {
            Game?.OnObjectFocus(handler.Id);
        }
        
        private static void OnTrackableEventHandlerLost(TrackableEventHandler handler)
        {
            Game?.OnObjectDefocus(handler.Id);
        }
    }
}