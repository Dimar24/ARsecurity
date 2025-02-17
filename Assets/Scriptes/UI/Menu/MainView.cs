﻿using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public sealed class MainView : View
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private int _number = 10;
        [SerializeField] private float _durationReset = 1.0f;
    
        private int _count;
        private float _lastClicked;

        protected override void OnCreate()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            base.OnCreate();
        }

        private void OnStartButtonClicked()
        {
            _count = 0;
            ViewManager.OpenModeView();
        }
    
        private void OnExitButtonClicked()
        {
            if (Time.time - _lastClicked > _durationReset)
                _count = 0;
            _count++;
            if (_count == _number)
            {
                _count = 0;
                ViewManager.OpenExitView();
            }
            _lastClicked = Time.time;   
    }
    }
}
