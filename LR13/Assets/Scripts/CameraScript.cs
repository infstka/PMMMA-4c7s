using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.y > transform.position.y) 
        {
            transform.position = new Vector3(transform.position.x, playerPos.position.y, transform.position.z);
        }
    }
}
