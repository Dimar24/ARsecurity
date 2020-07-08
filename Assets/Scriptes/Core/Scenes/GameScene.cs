using System;
using Core.GameModes;
using Core.Vuforia;
using Lazy.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scenes
{
    public class GameScene : MonoBehaviour, IGameScene
    {
        [SerializeField] private TrackableEventHandler[] _handlers;
        
        private const string SceneName = "Game";
        private const string Tag = "SceneManager";

        private Game _game;

        private void Awake()
        {
            foreach (var handler in _handlers)
            {
                handler.Found += OnTrackableEventHandlerFound; 
                handler.Lost += OnTrackableEventHandlerLost;
            }
        }

        public static void Load(Game game, Action<IGameScene> complete = null)
        {
            void LoadComplete()
            {
                var go = GameObject.FindWithTag(Tag);
                if (go == null)
                {
                    Debug.LogError($"Must be gameObject with tag {Tag} in scene \"{SceneName}\"");
                    complete?.Invoke(null);
                    return;
                }

                var instance = go.GetComponent<GameScene>();
                if (instance == null)
                {
                    Debug.LogError($"Must be component on gameObject with tag {Tag} in scene \"{SceneName}\"");
                    complete?.Invoke(null);
                    return;
                }

                instance._game = game;
                complete?.Invoke(instance);
            }
            
            new LazyCoroutine(() => SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive))
                .OnComplete(LoadComplete)
                .Run();
        }

        public void Unload(Action complete = null)
        {
            new LazyCoroutine(() => SceneManager.UnloadSceneAsync(SceneName))
                .OnComplete(complete)
                .Run();
        }
        
        private void OnTrackableEventHandlerFound(TrackableEventHandler handler)
        {
            _game?.OnObjectFocus(handler.Id);
        }
        
        private void OnTrackableEventHandlerLost(TrackableEventHandler handler)
        {
            _game?.OnObjectDefocus(handler.Id);
        }
    }
}

