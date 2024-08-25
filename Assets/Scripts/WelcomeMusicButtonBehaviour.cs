using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WelcomeMusicButtonBehaviour : MonoBehaviour
{
    public AudioSource ingamesource;
    public AudioSource welcomesource;

    public GameObject welcomebutton;
    public GameObject pausebutton;
    public GameObject endbutton;
    
    public bool music = true;

    private Image buttonImage;
    void Awake()
    {
        buttonImage = gameObject.GetComponent<Image>();
    }
    
    private void OnEnable()
    {
        if (music)
            buttonImage.color = Color.green;
        
        else
            buttonImage.color = Color.red;
    }
    
    public void OnButtonClick()
    {
        if (music)
        {
            ingamesource.Pause();
            welcomesource.Pause();
            music = false;
            buttonImage.color = Color.red;
        }
        
        else
        {
            ingamesource.UnPause();
            welcomesource.UnPause();
            music = true;
            buttonImage.color = Color.green;
        }
        pausebutton.GetComponent<PauseMusicButtonBehaviour>().music = music;
        endbutton.GetComponent<EndMusicButtonBehaviour>().music = music;
    }
}
