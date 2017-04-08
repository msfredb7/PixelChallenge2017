using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Intro : MonoBehaviour {

    public Camera cam;
    public Vector3 camOriginPos;
    public CanvasGroup canvasGroup;
    public float camOriginSize;
    public Image blackForground;
    public SpriteRenderer cappot;
    public Text uiText;
    public float textDuration = 3;
    public string[] texts;

    public bool skipIntro = false;
    
	void Start () {
        if (skipIntro)
        {
            cappot.gameObject.SetActive(false);
            blackForground.gameObject.SetActive(false);
            RoadManager.instance.start = true;
        }
        else
        {
            float camTargetSize = cam.orthographicSize;
            Vector3 camTargetPos = cam.transform.position;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            blackForground.gameObject.SetActive(true);

            cam.transform.position = camOriginPos;
            cam.orthographicSize = camOriginSize;

            Sequence seq = DOTween.Sequence();
            uiText.gameObject.SetActive(true);
            uiText.color = new Color(0, 0, 0, 0);
            blackForground.color = new Color(0, 0, 0, 1);

            seq.Append(blackForground.DOFade(0, 2));
            seq.AppendInterval(0.5f);
            foreach (string text in texts)
            {
                seq.AppendCallback(delegate ()
                {
                    uiText.text = text;
                });
                seq.Append(uiText.DOFade(1, 0.75f));
                seq.AppendInterval(textDuration);
                seq.Append(uiText.DOFade(0, 0.75f));
            }
            seq.AppendInterval(1);
            seq.Append(cam.DOOrthoSize(camTargetSize, 4).SetEase(Ease.InOutSine));
            seq.Join(cam.transform.DOMove(camTargetPos, 4).SetEase(Ease.InOutSine));
            seq.AppendCallback(delegate ()
            {
                canvasGroup.interactable = true;
                uiText.gameObject.SetActive(false);
            });
            seq.Append(cappot.DOFade(0, 1.5f));
            seq.Append(canvasGroup.DOFade(1, 1.5f));
            seq.AppendCallback(delegate ()
            {
                cappot.gameObject.SetActive(false);
                blackForground.gameObject.SetActive(false);
                RoadManager.instance.start = true;
            });
        }
    }
}
