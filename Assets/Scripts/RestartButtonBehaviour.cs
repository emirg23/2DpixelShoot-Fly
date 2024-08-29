using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class RestartButtonBehaviour : MonoBehaviour
{
    public GameObject backgroundcanvas;
    public GameObject canvas;
    public GameObject ingamebackground;
    public GameObject jordan1spawner;
    public GameObject pausepage;
    public GameObject shootbar;

    public AudioSource music;
    
    private GameObject emirg;
    private emirgControls emirgController;
    private MortalLogic emirghealth;
    
    private TextMeshProUGUI healthText;
    private Text scoreText;
    
    void Start()
    {
        emirg = GameObject.FindWithTag("emirg");
        emirgController = emirg.GetComponent<emirgControls>();
        emirghealth = emirg.GetComponent<MortalLogic>();
        
        scoreText = backgroundcanvas.transform.Find("ScoreText").GetComponentInChildren<Text>();
        healthText = canvas.transform.Find("HealthText").GetComponentInChildren<TextMeshProUGUI>();
    }
    public void onButtonClick()
    {
        emirg.SetActive(false);
        // destroying everything for a new game
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        GameObject[] boosts = GameObject.FindGameObjectsWithTag("Boost");
        GameObject[] enemybullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        GameObject[] bossbullets = GameObject.FindGameObjectsWithTag("BossBullet");
        GameObject[] emirgbullets = GameObject.FindGameObjectsWithTag("emirgBullet");

        GameObject[] allObjects = new GameObject[enemies.Length + bosses.Length + boosts.Length + enemybullets.Length + bossbullets.Length + emirgbullets.Length];

        enemies.CopyTo(allObjects, 0);
        bosses.CopyTo(allObjects, enemies.Length);
        boosts.CopyTo(allObjects, enemies.Length + bosses.Length);
        enemybullets.CopyTo(allObjects, enemies.Length + bosses.Length + boosts.Length);
        bossbullets.CopyTo(allObjects, enemies.Length + bosses.Length + boosts.Length + enemybullets.Length);
        emirgbullets.CopyTo(allObjects, enemies.Length + bosses.Length + boosts.Length + enemybullets.Length + bossbullets.Length);

        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }
        
        music.GetComponent<MusicController>().ResetMusic();

        GameObject.FindWithTag("BackgroundSpeed").GetComponent<BackgroundSpeed>().backgroundSpeed = 0;
        
        shootingBehavior emirgShooting = emirg.GetComponentInChildren<shootingBehavior>();
        emirgShooting.GetComponent<shootingBehavior>().minshootRate = 1.5f;
        emirgShooting.GetComponent<shootingBehavior>().maxshootRate = 1.5f;
        emirgShooting.GetComponent<shootingBehavior>().timer = 0;

        // resetting emirg attributes
        emirg.transform.position = new Vector3(-12f, 0f, 0f);
        emirg.GetComponent<emirgControls>().scoreOfEmirg = 0;
        emirg.GetComponent<emirgControls>().velocityOfFly = 5;
        emirg.GetComponent<MortalLogic>().health = 5;
        emirg.GetComponent<emirgControls>().endfly = 0;
        emirg.SetActive(true);
        emirgController.StartCoroutine(emirgController.spawn());
        
        jordan1spawner.SetActive(true);
        ingamebackground.SetActive(true);
        backgroundcanvas.SetActive(true);
        healthText.text = $"<sprite name=\"emirghead\">{emirghealth.health}x";
        scoreText.text = "SCORE:0";

        shootbar.transform.localScale = new Vector3(0, shootbar.transform.localScale.y, shootbar.transform.localScale.z);

        Time.timeScale = 1;
        pausepage.SetActive(false);
    }
}
