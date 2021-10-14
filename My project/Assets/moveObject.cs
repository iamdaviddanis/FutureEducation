using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{

    public float speed = .1f;

    // Update is called once per frame
    void Update()
    {

        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 moveSmer = new Vector3(xDirection, 0.0f, zDirection);

        transform.position += moveSmer * speed;
    }
}
