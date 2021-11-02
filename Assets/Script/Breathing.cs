using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing : MonoBehaviour
{
    [SerializeField] float startScale = 1f;
    void Breathe()
    {
        transform.DOScale(startScale + 0.1f, 1f).SetEase(Ease.InOutCubic).onComplete += () => { DeBreathe(); };
    }

    void DeBreathe()
    {
        transform.DOScale(startScale - 0.1f, 1f).SetEase(Ease.InOutCubic).onComplete += () => { Breathe(); };
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one * (startScale - 0.1f);
        Breathe();
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
