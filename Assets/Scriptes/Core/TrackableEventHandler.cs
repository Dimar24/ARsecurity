using System;
using Core;
using UnityEngine;
using Vuforia;

public class TrackableEventHandler : DefaultTrackableEventHandler
{
    [SerializeField] private int _id;
    
    private Renderer[] _renderers;
    private Collider[] _colliders;
    private Canvas[] _canvases;

    public int Id => _id;
    
    public event Action<TrackableEventHandler> Found;
    public event Action<TrackableEventHandler> Lost;

    private void OnEnable()
    {
        GameManager.AddTrackableEventHandler(this);
    }


    protected override void Start()
    {
        base.Start();
        if (mTrackableBehaviour == null)
            Debug.LogError($"{nameof(TrackableEventHandler)} requires {nameof(TrackableBehaviour)}!");
        Upd();
    }
    
    protected override void OnTrackingFound()
    {
        Upd();
        EnableChildComponents(true);
        Found?.Invoke(this);
    }
    
    protected override void OnTrackingLost()
    {
        Upd();
        EnableChildComponents(false);
        Lost?.Invoke(this);
    }

    private void Upd()
    {
        _renderers = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
        _colliders = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
        _canvases = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);
        Debug.Log($"{name} {_renderers.Length} {_canvases.Length} {_colliders.Length}");
    }
    private void EnableChildComponents(bool value)
    {
        foreach (var component in _renderers)
            component.enabled = value;

        foreach (var component in _colliders)
            component.enabled = value;

        foreach (var component in _canvases)
            component.enabled = value;
    }
}