using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    [SerializeField] RectTransform ShowPos, HidePos;
    Action action;
    Button button;
    
    private void OnEnable()
    {
        ShowAnimation();
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            action?.Invoke();
        });

    }

    public void SetAction(Action action)
    {
        this.action = action;
    }

    public void ShowAnimation()
    {
        this.transform.DOMoveY(ShowPos.position.y, 0.5f).SetEase(Ease.OutBounce);
    }

    public void HideAnimation()
    {
        this.transform.DOMoveY(HidePos.position.y, 0.5f).SetEase(Ease.InBounce);
    }
}
