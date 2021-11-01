using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
pohyb_status 0 = idle
			1 = walk
			2 = cuvaj
			3 = walk_left
			4= walk_right


*/
public class player_script : MonoBehaviour
{
    
    public Camera main;
    public Camera shoot;

    public Transform cam;
    public Transform cam_dva;

    public Transform ray_o;
    public Transform bone;

    public Animator animator;
   
	public CharacterController controller;
	public float speed=3;

	public float gravitacia=9.8f;
	
	private Vector3 move;
	private Vector2 rot;
 	private float cam_rot_y=0;

 	private float v_speed=0.0f;
 	private float turn_speed=80.0f;
	private bool aim=false;
	private bool is_f=false;
 	


	private Ray ray;

    void Start()
    {

 		Cursor.lockState = CursorLockMode.Locked;
    	main.enabled=true;
    	shoot.enabled=false;

        Debug.Log("bruh");
        move=new Vector3(0.0f,0.0f,0.0f);
        rot=new Vector2(0.0f,0.0f);
        rot.y=transform.eulerAngles.y;
     

        
    }

    void LateUpdate()
    {
    	if(aim)
    		bone.localRotation = Quaternion.Euler(rot.x, 0, 0);
    	else
    		bone.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        
         if(Input.GetMouseButtonUp(0))
	     {
	        ray = new Ray(ray_o.position, -transform.forward*100);
	        RaycastHit hit;
	        if (Physics.Raycast(ray,out hit))
	        {
	        	Debug.Log(hit.transform.root.gameObject.name);
	        	GameObject g = hit.transform.root.gameObject;
	        	g.GetComponent<bruh>().test_xd();
	        	

	        }
	     }

        Debug.DrawRay(ray_o.position, -transform.forward*100, Color.red);
        is_f=false;

        if(Input.GetMouseButtonDown(1))
        {
        	shoot.enabled=true;
        	main.enabled=false;
        	aim=true;

        	rot.x=0;
        	rot.y=0;
	       
        }
        else if(Input.GetMouseButtonUp(1))
        {
        	shoot.enabled=false;
        	main.enabled=true;
        	aim=false;

        	rot.x=0;
        	rot.y=0;
        }

		

    	if(Input.GetKey("w"))
    	{
    		is_f=true;
    		if(Input.GetKey("left shift"))
    		{
    			animator.SetInteger("pohyb_status",5);
    			speed=12;
    		}
    		else
    		{
    			animator.SetInteger("pohyb_status",1);
    			
    		}

    		move.x=transform.TransformDirection(Vector3.forward).x*speed;
    		move.z=transform.TransformDirection(Vector3.forward).z*speed;
    	}	
    	
    	else if(Input.GetKey("s"))
    	{
    		is_f=true;

    		move.x=-transform.TransformDirection(Vector3.forward).x*speed;
    		move.z=-transform.TransformDirection(Vector3.forward).z*speed;
    		animator.SetInteger("pohyb_status",2);
    	}
    	else
    		animator.SetInteger("pohyb_status",0);


    	//if(!aim)
    	{
    		if(is_f && !aim)
    		{
    			if(Input.GetKey("a"))
		    	{
		    		transform.Rotate(0.0f,-turn_speed*Time.deltaTime,0.0f,Space.Self);
		    	}
		    	else if(Input.GetKey("d"))
		    	{
		    		transform.Rotate(0.0f,turn_speed*Time.deltaTime,0.0f,Space.Self);
		    	}
    		}
    		else if(!is_f )
    		{
    			if(Input.GetKey("a"))
		    	{
		    		animator.SetInteger("pohyb_status",3);

		    		move.x=-transform.TransformDirection(Vector3.right).x*speed;
    				move.z=-transform.TransformDirection(Vector3.right).z*speed;

		    	}
		    	else if(Input.GetKey("d"))
		    	{
		    		animator.SetInteger("pohyb_status",4);

		    		move.x=transform.TransformDirection(Vector3.right).x*speed;
    				move.z=transform.TransformDirection(Vector3.right).z*speed;
		    	}
    		}
    		
    	}
    	
    

        if(controller.isGrounded)
        {
        	v_speed=0;
        }
        else
        	v_speed=gravitacia;
        move.y=v_speed;
        controller.Move(-move * Time.deltaTime);


		rot.y += Input.GetAxis("Mouse X") * 2.0f;
        rot.x += -Input.GetAxis("Mouse Y") * 2.0f;
        rot.x = Mathf.Clamp(rot.x, -60.0f, 60.0f);

       	nieco_camera();

       	
        speed=3;
        move.x=0;
        move.z=0;
    }


    void normal_camera()
    {
        cam.localRotation = Quaternion.Euler(rot.x, rot.y, 0);

    }

    void shot_camera()
    {
		transform.eulerAngles = new Vector2(0, rot.y);	
		cam_dva.localRotation = Quaternion.Euler(-rot.x, 0, 0);
    }

    void nieco_camera()
	{
		if(aim)
			shot_camera();
		else
			normal_camera();
	}
}


