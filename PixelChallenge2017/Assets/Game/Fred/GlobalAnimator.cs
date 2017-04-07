using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


public class GlobalAnimator : Singleton<GlobalAnimator>
{
    [Header("Sprites")]
    public Sprite stationEssenceSprite;
    public Sprite depaneurSprite;
    public Sprite epicerieSprite;
    public Sprite garageSprite;
    public Sprite costcoSprite;
    public Sprite montrealSprite;
    public Sprite quebecSprite;
    public Sprite troisRiviereSprite;
    public Sprite septIlesSprite;
    [Header("Animators")]
    public LigneAnimation lignes;
    public GazonAnimation gazon;
    public GazonAnimation gazonGauche;
    public SignAnimation sign;
    public BuildingAnimation building;
    public BuildingAnimation ville;
    public BuildingAnimation arretBus;
    public ItemAnimation items;

    void Stop(TweenCallback onComplete, float decelerateDuration = 3, float delay = 0)
    {
        CCC.Manager.DelayManager.CallTo(delegate ()
        {
            DOTween.To(() => lignes.speed, x => lignes.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
            DOTween.To(() => gazon.speed, x => gazon.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
            DOTween.To(() => gazonGauche.speed, x => gazonGauche.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
            DOTween.To(() => sign.speed, x => sign.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
            DOTween.To(() => building.speed, x => building.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
            DOTween.To(() => arretBus.speed, x => arretBus.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
            DOTween.To(() => items.speed, x => items.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine);
            DOTween.To(() => ville.speed, x => ville.speed = x, 0, decelerateDuration).SetEase(Ease.InOutSine).OnComplete(onComplete);
        }, delay);
    }

    void Run(TweenCallback onComplete)
    {
        float duration = 3;
        float speed = 45;
        DOTween.To(() => lignes.speed, x => lignes.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => gazon.speed, x => gazon.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => gazonGauche.speed, x => gazonGauche.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => sign.speed, x => sign.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => building.speed, x => building.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => arretBus.speed, x => arretBus.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => items.speed, x => items.speed = x, speed, duration).SetEase(Ease.InOutSine);
        DOTween.To(() => ville.speed, x => ville.speed = x, speed, duration).SetEase(Ease.InOutSine).OnComplete(onComplete);
    }

    static public void StopAt(LieuType type, TweenCallback onContinueTrip = null)
    {
        float duration = 0;
        float delayBeforeStop = 0;
        float decelerateDuration = 3;
        switch (type)
        {
            case LieuType.stationEssence:
                duration = 2.5f;
                instance.building.Run(instance.stationEssenceSprite);
                delayBeforeStop = 0.75f;
                break;
            case LieuType.laveAuto:
                duration = 2.5f;
                instance.building.Run(instance.stationEssenceSprite);
                delayBeforeStop = 0.75f;
                break;
            case LieuType.depaneur:
                duration = 2.5f;
                instance.building.Run(instance.depaneurSprite);
                delayBeforeStop = 0.75f;
                break;
            case LieuType.costco:
                duration = 2.5f;
                instance.building.Run(instance.costcoSprite);
                delayBeforeStop = 0.75f;
                break;
            case LieuType.restaurant:
                duration = 2.5f;
                instance.building.Run(instance.epicerieSprite);
                delayBeforeStop = 0.75f;
                break;
            case LieuType.garage:
                duration = 2.5f;
                instance.building.Run(instance.garageSprite);
                delayBeforeStop = 0.75f;
                break;
            case LieuType.Montreal:
                duration = 2.5f;
                instance.ville.Run(instance.montrealSprite);
                delayBeforeStop = 1.75f;
                break;
            case LieuType.Quebec:
                duration = 2.5f;
                instance.ville.Run(instance.quebecSprite);
                delayBeforeStop = 1.75f;
                break;
            case LieuType.SeptIles:
                duration = 2.5f;
                instance.ville.Run(instance.septIlesSprite);
                delayBeforeStop = 1.75f;
                break;
            case LieuType.TroisRiviere:
                duration = 2.5f;
                instance.ville.Run(instance.troisRiviereSprite);
                delayBeforeStop = 1.75f;
                break;
            case LieuType.arretBus:
                duration = 2.5f;
                instance.arretBus.Run(null);
                delayBeforeStop = 0;
                decelerateDuration = 2.5f;
                break;
            default:
                break;
        }
        instance.Stop(delegate ()
        {
            CCC.Manager.DelayManager.CallTo(delegate ()
            {
                instance.Run(onContinueTrip);
            }, duration);
        }, decelerateDuration, delayBeforeStop);
    }

    static public void AddFloatingItem(GameObject item)
    {
        if (!instance.items.items.Contains(item))
            instance.items.items.Add(item);
    }
    static public void RemoveFloatingItem(GameObject item)
    {
        instance.items.items.Remove(item);
    }


}
