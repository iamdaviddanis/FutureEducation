using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_main : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform bone;
    public Transform ruka_ik;


    public GameObject camera;
    private player_camera camera_script;

   
    private float gravity=-9.8f;
    private float bone_rot=0.0f;

    private bool hybe_sa=false;
    private bool mier=false;


    public int hp=100;
    private bool zije=true;

    public Texture2D cross_hair;


    private float bone_rot_cover=0.0f;


    public int ammo=100;
    private bool can_mier=true;

    public MeshRenderer zbran_model;
    public MeshRenderer tablet_model;

    
    
  
    void OnGUI()
    {
        GUI.Label(new Rect(5,0,80,20),"HP " + hp);
        GUI.Label(new Rect(105,0,80,20),"NABOJE " + ammo);
        if(!zije)
        {
             GUI.Label(new Rect(Screen.width/2,Screen.height/2,500,250),"RIP");
        }
        if(mier)
            GUI.Label(new Rect((Screen.width/2)-50,(Screen.height/2)-25,50,50),cross_hair);



    }

    void LateUpdate()
    {
        if(mier)
        {
            bone_rot+= Input.GetAxis("Mouse Y") * 2.0f;

             if(Input.GetKey("q"))
             {
                bone_rot_cover=45.0f;
             }

           /* if(bone_rot >= 15)s
                bone_rot=15;
            else if(bone_rot <= - 25)
                bone_rot=-25;  */         
        }
        else
        {
            bone_rot=0;
            bone_rot_cover=0;
        }
        //Debug.Log(bone_rot);
        camera_script.set_extra_rot(-bone_rot);
        bone.localRotation = Quaternion.Euler(-bone_rot*1.8f,0, 0);
           
    }


    public void hit(int vstup)
    {
        hp-=vstup;
    }


    public bool mierim()
    {
        return mier;
    }

    public bool mam_ammo()
    {
        if(ammo > 0)
            return true;
        return false;
    }
    public void minus_ammo(int v)
    {
        ammo-=v;
    }

    void Start()
    {
        camera_script = camera.GetComponent<player_camera>();
      
        ragdoll(true);
    }

    void Update()
    {
        if(zije)
        {
            if(hp <=0)
            {
                 zije=false;
                 ragdoll(false);
            }
               

             animator.SetInteger("status",0);      
            Vector3 pohyb=new Vector3(0.0f,0.0f,0.0f);
            if(controller.isGrounded)
                pohyb.y=0;
            else
                pohyb.y=gravity;

            if(can_mier)
            {
                if(mier)
                    camera_script.prepni(1);
                else
                    camera_script.prepni(0);
            }
      
            controls();
            //mozem_pohyb=rotuj_na_smer();
            controller.Move(pohyb*3.0f*Time.deltaTime);
        }
       
    }

    void controls()
    {
        if(Input.GetMouseButton(1))
        {
            mier=true;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            mier=false;
            animator.SetInteger("status",0);  
        }

        controls_normal();
        controls_gun();
        otazka_stav();
        
    }

    void controls_gun()
    {
        if(mier && can_mier)
        {


            float xd= Input.GetAxis("Mouse X") * Time.deltaTime * 200.0f;
            transform.Rotate(0,xd,0,Space.Self);

            animator.SetInteger("status",5);    
            if(Input.GetKey("w"))
            {
                animator.SetInteger("status",6); 

            }
            else if(Input.GetKey("s"))
            {
                animator.SetInteger("status",7); 
            }
            else if(Input.GetKey("a"))
            {
                animator.SetInteger("status",8); 
            }
            else if(Input.GetKey("d"))
            {
                animator.SetInteger("status",9); 
            }



           
        }
    }

    void controls_normal()
    {
        if(!mier)
        {
            animator.SetInteger("status",0);      
            hybe_sa=false;
            if(Input.GetKey("w"))
            {
                animator.SetInteger("status",1);   
                hybe_sa=true;        
            }
            else if(Input.GetKey("s"))
            {
                animator.SetInteger("status",2); 
                hybe_sa=true;             
            }
            else if(Input.GetKey("a"))
            {
                animator.SetInteger("status",3);           
            }
            else if(Input.GetKey("d"))
            {
                animator.SetInteger("status",4);           
            }
           
            if(hybe_sa)
            {
                if(Input.GetKey("a"))
                {
                    transform.Rotate(0,-80*Time.deltaTime,0,Space.Self);
                }
                else if(Input.GetKey("d"))
                    transform.Rotate(0,80*Time.deltaTime,0,Space.Self);
            }     
        }
    }
    

     void ragdoll(bool v)
    {
        controller.enabled=v;
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

    void otazka_stav()
    {
        //if otazka
        if(Input.GetKeyDown("v"))
        {
            can_mier=false;
            mier=false;

            camera_script.prepni(3);
            animator.SetInteger("status",999);       

            zbran_model.enabled=false;
            tablet_model.enabled=true;


            Vector3 ruka_pos=new Vector3(-0.00084f,0.00053f,-0.00126f);
            Vector3 ruka_rot=new Vector3(4f,117.2f,79.1f);
        
            
            ruka_ik.localPosition=ruka_pos;
            ruka_ik.localEulerAngles=ruka_rot;
           

           
        }
        
        if(Input.GetKeyDown("b"))
        {
            can_mier=true;
            mier=false;

            camera_script.prepni(0);
            animator.SetInteger("status",888);       

            zbran_model.enabled=true;
            tablet_model.enabled=false;


            Vector3 ruka_pos=new Vector3(0.0f,0.00065f,1e-05f);
            Vector3 ruka_rot=new Vector3(4.58f,135.47f,135.47f);


            ruka_ik.localPosition=ruka_pos;
            ruka_ik.localEulerAngles=ruka_rot;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
          if (collision.gameObject.tag == "drop")
          {
              ammo+=10;
              GameObject.Destroy(collision.gameObject);
          }
    }

}
