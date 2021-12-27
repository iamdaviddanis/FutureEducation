using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_XD : MonoBehaviour
{
    

    public Texture2D cross_hair;

    void OnGUI()
    {
        GUI.Label(new Rect(5,0,80,20),"CMONBRUH");
         GUI.Label(new Rect((Screen.width/2)-50,(Screen.height/2)-50,50,50),cross_hair);

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
