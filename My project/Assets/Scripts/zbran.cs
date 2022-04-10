using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zbran : MonoBehaviour
{
    
    public int dmg;

    public float speed_rate;
    private float cas;

    public GameObject naboj;
    public GameObject koniec;

    public Camera camera;


    public GameObject player;
    private player_main player_script;

    public ParticleSystem flash;

    void Start()
    {
        player_script = player.GetComponent<player_main>();
         flash.Stop();
         flash.loop=false;
    }

    void Update()
    {
/*
         Ray ray=new Ray();
         Vector3 zaciatok=transform.position;
         Vector3 koniec_pos=koniec.transform.position;
         Vector3 smer=koniec_pos-zaciatok;
         smer=Vector3.Normalize(smer);
        ray.origin=zaciatok;
        ray.direction=smer;

        Debug.DrawRay(ray.origin,ray.direction*1000,Color.black);
        */

        //if(Input.GetMouseButton(0) && player_script.mierim() && player_script.mam_ammo())

        

        if(Input.GetMouseButton(0) && player_script.mierim() && player_script.mam_ammo())
        {
            if(Time.time > cas)
            {
                cas=Time.time+speed_rate;
                strielaj();
                player_script.minus_ammo(1);
                flash.Play();
            } 
        }
        else
            flash.Stop();

    }

    void strielaj()
    {
        Ray ray=camera.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
       /* GameObject laser=GameObject.Instantiate(naboj,transform.position,transform.rotation) as GameObject;
        laser.GetComponent<ShotBehavior>().set_target(ray.direction);
        GameObject.Destroy(laser,5f);*/

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,1000.0f,1 << 9))
        {
            hit.transform.gameObject.SendMessage("hit",dmg);
        }
       


    }
}
