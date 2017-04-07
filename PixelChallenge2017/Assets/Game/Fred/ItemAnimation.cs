using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CCC.Manager;

public class ItemAnimation : MonoBehaviour
{
    public float speed;
    public List<GameObject> items;
    public float maxX;
    public float minX;

    public GameObject toSpawnTest;

    void Start()
    {
        Instantiate(toSpawnTest,transform.position,transform.rotation);
    }

    void Update()
    {
        if (speed == 0)
            return;
        for (int i = 0; i < items.Count; i++)
        {
            GameObject item = items[i];
            item.transform.localPosition -= Vector3.right * speed * Time.deltaTime;
            if (item.transform.localPosition.x < minX)
            {
                Kill(item);
                i--;
            }
        }
    }

    void Kill(GameObject item)
    {
        items.Remove(item);
        if (item.GetComponent<Item>() != null)
            item.GetComponent<Item>().Kill();
    }
}
