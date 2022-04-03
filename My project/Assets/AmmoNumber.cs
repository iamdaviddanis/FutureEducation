using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoNumber : MonoBehaviour
{
    public Text AmmoText;
    player_main player;
  
    // Start is called before the first frame update
    void Start()
    {
       // HealthBar = GetComponent<Image>();
        player = FindObjectOfType<player_main>();
        
    }

    // Update is called once per frame
    void Update()
    {

        AmmoText.text = player.ammo.ToString();
       
      

    }
}
