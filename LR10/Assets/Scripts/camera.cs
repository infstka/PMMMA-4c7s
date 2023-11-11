using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject GameCamera;
    public GameObject Camera;
    private bool isGameCameraActive;

    // Start is called before the first frame update
    void Start()
    {
        isGameCameraActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            isGameCameraActive = !isGameCameraActive;
            GameCamera.SetActive(isGameCameraActive);
            Camera.SetActive(!isGameCameraActive);
        }     
    }
}
