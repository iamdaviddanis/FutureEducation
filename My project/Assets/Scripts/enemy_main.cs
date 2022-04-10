using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_main : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;
    private Transform target;
   

    private int hp;
    private int status;
    private bool zije=true;
    private Vector3 look_smer=new Vector3(0,0,0);


    public Collider collider;
    private GameObject player;
    private float gravity=-9.8f;

    private bool fresh=true;

    public bool zijes()
    {
        if(zije==true && animator.GetBool("res") == true)
            return true;
        else if(zije == false && animator.GetBool("res") == true)
            return true;
        return false;
    }

    void ragdoll(bool v)
    {
        controller.enabled=v;
        collider.enabled=v;

        animator.enabled=v;
        Rigidbody [] bodies=GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody body in bodies)
        {
            body.isKinematic=v;
            body.velocity=Vector3.zero;
            body.angularVelocity=Vector3.zero;
        }
        
       // controller.collider.enabled=v;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "shot_prefab(Clone)")
        {
           
            //hit(10);
        }

         if (collision.gameObject.tag == "enemy")
         {
             Physics.IgnoreCollision(collision.collider,GetComponent<Collider>());
         }

    }
    void OnControllerColliderHit(ControllerColliderHit  collision)
    {
         if (collision.gameObject.tag == "enemy")
         {
             Physics.IgnoreCollision(collision.collider,GetComponent<Collider>());
         }
    }

    void Start()
    {

        tag="enemy";

        hp=100;
        animator.SetInteger("status",1); 
        animator.SetBool("res",true); 
        animator.SetInteger("hp",100); 
        ragdoll(true);
        Debug.Log("OK");

    }

    public void set_target(Transform vstup)
    {
        target=vstup;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pohyb=new Vector3(0.0f,0.0f,0.0f);
        if(controller.isGrounded)
        {
            if(fresh)
            {
                animator.SetInteger("status",50); 
                fresh=false;
            }
            pohyb.y=0;
        }
           
        else
        {
            pohyb.y=gravity;
        }
            
        controller.Move(pohyb*3.0f*Time.deltaTime);


        if(Input.GetKey("p"))
        {
             ragdoll(false);
        }
        status=animator.GetInteger("status");
        zije=animator.GetBool("zije");
        hp=animator.GetInteger("hp");
       
       if(zije)
       {
            agro();
            if(hp <=0)
            {
                if(status !=10)
                    animator.SetInteger("status",10); 
                 animator.SetBool("zije",false);
               
            }
       }

       if(status== - 3)
       {

            ragdoll(false);
            animator.SetInteger("status",420); 
       }
            
     
       
        transform.rotation = Quaternion.LookRotation(look_smer);
       //Debug.Log(status+"-"+ zije);
    }

    void agro()
    {
        if(status==1)
        {
            if(rotuj_na_target())
            {
                float distance = Vector3.Distance(target.position, transform.position);
                if(distance < 3)
                {
                    int random=Random.Range(2,4);
                    //Debug.Log(random);
                    animator.SetInteger("status",random); 
                }
            }
           
        }
    }
    
    bool rotuj_na_target()
    {
        Vector3 target_xy=new Vector3(target.position.x,transform.position.y,target.position.z);
        Vector3 smer=target_xy-transform.position;

        smer=Vector3.Normalize(smer);

        float uhol = Vector3.Angle(smer, transform.forward);
        float rot_speed = 8.0f * Time.deltaTime;
        look_smer = Vector3.RotateTowards(transform.forward, smer, rot_speed, 0.0f);
      
        if(uhol < 45.0f )
        {
            return true;
        }
        return false;
    }

    public void hit(int vstup)
    {
        if(zije)
        {
            animator.SetInteger("hp",hp-vstup); 
            animator.SetInteger("status",9); 
            animator.SetBool("d_stav",false);

            player=GameObject.FindWithTag("player");
             set_target(player.transform);
            

            
        }
       
    }


  

}
