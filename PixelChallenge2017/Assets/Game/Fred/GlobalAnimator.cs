using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


public class GlobalAnimator : Singleton<GlobalAnimator>
{
    [Header("Sprites")]
    public Sprite stationEssenceSprite;
    [Header("Animators")]
    public LigneAnimation lignes;
    public GazonAnimation gazon;
    public GazonAnimation gazonGauche;
    public SignAnimation sign;
    public BuildingAnimation building;

    void Stop(TweenCallback onComplete, float decelerateDuration = 4)
    {
        DOTween.To(() => lignes.speed, x => lignes.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
        DOTween.To(() => gazon.speed, x => gazon.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
        DOTween.To(() => gazonGauche.speed, x => gazonGauche.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
        DOTween.To(() => sign.speed, x => sign.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
        DOTween.To(() => building.speed, x => building.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine).OnComplete(onComplete);
    }

    void Run(TweenCallback onComplete)
    {
        float duration = 3;
        float speed = 45;
        DOTween.To(() => lignes.speed, x => lignes.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => gazon.speed, x => gazon.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => gazonGauche.speed, x => gazonGauche.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => sign.speed, x => sign.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => building.speed, x => building.speed = x, speed, duration).SetEase(Ease.InOutSine).OnComplete(onComplete);
    }

    static public void StopAt(LieuType type, TweenCallback onContinueTrip = null)
    {
        float duration = 0;
        Sprite spr = null;
        switch (type)
        {
            case LieuType.stationEssence:
                spr = instance.stationEssenceSprite;
                duration = 2.5f;
                break;
            case LieuType.laveAuto:
                duration = 2.5f;
                break;
            case LieuType.depaneur:
                duration = 2.5f;
                break;
            default:
                break;
        }
        instance.building.Run(spr);
        instance.Stop(delegate ()
        {
            CCC.Manager.DelayManager.CallTo(delegate ()
            {
                instance.Run(onContinueTrip);
            }, duration);
        });
    }


}
