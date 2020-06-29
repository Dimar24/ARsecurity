using System;
using System.Collections;
using System.Collections.Generic;
using UI.Menu;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private BaseMenu[] _menus;
    
    private static readonly Dictionary<Type, BaseMenu> Menus = new Dictionary<Type, BaseMenu>();
    private static MenuManager Instance { get; set; }
    private static BaseMenu _current;

    private void Awake()
    {
        Instance = this;
        foreach (var menu in _menus)
            AddMenu(menu);
    }

    public static void Open<T>(Action complete = null) where T : BaseMenu
    {
        var openMenu = Menus[typeof(T)];
        var closeMenu = _current;

        void OpenComplete()
        {
            _current = openMenu;
            complete?.Invoke();
        }
        
        if (closeMenu == null)
            openMenu.Open(OpenComplete);
        else
            closeMenu.Close(() => openMenu.Open(OpenComplete));
    }

    private void AddMenu(BaseMenu menu)
    {
        menu.gameObject.SetActive(false);
        Debug.Log(menu.GetType());
        Menus.Add(menu.GetType(), menu);
    }
}
