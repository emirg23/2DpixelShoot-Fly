using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossControls : MonoBehaviour
{
    public GameObject BarPrefab;
    
    public float Speed = 1;
    
    private GameObject emirg;
    private GameObject YouWonText;
    private GameObject BackgroundCanvas;
    private GameObject SpawnedBar;
    
    private SpriteRenderer HealthBar;
    private MortalLogic BossHealth;
    private Text ScoreText;
    
    private float Timer = 0;
    private float ScoreValue = 500;

    private bool BarSpawned = false;
    
    void Start()
    {
        emirg = GameObject.FindWithTag("emirg");
        BackgroundCanvas = GameObject.FindWithTag("BackgroundCanvas");

        BossHealth = gameObject.GetComponent<MortalLogic>();
        
        ScoreText = BackgroundCanvas.transform.Find("ScoreText").GetComponentInChildren<Text>();
        YouWonText = BackgroundCanvas.transform.Find("YouWon").gameObject;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        
        if (BarSpawned) // check green part of the bar
        {
            HealthBar.transform.localScale = new Vector3(
                BossHealth.health / 400f * 0.9f,
                HealthBar.transform.localScale.y,
                HealthBar.transform.localScale.z
            );
        }

        if (emirg.activeSelf)
            fly(); // till a certain point

        else // make boss go away as emirg dies
        {
            Speed += Time.deltaTime;
            gameObject.transform.position += Vector3.left * (Speed * Time.deltaTime);
            
            if(gameObject.transform.position.x < -14)
                Destroy(gameObject);
        }
    }

    public void fly()
    {
        if (gameObject.transform.position.x >= 4.6f) // stop at x = 4.6 
            gameObject.transform.position += Vector3.left * (Speed * Time.deltaTime);

        else if (!BarSpawned) // spawning the bar
        {
            BarSpawned = true;

            SpawnedBar = Instantiate(BarPrefab, new Vector3(-4.2388f, 4.8334f, 0), Quaternion.identity);
            HealthBar = SpawnedBar.transform.Find("Health").GetComponent<SpriteRenderer>();
        }
    }
    private void OnDestroy()
    {
        if (emirg.activeSelf && emirg.transform.position.x > -11)
        {
            ActivateText(); // you won
            
            ScoreValue = ScoreValue / (Timer / 50) + 300; // 50 seconds, 800. 100 seconds, 550.
            
            emirg.GetComponent<emirgControls>().scoreOfEmirg += (int)ScoreValue;
            
            ScoreText.text = "SCORE:" + emirg.GetComponent<emirgControls>().scoreOfEmirg;
        }
        
        Destroy(SpawnedBar);
    }
    void ActivateText()
    {
        if (YouWonText != null)
            YouWonText.GetComponent<TextBehaviour>().ActivateText();
    }
}