using System;
using System.Collections.Generic;
using UI.Menu;
using UI.Menu.ExamGameCompleteUI;
using UI.Menu.ExamGameUI;
using UI.Menu.TourGameUI;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private BaseView[] _menus;
    
    private static readonly Dictionary<Type, BaseView> Menus = new Dictionary<Type, BaseView>();
    private static ViewManager Instance { get; set; }
    private static BaseView _current;

    private void Awake()
    {
        Instance = this;
        foreach (var menu in _menus)
            AddMenu(menu);
    }

    // ToDo перевести это в динамику
    public static void OpenMainView(Action complete = null)
        => Open<MainView>(complete: complete);
    
    public static void OpenModeView(Action complete = null)
        => Open<ModeView>(complete: complete);
    
    public static void OpenExitView(Action complete = null)
        => Open<ExitView>(complete:  complete);
    
    public static void OpenExamGameView(ExamGameViewOptions options, Action complete = null)
        => Open<ExamGameView>(options, complete);
    
    public static void OpenTourGameView(TourGameViewOptions options, Action complete = null)
        => Open<TourGameView>(options, complete);

    public static void OpenExamGameCompleteView(ExamGameCompleteViewOptions options, Action complete = null)
        => Open<ExamGameCompleteView>(options, complete);
    
    // ToDo придумать систему без object
    private static void Open<T>(object param = null, Action complete = null) where T : BaseView
    {
        var openMenu = Menus[typeof(T)];
        var closeMenu = _current;

        void OpenComplete()
        {
            _current = openMenu;
            complete?.Invoke();
        }
        
        if (closeMenu == null)
            openMenu.Open(param, OpenComplete);
        else
            closeMenu.Close(() => openMenu.Open(param, OpenComplete));
    }

    private void AddMenu(BaseView view)
    {
        view.gameObject.SetActive(false);
        Menus.Add(view.GetType(), view);
    }
}
