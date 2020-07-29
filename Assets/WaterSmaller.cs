using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaterSmaller : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _distance = 1f;

    private float _start;
    
    private void Awake()
    {
        _start = transform.localScale.z;
    }

    private void OnEnable()
    {
        if (Math.Abs(_distance) < 0.001f)
            return;
        
        var currDist = transform.localScale.z - _start;
        var value = 1f - currDist / _distance;
        //transform.DOScaleZ(_start - _distance, _duration * value); //(1,1,1);//(_start + _distance, _duration * value);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}