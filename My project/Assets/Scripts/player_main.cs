using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_main : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform bone;

    public GameObject camera;
    private player_camera camera_script;

   
    private float gravity=-9.8f;
    private float bone_rot=0.0f;

    private bool hybe_sa=false;
    private bool mier=false;

    public Texture2D cross_hair;
    void OnGUI()
    {
        GUI.Label(new Rect(5,0,80,20),"CMONBRUH");
        if(mier)
            GUI.Label(new Rect((Screen.width/2)-50,(Screen.height/2)-25,50,50),cross_hair);

    }

    void LateUpdate()
    {
        if(mier)
        {
            bone_rot+= Input.GetAxis("Mouse Y") * 2.0f;

           /* if(bone_rot >= 15)
                bone_rot=15;
            else if(bone_rot <= - 25)
                bone_rot=-25;  */         
        }
        else
        {
            bone_rot=0;
        }
        //Debug.Log(bone_rot);
        camera_script.set_extra_rot(-bone_rot);
        bone.localRotation = Quaternion.Euler(-bone_rot*1.8f, 0, 0);
           
    }


    public bool mierim()
    {
        return mier;
    }

    void Start()
    {
        camera_script = camera.GetComponent<player_camera>();
    }

    void Update()
    {

       

        animator.SetInteger("status",0);      
        Vector3 pohyb=new Vector3(0.0f,0.0f,0.0f);
        if(controller.isGrounded)
            pohyb.y=0;
        else
            pohyb.y=gravity;

        if(mier)
            camera_script.prepni(false);
        else
            camera_script.prepni(true);
        controls();
        //mozem_pohyb=rotuj_na_smer();
        controller.Move(pohyb*3.0f*Time.deltaTime);
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
        
    }

    void controls_gun()
    {
        if(mier)
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
    
}
