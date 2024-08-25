using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingJordanBehaviour : MonoBehaviour
{
    private float speed = 5;
    private float rotationSpeed = 500f;
    private bool goingTop;
    void Start()
    {
        if (gameObject.transform.position.y < 0)
            goingTop = true;
        
        else
            goingTop = false;
    }
    void Update()
    {
        if(goingTop) 
            gameObject.transform.position += Vector3.up * speed * Time.deltaTime;
        
        else
            gameObject.transform.position += Vector3.down * speed * Time.deltaTime;

        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);  // constantly flips
        
        if(gameObject.transform.position.y < -6 || 6 < gameObject.transform.position.y)
            Destroy(gameObject);
    }
}
