using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomButton : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.DOMoveY(200f, 0.5f).SetEase(Ease.OutQuad);
    }
}
