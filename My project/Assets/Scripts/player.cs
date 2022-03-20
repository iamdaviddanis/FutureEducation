using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public CharacterController controller;
    public Transform bone;
    public Transform camera;

    private float gravity=-9.8f;
    private float pohyb_speed=3.0f;
    private Vector3 pohyb;

    private float mouse_x=0;
    private float mouse_y=0;

    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
       // bone.localRotation = Quaternion.Euler(mouse_y, 0, 0);
      //  camera.localRotation = Quaternion.Euler(mouse_y, 0, 0);
    }


    void Update()
    {
        pohyb=new Vector3(0.0f,0.0f,0.0f);
        if(controller.isGrounded)
            pohyb.y=0;
        else
            pohyb.y=gravity;

        rotate_smer();
        controls();
        controller.Move(pohyb*pohyb_speed*Time.deltaTime);   
    }

    void controls()
    {
        if(Input.GetKey("w"))
        {
            pohyb.x=transform.forward.x;
            pohyb.z=transform.forward.z;
        }
        else if(Input.GetKey("s"))
        {
            pohyb.x=-transform.forward.x;
            pohyb.z=-transform.forward.z;
        }
        else if(Input.GetKey("a"))
        {
            pohyb.x=-transform.right.x;
            pohyb.z=-transform.right.z;
        }
        else if(Input.GetKey("d"))
        {
            pohyb.x=transform.right.x;
            pohyb.z=transform.right.z;
        }
    }

    void rotate_smer()
    {
        mouse_x+= Input.GetAxis("Mouse X") * 200.0f * Time.deltaTime;
        mouse_y+= Input.GetAxis("Mouse Y") * 200.0f * Time.deltaTime;

        transform.eulerAngles = new Vector2(0, mouse_x);	
    }
}
