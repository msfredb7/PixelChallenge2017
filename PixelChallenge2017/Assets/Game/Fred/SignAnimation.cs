using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CCC.Manager;

public class SignAnimation : MonoBehaviour
{
    public float speed;
    public GameObject obj;
    public float pause;
    public float maxX;
    public float minX;

    bool isDead = false;

    void Start()
    {
        MasterManager.Sync();
    }

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

        if (!isDead)
            obj.transform.localPosition -= Vector3.right * speed * Time.deltaTime;
        if (obj.transform.localPosition.x < minX && !isDead)
        {
            isDead = true;
            DelayManager.CallTo(Repawn, pause);
        }

    }

    void Repawn()
    {
        isDead = false;
        obj.transform.localPosition += Vector3.right * (maxX - minX);
    }
}
