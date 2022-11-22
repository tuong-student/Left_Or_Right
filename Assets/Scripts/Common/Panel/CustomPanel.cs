using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CustomPanel : MonoBehaviour
{
    [SerializeField] RectTransform ShowPos, HidePos;
    [SerializeField] Text Text;
    RectTransform parentRect;

    private void OnEnable()
    {
        parentRect = this.transform.parent.GetComponent<RectTransform>();
        ShowAnimation();
    }

    void ShowAnimation()
    {
        this.transform.DOMoveY(ShowPos.position.y, 0.5f).SetEase(Ease.OutBounce);
    }

    public void HideAnimation()
    {
        this.transform.DOMoveY(HidePos.position.y, 0.5f).SetEase(Ease.InBounce);
    }

    public void SetText(string text)
    {
        this.Text.text = text;
    }
}
