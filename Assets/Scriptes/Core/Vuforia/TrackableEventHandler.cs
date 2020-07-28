using System;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace Core.Vuforia
{
    public class TrackableEventHandler : DefaultTrackableEventHandler
    {
        [SerializeField] private int _id;

        private readonly List<GameObject> _gameObjects = new List<GameObject>();

        public int Id => _id;

        public event Action<TrackableEventHandler> Found;
        public event Action<TrackableEventHandler> Lost;


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
            _gameObjects.Clear();
            foreach (Transform tran in transform)
                _gameObjects.Add(tran.gameObject);
        }

        private void EnableChildComponents(bool value)
        {
            foreach (var go in _gameObjects)
                go.SetActive(value);
        }
    }
}