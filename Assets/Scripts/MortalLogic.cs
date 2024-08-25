using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class MortalLogic : MonoBehaviour
{
    public GameObject emirg;

    public int health;
    public string bulletOfMortal;
    
    private MortalLogic mortalLogic;
    
    public GameObject youwontext;
    public GameObject youlosetext;
    public GameObject ingamebackground;
    public GameObject endpage;

    private TextMeshProUGUI healthText;
    void Start()
    {
        GameObject canvas = GameObject.FindWithTag("Canvas");
        
        healthText = canvas.transform.Find("HealthText").GetComponentInChildren<TextMeshProUGUI>();
        
        if(emirg!= null)
            mortalLogic = emirg.GetComponent<MortalLogic>();
        
    }
    void Update()
    {
        
        if (health <= 0)
        {
            if (gameObject == emirg)
            {
                emirg.SetActive(false);
                
                ActivateText(); // you lose
        
                ingamebackground.SetActive(false);
                endpage.SetActive(true);
                gameObject.SetActive(false);
            }
            
            else
                Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // emirg taking damage
        if (!collision.collider.CompareTag(bulletOfMortal) && !(collision.collider.CompareTag("Boost"))&& !collision.collider.CompareTag("emirg") && !collision.collider.CompareTag("Boss"))
        {  
            Destroy(collision.collider.gameObject);
            
            // only if game is continuing
            if(youwontext!=null && youwontext.activeSelf)
                return;
                
            health--;
            
            if(gameObject == emirg) 
                healthText.text = $"<sprite name=\"emirghead\">{mortalLogic.health}x";
        }
    }
    void ActivateText()
    {
        if (youlosetext != null)
        {
            youlosetext.GetComponent<TextBehaviour>().ActivateText();
        }
    }
}