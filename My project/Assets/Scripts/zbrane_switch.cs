using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zbrane_switch : MonoBehaviour
{
    
    private int selected = 0;
    public GameObject scope_obj;


    public GameObject player;
    public GameObject player_body;
    private player_main player_script;


    public Camera camera;


    void Start()
    {
         player_script = player.GetComponent<player_main>();
        select();
    }

 
    void Update()
    {
        
        int prev_selected = selected;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
             scope_obj.SetActive(false);
             player_script.set_scope(false);
             player_body.SetActive(true);
              camera.fieldOfView=60;

            if(selected >= transform.childCount - 1)
                selected=0;
            else
                selected++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            scope_obj.SetActive(false);
            player_script.set_scope(false);
            player_body.SetActive(true);
             camera.fieldOfView=60;

            if(selected <=0)
                selected= transform.childCount - 1;
            else
                selected--;
        }
        if(prev_selected != selected)
            select();
           
    }

    void select()
    {
        int i=0;
        foreach(Transform zbrane in transform)
        {
            if(i == selected)
            {
                zbrane.gameObject.SetActive(true);
            }
            else
                zbrane.gameObject.SetActive(false);
            i++;
        }
    }
}
