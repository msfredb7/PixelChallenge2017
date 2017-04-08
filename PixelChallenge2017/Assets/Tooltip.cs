﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : PublicSingleton<Tooltip> {

    Text textArea;

    public void Start()
    {
        textArea = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void PrintToolTip(string inText)
    {
        gameObject.SetActive(true);
        
        textArea.text = inText;
        transform.position = Input.mousePosition;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}