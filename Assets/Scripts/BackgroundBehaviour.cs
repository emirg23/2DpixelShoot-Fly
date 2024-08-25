using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
   public float BackgroundSpeedObject = 0;
   public float DeadZone = -21;
   
   public GameObject Ground;
   public GameObject emirg;
  
   private bool SpawnRight = true;
   void Start()
   {
      if(emirg== null)
         emirg = GameObject.FindWithTag("emirg");
   }

   void Update()
   {
      BackgroundSpeedObject = GameObject.FindWithTag("BackgroundSpeed").GetComponent<BackgroundSpeed>().backgroundSpeed;
      
      gameObject.transform.position += Vector3.left * (BackgroundSpeedObject * Time.deltaTime);
     
      if (gameObject.transform.position.x < DeadZone) // dead zone check
         Destroy(gameObject);

      if (Ground != null) // ground spawns itself with triggering at camera border
      {
         float cameraRightEdgeX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;
         float tolerance = 0.000000001f;

         
         if ((gameObject.transform.position.x - cameraRightEdgeX) < tolerance && BackgroundSpeedObject != 0)
         {
            if (SpawnRight)
            {
               Instantiate(Ground, new Vector3(19.59132f, -4.84f, 0), gameObject.transform.rotation);
               SpawnRight = false;
            }
         }
      }
   }
}
