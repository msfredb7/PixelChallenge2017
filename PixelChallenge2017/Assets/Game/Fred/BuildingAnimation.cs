using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildingAnimation : MonoBehaviour
{
    public float speed;
    public GameObject holder;
    public SpriteRenderer pannelRenderer;
    public float maxX;
    public float minX;
    public float centerX;

    bool isRunning = true;

    public void Run(Sprite pannelImage)
    {
        if (pannelImage != null)
            pannelRenderer.sprite = pannelImage;
        Vector3 pos = holder.transform.position;
        holder.transform.position = new Vector3(maxX, pos.y, pos.z);
        isRunning = true;
    }

    void Update()
    {
        if (speed == 0 && isRunning)
            return;

        if (holder.transform.localPosition.x > minX)
            holder.transform.localPosition -= Vector3.right * speed * Time.deltaTime;
        else
            isRunning = false;
    }
}
