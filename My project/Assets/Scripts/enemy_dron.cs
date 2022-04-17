using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_dron : MonoBehaviour
{

    public GameObject boom;

    public Rigidbody main;
    public Transform target;
    private Vector3 go_to;
    private float speed=6.0f;

   
    private float period = 0.0f;
    private float za;

    Vector3 target_pos;
    private int hp=100;


    public GameObject naboj;
    public Transform zdroj;


    public float speed_rate;
    private float cas;

    private bool zije=true;

    public GameObject ohen;
    
    void Start()
    {
        main.isKinematic=true;
        main.angularVelocity = Vector3.zero;

        go_to=new Vector3(0,0,0);
        za=Random.Range(5,10);
        gen_lokacia();


       ohen.SetActive(false);
    }
  
    void Update()
    {
         
        if(hp > 0)
        {
            if(hp <=20)
            {
              ohen.SetActive(true); 
            }
            target_pos=target.position;
            if (period > za)
            {
                gen_lokacia();
                period = 0;
            }
            period += Time.deltaTime;

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, go_to, step);

            if(Time.time > cas)
            {
                cas=Time.time+speed_rate;
                strielaj();
            }
        }
        else
        {
            main.isKinematic=false;   
            if(zije)
            {
                 //ohen.Stop();
                zije=false;
                GameObject ex=GameObject.Instantiate(boom,transform.position,transform.rotation) as GameObject;
                //ex.transform.SetParent(transform);
                GameObject.Destroy(ex,2f);
            }
        }
    }


    void gen_lokacia()
    {
      
        float random_x=target_pos.x + Random.Range(-10.0f,10.0f);
        float random_z=target_pos.z + Random.Range(-10.0f,10.0f);
        float random_y=Random.Range(10.0f,17.0f);

        go_to.x=random_x;
        go_to.y=random_y;
        go_to.z=random_z;
    }

    void strielaj()
    {
        target_pos.y+=0.5f;
        Vector3 smer = ( target_pos - zdroj.position ).normalized;
        Ray ray=new Ray(zdroj.position,smer);
        Quaternion rot=Quaternion.LookRotation(smer);
        GameObject laser=GameObject.Instantiate(naboj,transform.position,rot) as GameObject;
        laser.GetComponent<ShotBehavior>().set_target(ray.direction);
        GameObject.Destroy(laser,4f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(hp > 0)
        {
           /* if (collision.gameObject.name == "shot_prefab(Clone)")
            {
                hp--;
            }*/
        }
    }

    public void hit(int vstup)
    {
        hp-=vstup;
    }

    public void set_target(Transform t)
    {
        target=t;
    }

    public bool zijes()
    {
        return zije;
    }
}
