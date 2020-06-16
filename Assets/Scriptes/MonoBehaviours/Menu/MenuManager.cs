using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _modeMenu;

    private static MenuManager Instance { get; set; }

    public static GameObject MainMenu => Instance._mainMenu;

    public static GameObject ModeMenu => Instance._modeMenu;

    private void Awake()
    {
        Instance = this;
    }
}
