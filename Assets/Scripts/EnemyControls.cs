using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyControls : MonoBehaviour
{
    
    public float speedGoingToEmirg = 1;
    public float speedEnteringTheGame = 2;
    public float speedDodgingTheBullets = 2;

    public float targetArea = -10;
    
    public bool goingBottom;
    
    private GameObject emirg;
    private emirgControls emirgControls;
    private GameObject canvas;
    private GameObject backgroundcanvas;
    private SpriteRenderer healthbar;
    private MortalLogic jordanHealth;
    private SpriteRenderer bar;
    
    public GameObject AS;
    public GameObject HP;
    public GameObject SPEED;
    
    private Text scoreText;
    private TextMeshProUGUI healthText;
    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
        backgroundcanvas = GameObject.FindWithTag("BackgroundCanvas");

        healthText = canvas.transform.Find("HealthText").GetComponentInChildren<TextMeshProUGUI>();
        scoreText = backgroundcanvas.transform.Find("ScoreText").GetComponentInChildren<Text>();

        bar = gameObject.transform.Find("Bar").GetComponentInChildren<SpriteRenderer>();
        healthbar = bar.transform.Find("Health").GetComponentInChildren<SpriteRenderer>();

        jordanHealth = gameObject.GetComponent<MortalLogic>();

        emirg = GameObject.FindWithTag("emirg");
        emirgControls = emirg.GetComponent<emirgControls>();
    
        // Randomly determine the initial direction
        int booleanNumber = Random.Range(0, 2);
        goingBottom = booleanNumber == 1;
    }

    void Update()
    {
        // check green part of the bar
        
        healthbar.transform.localScale = new Vector3(jordanHealth.health/2f*0.9f, healthbar.transform.localScale.y, healthbar.transform.localScale.y);
        
        if (!emirg.activeSelf) // go straight left if emirg is dead
        {
            speedGoingToEmirg += 10f * Time.deltaTime;
            speedDodgingTheBullets = 0;
        }

        else if (emirgControls.scoreOfEmirg >= 200) // get even faster after 200 scores
        {
            speedGoingToEmirg = 1.5f;
            speedDodgingTheBullets = 2.5f;
        }
        
        else if (emirgControls.scoreOfEmirg >= 100) // get faster after 100 scores
        {
            speedGoingToEmirg = 1.25f;
            speedDodgingTheBullets = 2.25f;
        }
        
        fly();
        
        if (gameObject.transform.position.x < targetArea) 
        { // if jordan1 reaches deadzone, emirg gets physical contact damage
            Destroy(gameObject);
            
            if (emirg != null)
            {
                MortalLogic emirgLogic = emirg.GetComponent<MortalLogic>();
                
                emirgLogic.health--;
                healthText.text = $"<sprite name=\"emirghead\">{emirgLogic.health}x";
            }
        }
            
        if (gameObject.transform.position.y < -3.7) 
            goingBottom = false;

        else if (gameObject.transform.position.y > 4.4)
            goingBottom = true;
    }

    private void OnDestroy()
    {
        if (emirg.transform.position.x > -11f) // not working if emirg is not in game
        {
            // chances of dropping boost
            int chance = Random.Range(1, 101);

            if (chance <= 20) // %20 chance of attack speed 
                Instantiate(AS, transform.position, transform.rotation);

            else
            {
                chance = Random.Range(1, 101);

                if (chance <= 20) // %20 chance of HP if attack speed did not spawn
                    Instantiate(HP, transform.position, transform.rotation);

                else
                {
                    chance = Random.Range(1, 101);

                    if (chance <= 10) // %10 chance of SPEED if attack speed and HP did not spawn
                        Instantiate(SPEED, transform.position, transform.rotation);
                }
            }

            if (gameObject.transform.position.x > -10 && emirg.activeSelf) // if jordan1 didn't get killed by deadzone
            {
                emirg.GetComponent<emirgControls>().scoreOfEmirg += 5;
                scoreText.text = "SCORE:" + emirg.GetComponent<emirgControls>().scoreOfEmirg;
            }
        }
    }

    public void fly()
    {
        if(gameObject.transform.position.x > 11) 
            flySpeed(Vector3.left, speedEnteringTheGame);
        
        else
        {
            flySpeed(Vector3.left, speedGoingToEmirg);

            if(goingBottom)
                flySpeed(Vector3.down, speedDodgingTheBullets);
            
            else
                flySpeed(Vector3.up, speedDodgingTheBullets);
        }
    }

    public void flySpeed(Vector3 direction,float speed)
    {
        gameObject.transform.position += direction * (speed * Time.deltaTime);
    }
}
