using System;
using UnityEngine;
using Vuforia;

namespace Core.Vuforia
{
    public class TrackableEventHandler : DefaultTrackableEventHandler
    {
        [SerializeField] private int _id;
    
        private Renderer[] _renderers;

        public int Id => _id;
    
        public event Action<TrackableEventHandler> Found;
        public event Action<TrackableEventHandler> Lost;

        private void OnEnable()
        {
            // TODO Create Manager 
            GameManager.AddTrackableEventHandler(this);
        }


        protected override void Start()
        {
            base.Start();
            if (mTrackableBehaviour == null)
                Debug.LogError($"{nameof(TrackableEventHandler)} requires {nameof(TrackableBehaviour)}!");
            UpdateComponents();
        }
    
        protected override void OnTrackingFound()
        {
            UpdateComponents();
            EnableChildComponents(true);
            Found?.Invoke(this);
        }
    
        protected override void OnTrackingLost()
        {
            UpdateComponents();
            EnableChildComponents(false);
            Lost?.Invoke(this);
        }

        private void UpdateComponents()
        {
            _renderers = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
        }
        
        private void EnableChildComponents(bool value)
        {
            foreach (var component in _renderers)
                component.enabled = value;
        }
    }

}
