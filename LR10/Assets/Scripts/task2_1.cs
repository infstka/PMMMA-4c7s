using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task2_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 30 * Time.deltaTime);      
        transform.Rotate(Vector3.right * 30 * Time.deltaTime);   
    }
}
