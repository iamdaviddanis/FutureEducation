using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject otazky;
    public GameObject enemy;
    public GameObject player;
    public Transform budova;

    public GameObject enemy_dron;
    public GameObject kamos_dron;

    private QuestionScript otazky_script;
    private player_main player_script;

    private bool can_spawn=true;
    private int pocet=10;
    


    public GameObject[] enemies;
    public GameObject[] e_drony;


    void OnGUI()
    {
        /*if(otazky_script.get_odpoved()==-1)
            GUI.Label(new Rect(5,20,80,20),"ODPOVEDAJ");
        else if(otazky_script.get_odpoved()==0)
            GUI.Label(new Rect(5,20,80,100),"ZLA ODPOVED");
        else
            GUI.Label(new Rect(5,20,80,100),"DOBRA ODPOVED");*/
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

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
                    float random_x=Random.Range(100.0f,130.0f);
                    float random_z=Random.Range(10,-25.0f);
                    float random_y=Random.Range(10.0f,50.0f);

                    Vector3 pos=new Vector3(random_x,random_y,random_z);
                    GameObject enemy_robot=GameObject.Instantiate(enemy,pos,transform.rotation) as GameObject;

                    float random_z_budova=Random.Range(0,-15.0f);
                    Transform budova_t=budova;
                    budova_t.position = new Vector3(budova.position.x,budova.position.y,random_z_budova);
                    enemy_robot.GetComponent<enemy_main>().set_target(budova_t);
                }
                enemies = GameObject.FindGameObjectsWithTag("enemy");

                int dron_pocet=Random.Range(1,2);
                for(int i=0;i<dron_pocet;i++)
                {
                    float random_x=Random.Range(100.0f,130.0f);
                    float random_z=Random.Range(10,-25.0f);
                    float random_y=Random.Range(10.0f,12.0f);

                    Vector3 pos=new Vector3(random_x,random_y,random_z);
                    Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                    GameObject enemy_dron_spawn=GameObject.Instantiate(enemy_dron,pos,rotation) as GameObject;

                    enemy_dron_spawn.GetComponent<enemy_dron>().set_target(player.transform);
                }
                e_drony = GameObject.FindGameObjectsWithTag("enemy_dron");

                for(int i=0;i<dron_pocet;i++)
                {
                    float random_x=Random.Range(100.0f,130.0f);
                    float random_z=Random.Range(10,-25.0f);
                    float random_y=Random.Range(10.0f,12.0f);

                    Vector3 pos=new Vector3(random_x,random_y,random_z);
                    Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                    GameObject kamos_dron_spawn=GameObject.Instantiate(kamos_dron,pos,rotation) as GameObject;

    
                }

                can_spawn=false;
                if(odpoved == 0)
                    pocet+=2;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            reset();
        }
        
      
    }
  
    void reset()
    {

        bool jedna=true;
        bool dva=true;

        if(enemies !=null)
        {
            foreach(GameObject e in enemies)
            {
                
                enemy_main enemy_script=e.GetComponent<enemy_main>();
                if(enemy_script.zijes())
                {
                    can_spawn=false;
                    jedna=false;
                    break;
                }  
               
            }  
        }  
         if(e_drony !=null)
        {
            foreach(GameObject e in e_drony)
            {
                
                enemy_dron enemy_script=e.GetComponent<enemy_dron>();
                if(enemy_script.zijes())
                {
                    can_spawn=false;
                    dva=false;
                    break;
                }  
                
            }  
        }
        if(jedna && dva)
        {
            can_spawn=true;
            otazky_script.reset();
            xd=true;
            o_r=true;
        }

    }
}
