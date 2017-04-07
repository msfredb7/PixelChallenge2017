using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragNDrop : MonoBehaviour {

    public float depth;
    public Item item;

    private bool dragging;
    private Vector3 objPos;
    private bool onCase;
    private int maxShapeSize = 7;

    private void Start()
    {
        maxShapeSize = ArrayLayout.
    }



    // Update is called once per frame
    void Update()
    {
        if (dragging)
        { 
            int layer = 1 << 8;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10.0f, layer))
            {
                testPosition(hit);
            }

            else
            {
                Vector2 mousePos = Input.mousePosition;
                objPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
            }
            transform.position = objPos;
        }
    }


    public void testPosition(RaycastHit hit)
    {
        Case hitCase = hit.transform.GetComponent<Case>();

        


        objPos = hit.transform.position;
        objPos.y += 0.1f;

    }



    public void OnMouseDown()
    {
        dragging = true;
        objPos = transform.position;
    }

    public void OnMouseUp()
    {
        dragging = false;
        transform.position = objPos;
    }



  




}
 
