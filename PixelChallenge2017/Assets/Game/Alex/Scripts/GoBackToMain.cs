using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToMain : MonoBehaviour {
    
	void Start ()
    {
        DelayManager.CallTo(delegate ()
        {
            SceneManager.LoadScene("Main");
        },3);
	}
}
