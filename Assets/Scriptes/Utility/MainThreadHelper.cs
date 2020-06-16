using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Pattern;
using UnityEngine;

namespace Utility
{
    public class MainThreadHelper : SingletonMonoBehaviour<MainThreadHelper>
    {
        private static readonly object Mutex = new object();
        private static readonly List<Action> Actions = new List<Action>();
        private static int _mainThreadId = -1;

        public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == _mainThreadId;
        public static bool IsRunning { get; private set; }
        

        public static event Action<bool> Focused;
        public static event Action<bool> Paused;

        public static Coroutine StartCoroutineMainThread(IEnumerator coroutine) => Instance.StartCoroutine(coroutine);

        public static void StopCoroutineMainThread(Coroutine coroutine) => Instance.StopCoroutine(coroutine);

        public static void StopCoroutineMainThread(IEnumerator coroutine) => Instance.StopCoroutine(coroutine);

        public static void Run(Action action)
        {
            if (Instance == null)
                return;
            
            lock (Mutex)
                Actions.Add(action);
        }

        /// <summary>
        /// Warming up.
        /// </summary>
        public void Init() { }

        private static void HandleActions()
        {
            if (Actions.Count <= 0) // It's too much?
                return;

            lock (Mutex)
            {
                for (var i = 0; i < Actions.Count; i++)
                {
                    try
                    {
                        Actions[i]();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        throw;
                    }
                    finally
                    {
                        Actions.RemoveAt(i);
                        --i;
                    }
                }
            }
        }
        
        private void Awake()
        {
            IsRunning = true;
            _mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }
        
        private void Update() => HandleActions();

        private void OnApplicationFocus(bool hasFocus) => Focused?.Invoke(hasFocus);

        private void OnApplicationPause(bool pauseStatus) => Paused?.Invoke(pauseStatus);

        private void OnDestroy() => IsRunning = false;
    }
}