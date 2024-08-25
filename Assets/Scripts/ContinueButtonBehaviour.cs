using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButtonBehaviour : MonoBehaviour
{
    public GameObject pausepage;
    public void onButtonClick()
    {
        Time.timeScale = 1;
        pausepage.SetActive(false);
    }
}
