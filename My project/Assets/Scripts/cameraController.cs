using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    // [SerializeField] public float mouseSensitivity;
    public static float mouseSensitivity;


    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        if (mouseSensitivity == 0) {
            mouseSensitivity = 250;
        }
     parent = transform.parent;
    //    Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
     
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        parent.Rotate(Vector3.up, mouseX);
    }

    public void ChangeMouseSensitivity(float _sensitivity)
    {
        mouseSensitivity = _sensitivity;
        Debug.Log(mouseSensitivity);
       
    }
}
