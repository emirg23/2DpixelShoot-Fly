using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BackgroundSpeed : MonoBehaviour
{
    public GameObject emirg;
    public GameObject YouWonText;
    public GameObject YouLoseText;
    public TextMeshProUGUI HighestScore;

    public float backgroundSpeed = 0;
    
    private float Timer;
    private List<int> Scores = new List<int>();

    void Start()
    {
        if(emirg== null)
            emirg = GameObject.FindWithTag("emirg");
    }

    void Update()
    {
        if (emirg != null && emirg.activeSelf && emirg.transform.position.x < 12f)
        {
            if (backgroundSpeed < 4) // max speed is 4 for background objects
                backgroundSpeed += 0.5f * Time.deltaTime;
        }
        
        else if(backgroundSpeed > 0)
        {
            if (Timer < 0.1f)
                Timer += Time.deltaTime;
            
            else
            {
                if(backgroundSpeed > 0) // slowing down
                    backgroundSpeed /= 1.1f;
              
                if (backgroundSpeed < 0.05f) // close enough to 0 so stopping
                    backgroundSpeed = 0;
               
                Timer = 0;
            }
        }
        
        // checking highscore since BackgroundSpeed only script that exist from start of the game till the exit
        if ((YouLoseText.activeSelf || YouWonText.activeSelf) && !Scores.Contains(emirg.GetComponent<emirgControls>().scoreOfEmirg))
        {
            Scores.Add(emirg.GetComponent<emirgControls>().scoreOfEmirg);
            
            if(Scores.Max() == 0)
                HighestScore.text = "HIGHEST SCORE:"+Scores.Max();
            
            else
                HighestScore.text = "HIGHEST SCORE:"+Scores.Max()+"!";  
        }
    }
}
