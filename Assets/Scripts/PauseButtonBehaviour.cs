using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonBehaviour : MonoBehaviour
{
    public GameObject pausepage;
    public GameObject ingamebackground;
    void Update()
    {
        if (ingamebackground.activeSelf && Time.timeScale == 1)
        {
            gameObject.SetActive(true);
        }
        
        else
            gameObject.SetActive(false);
    }

    public void onButtonClick()
    {
        gameObject.SetActive(false);
        pausepage.SetActive(true); 
        Time.timeScale = 0;
    }
}
