using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayAgainBehaviour : MonoBehaviour
{
    public GameObject emirg;
    private MortalLogic emirghealth;
    private emirgControls emirgController;
    
    public GameObject ingamebackground;
    public GameObject jordan1spawner;
    public GameObject endpage;
    public GameObject backgroundcanvas;
    public GameObject canvas;
    public GameObject youwontext;
    public GameObject youlosetext;
    public GameObject shootbar;

    private TextMeshProUGUI healthText;
    private Text scoreText;

    
    void Start()
    {
        scoreText = backgroundcanvas.transform.Find("ScoreText").GetComponentInChildren<Text>();
        healthText = canvas.transform.Find("HealthText").GetComponentInChildren<TextMeshProUGUI>();

        emirghealth = emirg.GetComponent<MortalLogic>();
        emirgController = emirg.GetComponent<emirgControls>();
    }
    public void onButtonClick()
    {
        if (GameObject.FindWithTag("Boss") == null && GameObject.FindWithTag("Enemy") == null) // there should be no enemy
        {
            youlosetext.SetActive(false);
            youwontext.SetActive(false);
           
            GameObject.FindWithTag("Music").GetComponent<MusicController>().ResetMusic();
            
            // resetting emirg attributes
            emirg.SetActive(true);
            emirg.transform.position = new Vector3(-12f, 0f, 0f);
            emirg.GetComponent<emirgControls>().scoreOfEmirg = 0;
            emirg.GetComponent<emirgControls>().velocityOfFly = 5;
            emirg.GetComponent<MortalLogic>().health = 5;
            emirg.GetComponent<emirgControls>().endfly = 0;
            emirgController.StartCoroutine(emirgController.spawn());

            shootingBehavior emirgShooting = emirg.GetComponentInChildren<shootingBehavior>();
            emirgShooting.GetComponent<shootingBehavior>().minshootRate = 1.5f;
            emirgShooting.GetComponent<shootingBehavior>().maxshootRate = 1.5f;
            emirgShooting.GetComponent<shootingBehavior>().timer = 0;
            
            jordan1spawner.SetActive(true);
            ingamebackground.SetActive(true);
            backgroundcanvas.SetActive(true);
            healthText.GameObject().SetActive(true);
            healthText.text = $"<sprite name=\"emirghead\">{emirghealth.health}x";
            scoreText.text = "SCORE:0";

            
            shootbar.transform.localScale = new Vector3(0, shootbar.transform.localScale.y, shootbar.transform.localScale.z);

            endpage.SetActive(false);

        }
    }
}
