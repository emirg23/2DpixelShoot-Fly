using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public class emirgControls : MonoBehaviour
{
    public GameObject ingamebackground;
    
    public GameObject pausepage;
    public GameObject pausebutton;
    
    public GameObject youlose;
    public GameObject youwon;
    public GameObject endpage;

    public float velocityOfFly = 5f;
    public float endfly;
    
    public int scoreOfEmirg;
    
    private bool jordan4spawned = false;

    private float rightBorder = 7;
    private float leftBorder = -7.4f;
    private float upBorder = 3.9f;
    private float bottomBorder = -3.0f;

    private GameObject jordan4;
    private GameObject canvas;
    
    private shootingBehavior emirgShooting;
    private MortalLogic mortalLogic;

    private TextMeshProUGUI healthText;

    private GameObject ShootBar;
    private float ShootTimeLeft;
    private float ShootCooldown;
    void Start()
    {
        StartCoroutine(spawn());
        
        canvas = GameObject.FindWithTag("Canvas");
        
        mortalLogic = gameObject.GetComponent<MortalLogic>();
        
        healthText = canvas.transform.Find("HealthText").GetComponentInChildren<TextMeshProUGUI>();
        
        if (gameObject != null)
        {
            emirgShooting = gameObject.GetComponentInChildren<shootingBehavior>();
            
            if(emirgShooting != null) 
                emirgShooting.controls = true;
        }

        ShootBar = GameObject.FindWithTag("ShootBar");
        ShootBar.transform.localScale =
            new Vector3(0, ShootBar.transform.localScale.y, ShootBar.transform.localScale.y);
        
        ShootTimeLeft = ShootCooldown - emirgShooting.timer;
        
        ShootCooldown = emirgShooting.maxshootRate;
    }
    
    void Update()
    {
        ShootTimeLeft = ShootCooldown - emirgShooting.timer;
        
        ShootCooldown = emirgShooting.maxshootRate;
        
        ShootBar.transform.localScale = new Vector3(Mathf.Clamp01((ShootCooldown - ShootTimeLeft) / ShootCooldown), ShootBar.transform.localScale.y, ShootBar.transform.localScale.y);

        ShootCooldown = emirgShooting.maxshootRate;

        if (Input.GetKeyDown(KeyCode.Escape) && !youlose.activeSelf && !youwon.activeSelf)
        {
            if (Time.timeScale == 1)
            {
                pausepage.SetActive(true);   
                Time.timeScale = 0;
            }
            
            else
            {
                pausepage.SetActive(false);
                Time.timeScale = 1;
            }
        }
        
        if(ingamebackground.activeSelf && Time.timeScale == 1)
            pausebutton.SetActive(true);
        
        if (Input.GetKeyDown(KeyCode.F))
            gameObject.GetComponentInChildren<shootingBehavior>().controls =
                !gameObject.GetComponentInChildren<shootingBehavior>().controls;
        
        fly();
            
        if (gameObject.transform.position.x < -11f)
            jordan4spawned = false;
        
        jordan4 = GameObject.FindWithTag("Boss");

        if(jordan4 != null) 
            jordan4spawned = true;

        if (jordan4spawned == true && jordan4 == null) // jordan4 got killed
        {
            healthText.gameObject.SetActive(false);
            
            endpage.SetActive(true);
            ingamebackground.SetActive(false);
                
            endfly += velocityOfFly * Time.deltaTime * 2; // going right to infinity as emirg wins
            gameObject.transform.position += Vector3.right * (endfly * Time.deltaTime);

            if (gameObject.transform.position.x > 12.2f)
                gameObject.transform.position = new Vector3(12.2f, 0f, 0f); // stay still out of the screen
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Boost")) // effects of boosts
        {
            if (collision.collider.name == "2xAS(Clone)")
            {
                emirgShooting.maxshootRate /= 1.25f;
                emirgShooting.minshootRate /= 1.25f; 
            }

            else if (collision.collider.name == "+HP(Clone)")
            {
                mortalLogic.health++;
                healthText.text = $"<sprite name=\"emirghead\">{mortalLogic.health}x";
            }
            
            else if (collision.collider.name == "SPEED(Clone)")
                velocityOfFly += 2;
        }
        

        if (!collision.collider.CompareTag("emirgBullet") && !collision.collider.CompareTag("Boss")) // on collision destroy collider (enemybullet,boost)-except boss-
        {
            Destroy(collision.collider.gameObject);
        }

        else if (collision.collider.CompareTag("Boss")) // if colliding with boss emirg gets killed
        {
            mortalLogic.health = 0;
            endpage.SetActive(true);
        }
    }
    
    public void fly()
    {
        if (((Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))) && transform.position.y < upBorder)
            transform.position += Vector3.up * (velocityOfFly * Time.deltaTime); // flying up

        if (!(jordan4spawned == true && jordan4 == null) && ((Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))) && transform.position.x > leftBorder)
            transform.position += Vector3.left * (velocityOfFly * Time.deltaTime); // flying left
            // jordan condition is for when jordan4 gets killed, we gotta go straight to right.
        
        if (((Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))) && transform.position.y > bottomBorder)
            transform.position += Vector3.down * (velocityOfFly * Time.deltaTime); // flying down
        
        if (((Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))) && transform.position.x < rightBorder)
            transform.position += Vector3.right * (velocityOfFly * Time.deltaTime); // flying right
    }
    
    void ActivateText()
    {
        if (youlose != null)
            youlose.GetComponent<TextBehaviour>().ActivateText();
    }

    public IEnumerator spawn()
    {
        gameObject.SetActive(true);
        
        Vector3 startPosition = new Vector3(-12f, 0f, 0f);
        Vector3 endPosition = new Vector3(-5f, 0f, 0f);

        transform.position = startPosition;

        float timer = 0;

        while (timer < 3)
        {
            if(gameObject.transform.position.x < 6)
                transform.position += 2.5f * Vector3.right * Time.deltaTime;
            
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
