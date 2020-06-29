using System;
using System.Linq;
using Lazy;
using Lazy.Generic;
using Subsystem.Question;
using UI.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] private int[] _sceneIds;

    private void Awake()
    {
#if UNITY_EDITOR
        var normalizeSceneIds = _sceneIds.Distinct().ToArray();
        if (normalizeSceneIds.Length != _sceneIds.Length)
        {
            Debug.LogError($"Only unique values can be used [{nameof(_sceneIds)}]");
            _sceneIds = normalizeSceneIds;
        }
#endif
    }

    private void Start()
    {
        var loadPipeline =  new LazySequence();

        foreach (var id in _sceneIds)
            loadPipeline
                .Join(LoadSceneLazy(id));
        
        loadPipeline
            .Append(() => MenuManager.Open<MainMenu>())
            .Join(LoadQuestionsLazy())
            .Append(UnLoadSplashLazy())
            .Run();
    }

    private BaseLazy LoadQuestionsLazy() => 
        new LazyCallback(QuestionManager.LoadQuestionsAsync);

    private BaseLazy UnLoadSplashLazy() => 
        new LazyCoroutine(() => SceneManager.UnloadSceneAsync(gameObject.scene.buildIndex));

    private BaseLazy LoadSceneLazy(int id) =>
        new LazyCoroutine(() => SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive));
}
