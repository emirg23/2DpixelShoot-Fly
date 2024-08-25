using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shootingBehavior : MonoBehaviour
{
    public GameObject bullet;

    public float minshootRate;
    public float maxshootRate;
    public float timer;
    
    public bool controls;
    
    private float shootRate;
    private bool autoShoot;
    void Start()
    {
        shootRate = Random.Range(minshootRate, maxshootRate);
    }

    void Update()
    {
        if (timer < shootRate)
            timer += Time.deltaTime;
        
        else
        {
            if (controls) // only emirg
            {
                if (Time.timeScale == 1 && (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space)))
                {
                    Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
                    
                    timer = 0;
                    shootRate = Random.Range(minshootRate, maxshootRate);
                }
            }
            
            else // enemies
            {
                Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
                
                timer = 0;
                shootRate = Random.Range(minshootRate, maxshootRate);
            }
        }
    }
}
