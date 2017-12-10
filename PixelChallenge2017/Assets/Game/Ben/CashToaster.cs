using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CashToaster : PublicSingleton<CashToaster> {//MonoBehaviour {

    public Text prefab;


    /*
    float timer = 3;
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            SpawnAmount( Random.Range(0,100) - 50 );
            timer = 5;
        }
    }
    */

    public void SpawnAmount(int amount)
    {
        if (amount < 1)
            return;

        Text text = Instantiate(prefab.gameObject).GetComponent<Text>();
        text.transform.position = prefab.transform.position + Vector3.down * Screen.height * 0.05f;
        text.transform.SetParent(transform, true);
        text.fontSize = 45;
        text.transform.localScale = Vector3.one;
        text.text = (amount < 0 ? "" : "+" ) + amount + "$";

        text.color = amount < 0 ? Color.red : Color.green;
        // text.transform.DOMoveY(text.transform.position.y - Screen.height * 0.1f, 1).SetEase(Ease.);
        text.transform.DOMoveY(text.transform.position.y - Screen.height * 0.5f, 5).SetEase(Ease.OutQuad);
        text.DOFade(0, 0.5f).SetDelay(0.5f);
        Destroy(text.gameObject, 3);
    }
}
