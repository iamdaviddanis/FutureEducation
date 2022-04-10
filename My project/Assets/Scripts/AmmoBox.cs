using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    public Rigidbody main;
    private bool hit_ma=false;

    void Start()
    {
        main.isKinematic=true;
        main.angularVelocity = Vector3.zero;
       
    }

    void Update()
    { 
        main.angularVelocity = Vector3.zero;
       /* if(hit)
        {
            rotuj();
        }*/
    }

    void rotuj()
    {
         transform.Rotate(0, 35.0f*Time.deltaTime, 0, Space.Self);
    }


    public bool get_status()
    {
        return hit_ma;
    }

    void OnCollisionEnter(Collision collision)
    {
       /* if (collision.gameObject.name == "shot_prefab(Clone)" && !hit)
        {
            hit=true;
            main.isKinematic=false;
            main.velocity=Vector3.zero;
           transform.SetParent(null);
           
            //GameObject.Destroy(this);
          
        }*/
         
        
    }

    public void hit(int vstup)
    {
        if(!hit_ma)
        {
            hit_ma=true;
            main.isKinematic=false;
            main.velocity=Vector3.zero;
            transform.SetParent(null);
        }
      
    }

    
}
