using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zbrane_switch : MonoBehaviour
{
    
    private int selected = 0;

    void Start()
    {
        select();
    }

 
    void Update()
    {
        int prev_selected = selected;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selected >= transform.childCount - 1)
                selected=0;
            else
                selected++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
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
