using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackBehaviour : MonoBehaviour
{
    public GameObject BigBullet;
    public GameObject LittleBullet;
    public GameObject FlippingJordan1;

    private GameObject Jordan4;
    private GameObject emirg;

    private float Timer = 0;
    private float AttackPerformTimer = 1;

    private int AttackIndex;
    private List<int> PulledIndex;

    private bool GoingBottom;
    void Start()
    {
        emirg = GameObject.FindWithTag("emirg");
        Jordan4 = GameObject.FindWithTag("Boss");
        PulledIndex = new List<int>();
        
        if (Random.Range(0, 2) == 0) 
            GoingBottom = true;
        
        else 
            GoingBottom = false;
        
        AttackIndex = Random.Range(1, 4); // one of three styles of attack
        
        PulledIndex.Add(AttackIndex); // need to use all three of them to use one again
        
        AttackPerformTimer = 20 - AttackIndex * 5;
    }

    void Update()
    {
        if (Jordan4.transform.position.x <= 4.6f && emirg!= null)
        {
            if (AttackPerformTimer > 0)
            {
                if(emirg.activeSelf)
                    performAttack(AttackIndex);
                
                AttackPerformTimer -= Time.deltaTime;
            }
            
            else
            {
                if (PulledIndex.Count == 3) // all three used so reset
                    PulledIndex.Clear();
                
                AttackIndex = Random.Range(1,4);
                
                while(PulledIndex.Contains(AttackIndex)) 
                    AttackIndex = Random.Range(1,4);
                
                PulledIndex.Add(AttackIndex);
                
                AttackPerformTimer = 20 - AttackIndex * 5;
            }
        }
    }
    
    void performAttack(int attackNum)
    {
        switch (attackNum)
        {
            case 1: // source goes top to bottom, bottom to top. low speed little bullet. reload fast, 15s
            {
                
                float speed = 7;
                
                if (transform.position.y < -4f)
                    GoingBottom = false;
                
                else if (transform.position.y > 4.2f)
                    GoingBottom = true;
        
                if (GoingBottom) // bullet source
                    transform.position += Vector3.down * speed * Time.deltaTime;
                
                else
                    transform.position += Vector3.up * speed * Time.deltaTime;

                if (Timer <= 0)
                {
                    Instantiate(LittleBullet, transform.position, transform.rotation);
                    Timer = 0.40f;
                }
                
                else
                    Timer -= Time.deltaTime;
                
                break;
            }
            case 2: // to the emirg y coordinate. high speed big bullet. reload slow, 10s
            {
                if (Timer <= 0)
                {
                    Instantiate(BigBullet, new Vector3(transform.position.x-0.5f, emirg.transform.position.y, transform.position.z), transform.rotation);
                    Timer = 1;
                }
                
                else
                    Timer -= Time.deltaTime;
                
                break;
            }
            case 3: // bullet goes from top to bottom or bottom to top. flipping jordan1 s that only has touch damage function. 5s
            {
                if (Timer <= 0)
                {
                    float xCoordinate = Random.Range(-8, 0);
                    float yCoordinate;

                    int chance = Random.Range(0, 2);
                    
                    if (chance == 0)
                        yCoordinate = 5;
                    
                    else
                        yCoordinate = -5;
                    
                    Instantiate(FlippingJordan1, new Vector3(xCoordinate, yCoordinate, transform.position.z), transform.rotation);
                    Timer = 0.75f;
                }
                
                else
                    Timer -= Time.deltaTime;
                
                break;
            }
        } 
    }
}
