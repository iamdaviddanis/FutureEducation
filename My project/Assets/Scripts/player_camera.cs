using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_camera : MonoBehaviour
{
    private float radius=5.0f;
    public Vector3 smer;
    

    private float v=0;
    private float h=0;
    private Vector3 rotation;
    private Vector3 position;

    private float rot_speed=6.0f;

    public GameObject target;
    public Vector3 off_set;
    private Vector3 target_final;


    public int main=0;


    private float zaloha_v=0;
    private float zaloha_h=0;

    bool bruh=true;


    private float rot_extra_x=0;




    private float otazka_position_x=0;
    private float otazka_position_y=3.73f;
    private float otazka_position_z=0.1f;

    private float otazka_rotation_x=35.43f;
    private float otazka_rotation_y=0f;
    private float otazka_rotation_z=0f;


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;

        smer=new Vector3();
        rotation=new Vector3();
        position=new Vector3();
        target_final=new Vector3();
    }


    void Update()
    {
        
        
        if(main == 0)
            hlavna_camera();
        else if(main == 1)
            sekundarna_camera();
         else
            camera_otazka();
        
    }

    void sekundarna_camera()
    {
        Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;

        transform.parent=target.transform;

       /* position.x=2.0f;
        position.y=4.5f;
        position.z=-0.5f;

        rotation.x=10f;
        rotation.y=-45f;
        rotation.z=0f;*/

        position.x=2.42f;
        position.y=4.03f;
        position.z=-1.17f;

        rotation.x=00f;
        rotation.y=-38.33f;
        rotation.z=0f;


        rotation.x+=rot_extra_x;

        transform.localPosition=position;
        transform.localEulerAngles=rotation; 
    }

    public void set_extra_rot(float vstup)
    {
        rot_extra_x=vstup;
    }
    
    void hlavna_camera()
    {
        Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
        transform.parent=null;

        controls_hlavna();

        target_final=target.transform.position;
        target_final=target_final+off_set;

        rotation.x=Mathf.Sin(h)*radius;
        rotation.y=Mathf.Sin(v)*radius;
        rotation.z=Mathf.Cos(h)*radius;

        position.x=rotation.x+target_final.x;
        position.y=-rotation.y+target_final.y;
        position.z=rotation.z+target_final.z;

        smer=target_final-position;
        smer=Vector3.Normalize(smer);

        transform.position=position;
        transform.LookAt(position+smer,Vector3.up);
          
    }

    void controls_hlavna()
    {
        if(Input.GetKey("f"))
            bruh=false;
        if(bruh)
        {
            h+= Input.GetAxis("Mouse X") * Time.deltaTime * rot_speed;
            v+= Input.GetAxis("Mouse Y") * Time.deltaTime * rot_speed;
        }
        
    }

    public void prepni(int status)
    {
        if(status==main)
            return;
        if(status==0)
        {
            zaloha_h=h;
            zaloha_v=v;
        }
        else
        {
            h=zaloha_h;
            v=zaloha_v;
        }
        main=status;
    }


    void camera_otazka()
    {
        Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
        transform.parent=target.transform;

       position.x=otazka_position_x;
       position.y=otazka_position_y;
       position.z=otazka_position_z;

       rotation.x=otazka_rotation_x;
       rotation.y=otazka_rotation_y;
       rotation.z=otazka_rotation_z;

        transform.localPosition=position;
        transform.localEulerAngles=rotation; 


    }


}
