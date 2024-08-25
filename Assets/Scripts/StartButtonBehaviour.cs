 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startbuttonbehaviour : MonoBehaviour
{

    public GameObject emirg;
    public GameObject ingamebackground;
    public GameObject welcomepage;
    public GameObject backgroundcanvas;

    private GameObject scoretext;
    
    public void OnButtonClick()
    {
        if (emirg != null)
        {
            emirg.SetActive(true);
            emirg.transform.position = new Vector3(-12f, 0f, 0f);
        }
        
        ingamebackground.SetActive(!ingamebackground.activeSelf);
        backgroundcanvas.SetActive(true);
        welcomepage.SetActive(false);
    }
}
