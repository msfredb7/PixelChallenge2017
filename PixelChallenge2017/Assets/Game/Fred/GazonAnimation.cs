using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GazonAnimation : MonoBehaviour
{
    public float speed;
    public GameObject[] renderers;
    public Sprite[] sprites;
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;

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
        foreach (GameObject renderer in renderers)
        {
            renderer.transform.localPosition -= Vector3.right * speed * Time.deltaTime;
            if (renderer.transform.localPosition.x < minX)
            {
                SpriteRenderer spr = renderer.GetComponent<SpriteRenderer>();
                if (spr != null)
                    spr.sprite = sprites[Random.Range(0, sprites.Length)];
                renderer.transform.localPosition = new Vector3(renderer.transform.position.x, renderer.transform.position.y, Random.Range(minZ, maxZ));
                renderer.transform.localPosition += Vector3.right * (maxX - minX);
            }
        }
    }
}
