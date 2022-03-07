using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image HealthBar;
    public float CurrentHealth;
    private float MaxHealth = 100f;
    player_main player;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponent<Image>();
        player = FindObjectOfType<player_main>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = player.hp;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
    }
}
