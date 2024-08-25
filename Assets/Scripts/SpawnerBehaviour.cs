using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerBehavior : MonoBehaviour
{
    public float firstSpawn;
    public float spawnRate = 3;

    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;

    public GameObject spawningObject;
    public GameObject jordan4;

    private GameObject emirg;
    private emirgControls emirgControls;

    private float timer;
    private int scoreOfEmirg;

    private bool firstSpawnHappened;
    void Start()
    {
        emirg = GameObject.FindWithTag("emirg");
        emirgControls = emirg.GetComponent<emirgControls>();
    }
    void Update()
    {
        if (spawningObject.name == "Jordan1")
        {
            scoreOfEmirg = emirgControls.scoreOfEmirg;
            
            if(scoreOfEmirg <= 250)
                spawnRate = ((250 - scoreOfEmirg) / 250f) * 3.5f + 0.5f; // starts from 4 and goes to 0.5 (max at 250)

            else if (scoreOfEmirg >= 300) // jordan4 spawning at 300 score and breaking jordan1 spawner
            { 
                gameObject.SetActive(false);
                Instantiate(jordan4, new Vector3(16, -0.1f, 0), gameObject.transform.rotation);
            }
        }
        
        if (emirg != null && emirg.activeSelf)
        {
            if (firstSpawnHappened == false) // first spawn depends on a independent constant from spawn rate
            {
                if (firstSpawn <= 0)
                {
                    Instantiate(spawningObject,
                        new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY),
                            transform.position.z), transform.rotation);
                    
                    firstSpawnHappened = true;
                }
                
                else
                    firstSpawn -= Time.deltaTime;
                
            } 
            
            else
            {
                if (timer < spawnRate)
                    timer += Time.deltaTime;
                
                else
                {
                    Instantiate(spawningObject,
                        new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY),
                            transform.position.z), transform.rotation);
                    
                    timer = 0;
                }
            }
        }
    }
}
