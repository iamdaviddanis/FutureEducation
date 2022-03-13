using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBarNumber : MonoBehaviour
{

    private Image TimerBar;
    QuestionScript time;
    // Start is called before the first frame update
    void Start()
    {
        TimerBar = GetComponent<Image>();
        time = FindObjectOfType<QuestionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerBar != null)
            TimerBar.fillAmount = (float)time.b / 60f;
    }
}
