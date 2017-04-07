using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LigneAnimation : MonoBehaviour
{
    public float speed;
    public GameObject[] lignes;
    public float maxX;
    public float minX;

    public void Run()
    {

    }

    public void Stop()
    {

    }

    void Update()
    {
        if (speed == 0)
            return;
        foreach (GameObject ligne in lignes)
        {
            ligne.transform.localPosition -= Vector3.right * speed * Time.deltaTime;
            if(ligne.transform.localPosition.x < minX)
                ligne.transform.localPosition += Vector3.right * (maxX - minX);
        }
    }
}
