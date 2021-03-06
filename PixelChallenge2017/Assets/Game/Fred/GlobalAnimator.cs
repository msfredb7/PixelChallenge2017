﻿using System.Collections;
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

    public Validator currentValidator;

    public class Validator
    {
        public bool cancel = false;
    }

    public UnityEvent onRestart = new UnityEvent();

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

    static private TweenCallback onContinueTrip;
    static public UnityEvent OnRestart
    {
        get { return instance.onRestart; }
    }

    static public void Restart()
    {
        PewDiePieUI.instance.shop.Close();
        instance.currentValidator.cancel = true;
        instance.Run(delegate()
        {
            GameManager.instance.car.IsRunning = true;
            if (onContinueTrip != null)
                onContinueTrip.Invoke();
            onContinueTrip = null;
            RoadManager.instance.StopEnd();
        });
        instance.onRestart.Invoke();
    }

    static public void StopAt(LieuType type, TweenCallback onContinueTrip = null, TweenCallback onStopComplete = null)
    {
        if (instance.currentValidator != null)
            instance.currentValidator.cancel = true;
        Validator localValidator = new Validator();
        instance.currentValidator = localValidator;
        print("Arret a " + type);
        RoadManager.instance.timeLastStop = Time.time;
        GameManager.instance.car.IsRunning = false;
        GlobalAnimator.onContinueTrip = onContinueTrip;
        float duration = -1;
        float delayBeforeStop = 0;
        float decelerateDuration = 3;
        bool continueButton = true;
        switch (type)
        {
            case LieuType.stationEssence:
                instance.building.Run(instance.stationEssenceSprite);
                delayBeforeStop = 0.75f;
                duration = 10f;
                break;
            case LieuType.nullePart:
                continueButton = false;
                break;
            case LieuType.depaneur:
                instance.building.Run(instance.depaneurSprite);
                delayBeforeStop = 0.75f;
                duration = 10f;
                break;
            case LieuType.costco:
                instance.building.Run(instance.costcoSprite);
                delayBeforeStop = 0.75f;
                duration = 10f;
                break;
            case LieuType.restaurant:
                instance.building.Run(instance.epicerieSprite);
                delayBeforeStop = 0.75f;
                duration = 10f;
                break;
            case LieuType.garage:
                instance.building.Run(instance.garageSprite);
                delayBeforeStop = 0.75f;
                duration = 10f;
                break;
            case LieuType.Montreal:
                instance.ville.Run(instance.montrealSprite);
                delayBeforeStop = 1.75f;
                duration = 10f;
                break;
            case LieuType.Quebec:
                instance.ville.Run(instance.quebecSprite);
                delayBeforeStop = 1.75f;
                duration = 10f;
                break;
            case LieuType.SeptIles:
                instance.ville.Run(instance.septIlesSprite);
                delayBeforeStop = 1.75f;
                duration = 10f;
                break;
            case LieuType.TroisRiviere:
                instance.ville.Run(instance.troisRiviereSprite);
                delayBeforeStop = 1.75f;
                duration = 10f;
                break;
            case LieuType.arretBus:
                instance.arretBus.Run(null);
                delayBeforeStop = 0;
                decelerateDuration = 2.5f;
                duration = 10f;
                break;
            default:
                break;
        }
        instance.Stop(delegate ()
        {
            if (onStopComplete != null)
                onStopComplete.Invoke();
            
            if (continueButton)
                PewDiePieUI.instance.continueButton.gameObject.SetActive(true);

            if(duration >= 0)
                CCC.Manager.DelayManager.CallTo(delegate ()
                {
                    if(!localValidator.cancel)
                        Restart();
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
