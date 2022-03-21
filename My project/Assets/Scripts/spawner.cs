using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject otazky;
    public GameObject enemy;
    public GameObject player;
    public Transform budova;


    private QuestionScript otazky_script;
    private player_main player_script;

    private bool can_spawn=true;
    private int pocet=2;
    


    public GameObject[] enemies;

    void OnGUI()
    {
        if(otazky_script.get_odpoved()==-1)
            GUI.Label(new Rect(5,20,80,20),"ODPOVEDAJ");
        else if(otazky_script.get_odpoved()==0)
            GUI.Label(new Rect(5,20,80,100),"ZLA ODPOVED");
        else
            GUI.Label(new Rect(5,20,80,100),"DOBRA ODPOVED");
    }
    void Start()
    {
        otazky_script = otazky.GetComponent<QuestionScript>();
        player_script=player.GetComponent<player_main>();
    }

    private bool xd=true;
    private bool o_r=true;
    private float cas=0.0f;

    void Update()
    {
         
        int odpoved=otazky_script.get_odpoved();
        
        if(odpoved == -1 && otazky_script.prisla_otazka)
        {
            if(xd)
            {
                player_script.set_stav(1);
                player_script.set_stav(0);
                
                xd=false;  
            }
            else if(o_r)
            {
                if(cas > 1)
                {
                    otazky_script.render_otazka();
                    cas=0.0f;
                    o_r=false;
                }
                cas += Time.deltaTime;
            }

        }
        
        if(odpoved >=0)
        {
            if(can_spawn)
            {
                player_script.set_stav(1);

                for(int i=0;i<pocet;i++)
                {
                    float random_x=Random.Range(60.0f,80.0f);
                    float random_z=Random.Range(-50.0f,50.0f);
                    float random_y=Random.Range(10.0f,50.0f);

                    Vector3 pos=new Vector3(random_x,random_y,random_z);
                    GameObject enemy_robot=GameObject.Instantiate(enemy,pos,transform.rotation) as GameObject;
                    enemy_robot.GetComponent<enemy_main>().set_target(budova);
                }
                enemies = GameObject.FindGameObjectsWithTag("boha");
                can_spawn=false;
                if(odpoved == 0)
                    pocet+=2;
            }
            reset();
        }
        
      
    }


    void reset()
    {

        if(enemies !=null)
        {
            foreach(GameObject e in enemies)
            {
                
                enemy_main enemy_script=e.GetComponent<enemy_main>();
                if(enemy_script.zijes())
                {
                    can_spawn=false;
                    break;
                }  
                can_spawn=true; 
            }
            if(can_spawn)
            {
                 Debug.Log("PCIE KRAVE RESET");
                otazky_script.reset();
                xd=true;
                o_r=true;

               
            }
                
        }  
    }
}
