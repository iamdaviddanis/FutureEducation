using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Budova : MonoBehaviour
{
    public GameObject budova;
    public int hp_budova = 100;
    public void hit_budova(int vstup)
    {
        hp_budova -= vstup;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
