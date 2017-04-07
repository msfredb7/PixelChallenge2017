using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragNDrop : MonoBehaviour {

    private bool dragging;
    private Plane plan;
    public float depth;

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 objPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
            transform.position = objPos;
        }
    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
} 
