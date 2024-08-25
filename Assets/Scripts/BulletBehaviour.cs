using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    public bool GoingToRight;
    public float BulletSpeed = 2;
    
    private float DeadZoneX = 9.8f;
    
    private GameObject emirg;
    
    void Start()
    {
        emirg = GameObject.FindWithTag("emirg");
    } 
    void Update()
    {
         bulletTravel();   
         deadZoneCheck();
         
         // make bullets disappear after emirg dies
         if (emirg == null && !GoingToRight) 
             BulletSpeed += 0.01f;
         
         else if (emirg == null && GoingToRight)
             BulletSpeed -= 0.01f;
    }

    void OnCollisionEnter2D(Collision2D collision) // collision of enemy bullets and emirg makes them both disappear
    {
        if(gameObject.CompareTag("emirgBullet") && collision.collider.CompareTag("EnemyBullet") || gameObject.CompareTag("EnemyBullet") && collision.collider.CompareTag("emirgBullet") || gameObject.CompareTag("BossBullet") && collision.collider.CompareTag("emirgBullet"))
            Destroy(collision.collider.gameObject);
    }
    
    void bulletTravel()
    {
        if (GoingToRight)
            gameObject.transform.position += Vector3.right * BulletSpeed * Time.deltaTime;
        
        else
            gameObject.transform.position += Vector3.left * BulletSpeed * Time.deltaTime;
    }

    void deadZoneCheck()
    {
        if (gameObject.transform.position.x < -DeadZoneX || DeadZoneX < gameObject.transform.position.x)
            Destroy(gameObject);
    }
}
