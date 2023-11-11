using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task5 : MonoBehaviour
{
    public float speed = 10.0f;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }
}
