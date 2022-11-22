using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CustomPanel : MonoBehaviour
{
    [SerializeField] Text Text;

    private void OnEnable()
    {
        ShowAnimation();
    }

    void ShowAnimation()
    {
        this.transform.DOMoveY(390f, 0.5f).SetEase(Ease.OutBounce);
    }

    public void HideAnimation()
    {
        this.transform.DOMoveY(500f, 0.5f).SetEase(Ease.InBounce);
    }

    public void SetText(string text)
    {
        this.Text.text = text;
    }
}
