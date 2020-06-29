using System;
using UnityEngine;

namespace UI.Menu
{
    public class BaseMenu : MonoBehaviour
    {
        private void Awake()
        {
            OnCreate();
        }

        private void Start() { }

        private void OnEnable() { }

        private void OnDisable() { }

        protected virtual void OnCreate() { }
        
        public virtual void Open(Action complete = null)
        {
            gameObject.SetActive(true);
            complete?.Invoke();
        }
        
        public virtual void Close(Action complete = null)
        {
            gameObject.SetActive(false);
            complete?.Invoke();
        }
    }
}