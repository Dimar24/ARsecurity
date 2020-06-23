using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _modeMenu;
    [SerializeField] private GameObject _exitPopup;

    private static MenuManager Instance { get; set; }

    public static GameObject MainMenu => Instance._mainMenu;

    public static GameObject ModeMenu => Instance._modeMenu;
    
    public static GameObject ExitPopup  => Instance._exitPopup;

    private void Awake()
    {
        Instance = this;
    }
}
