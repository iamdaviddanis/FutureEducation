using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron : MonoBehaviour
{
    

    private Vector3 go_to;
    private float speed=6.0f;

   
    private float period = 0.0f;
    private float za;


    
    void Start()
    {
        go_to=new Vector3(0,0,0);
        za=Random.Range(5,10);
        gen_lokacia();
    }

  
    void Update()
    {
        if (period > za)
        {

            gen_lokacia();
            period = 0;
        }
        period += Time.deltaTime;

        

         float step = speed * Time.deltaTime;
         transform.position = Vector3.MoveTowards(transform.position, go_to, step);
    }


    void gen_lokacia()
    {
        float random_x=Random.Range(-150,150);
        float random_z=Random.Range(-150.0f,150.0f);
        float random_y=Random.Range(10.0f,50.0f);

        go_to.x=random_x;
        go_to.y=random_y;
        go_to.z=random_z;
    }
}
