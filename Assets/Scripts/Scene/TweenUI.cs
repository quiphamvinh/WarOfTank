using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenUI : MonoBehaviour
{
    [SerializeField] GameObject wot, ttp, tank;
    void Start()
    {
        LeanTween.scale(wot, new Vector3(1.5f, 1.5f, 1.5f), 1.5f).setDelay(.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(wot, new Vector3(0f, 270f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(wot, new Vector3(1f, 1f, 1f), 1.5f).setDelay(1.7f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(ttp, new Vector3(1f, 1f, 1f), 1.5f).setDelay(1f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(tank, new Vector3(105f, 105f, 105f), 1.5f).setDelay(3f).setEase(LeanTweenType.easeOutElastic);
    }
}
