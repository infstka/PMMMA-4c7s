using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision otherObj)
    {
     if (otherObj.gameObject.tag == "Cube 3.1") {
         Destroy(gameObject,.5f);
     }   
    }
}
