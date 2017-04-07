using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragNDrop : MonoBehaviour {

    private bool dragging;
    private Plane plan;
    public float depth;



    public void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 objPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
            objPos.y = 0.1f;
            transform.position = objPos;
            gameObject.SendMessage("OnDrag");

        }
    }

    private void OnMouseDown()
    {
        dragging = true;
        gameObject.SendMessage("StartDrag");

    }

    private void OnMouseUp()
    {
        dragging = false;
        gameObject.SendMessage("EndDrag");

    }
} 
