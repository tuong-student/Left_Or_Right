using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomPanel : MonoBehaviour
{
    private void OnEnable()
    {
        this.gameObject.transform.DOMoveY(1000f, 0.5f, true).SetEase(Ease.OutQuad);
    }
}
