using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimatorController : MonoBehaviour
{

    public bool scaleInOnEnable = false;

    public float scaleTime = 0f;

    private void Awake()
    {
        RectTransform thisRectTransform = this.gameObject.GetComponent<RectTransform>();
        thisRectTransform.localScale = new Vector3(0, 0, 0);
    }

    private void OnEnable()
    {
        if (scaleInOnEnable)
        {
            LeanTween.scale(this.gameObject, new Vector3(1, 1, 1) , scaleTime).setEase(LeanTweenType.linear);
        }
    }

    private void OnDisable()
    {
        if (scaleInOnEnable)
        {
            LeanTween.scale(this.gameObject, new Vector3(0, 0, 0) , scaleTime).setEase(LeanTweenType.linear);
        }
    }
}
