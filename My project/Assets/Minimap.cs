using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    int pom = 0;
    public Camera m_OrthographicCamera;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //rotacia minimapy
        transform.rotation = Quaternion.Euler(90f,player.eulerAngles.y,0f);
    }


    private void Start()
    {
        m_OrthographicCamera.orthographicSize = 60;
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.C))
        {
            pom++;
        }

        if (pom == 2) {
            m_OrthographicCamera.orthographicSize = 40;
        }
        if (pom == 1)
        {
            m_OrthographicCamera.orthographicSize = 90;
        }
        if (pom == 3)
        {
            m_OrthographicCamera.orthographicSize = 60;
            pom = 0;
        }

    }
}
