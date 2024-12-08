using DG.Tweening;
using System;
using UnityEngine;

public class FeelTools 
{
    public static void ScaleAppear(Transform transform, float duration = 0.5f, Ease ease = Ease.OutBack, Action callBack = null)
    {
        Vector3 lScale = transform.localScale == Vector3.zero ? Vector3.one : transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(lScale, duration).SetEase(ease).OnComplete(() => { if (callBack != null) callBack(); });
    }
    public static void ScaleDisappear(Transform transform, float duration = 0.5f, Ease ease = Ease.InBack, Action callBack = null, bool resetScale = true, bool shouldDeactivate = true)
    {
        Vector3 lScale = transform.localScale;

        transform.DOScale(0f, duration).SetEase(ease).OnComplete(() => { 
            if (resetScale) { transform.localScale = lScale; }
            if (shouldDeactivate) { transform.gameObject.SetActive(false); }
            if (callBack != null) callBack(); 
        });
    }

    public static void PlayRandomPitchedSource(ref AudioSource src, float minPitch = 0.9f, float maxPitch = 1.4f)
    {
        src.pitch = HelpersTools.GetRandomFloat(minPitch, maxPitch);
        src.Play();
    }
}
