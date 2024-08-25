using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonBehaviour : MonoBehaviour
{
    public void OnButtonClick()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
