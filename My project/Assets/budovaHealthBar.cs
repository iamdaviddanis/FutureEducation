using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class budovaHealthBar : MonoBehaviour
{
    private Image HealthBarBudova;
                            // public float CurrentHealth;
                            //private float MaxHealth = 100f;
   
    Budova budova;
    // Start is called before the first frame update
    void Start()
    {
      
        HealthBarBudova = GetComponent<Image>();
        
        budova = FindObjectOfType<Budova>();
    }

    // Update is called once per frame
    void Update()
    {
      

          if (HealthBarBudova != null)
               HealthBarBudova.fillAmount = budova.hp_budova / 100f;
    }
}
