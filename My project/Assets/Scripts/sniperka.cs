using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperka : MonoBehaviour
{
    public int dmg;

    public float speed_rate;
    private float cas;

  
    public Camera camera;

    public GameObject player;
    private player_main player_script;

    public ParticleSystem flash;

    
    public GameObject scope_obj;


    public GameObject zbran;
    public GameObject player_body;



    void Start()
    {
        player_script = player.GetComponent<player_main>();
        flash.Stop();
        flash.loop=false;
       
        scope_obj.SetActive(false);
    }

    void Update()
    {

        if(player_script.scope_b())
        {  
            scope_obj.SetActive(true);
            zbran.GetComponent<Renderer>().enabled = false;
            player_body.SetActive(false);
            camera.fieldOfView=30;
        }         
        else
        {
             scope_obj.SetActive(false);
             zbran.GetComponent<MeshRenderer>().enabled = true;
            player_body.SetActive(true);
            camera.fieldOfView=60;
        }

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
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,1000.0f,1 << 9))
        {
            hit.transform.gameObject.SendMessage("hit",dmg);
        }
    }
}
