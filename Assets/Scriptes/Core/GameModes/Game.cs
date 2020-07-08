using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Core.GameModes
{
    public interface IFocuser
    {
        void OnObjectFocus(int id);
        void OnObjectDefocus(int id);
    }
    
    public abstract class Game : IFocuser
    {
        protected readonly HashSet<int> Ids;
        
        public Game(GameOptions options)
        {
            Ids = options.Ids;
        }
        
        public virtual void OnObjectFocus(int id)
        {
            Debug.Log("Focus " + Ids.Contains(id));
        }
        
        public virtual void OnObjectDefocus(int id)
        {
            Debug.Log("Defocus " + Ids.Contains(id));
        }

        protected Coroutine StartCoroutine(IEnumerator routine) => MainThreadHelper.StartCoroutineMainThread(routine);
        protected void StopCoroutine(Coroutine coroutine) => MainThreadHelper.StopCoroutineMainThread(coroutine);
    }
}