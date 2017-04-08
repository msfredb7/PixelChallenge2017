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

    void Update()
    {
        if (speed == 0)
            return;

        if(Input.GetKeyDown("a"))
        {
            Instantiate(items[0], transform.position, transform.rotation);
        }

        for (int i = 0; i < items.Count; i++)
        {
            GameObject item = items[i];
            item.transform.localPosition -= Vector3.right * speed * Time.deltaTime;
            if (item.transform.position.x < minX)
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
