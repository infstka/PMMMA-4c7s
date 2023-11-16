using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 2.0f; //прыжок
    private Rigidbody rb;
    private bool isJumping = false;
    private LineRenderer lr;
    private float rayLength = 15.0f; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = gameObject.AddComponent<LineRenderer>(); 
        lr.material = new Material(Shader.Find("Sprites/Default")); 
        lr.startColor = lr.endColor = Color.red; 
        lr.startWidth = lr.endWidth = 0.1f; 
    }

void Update()
{
    if (Input.GetButtonDown("Jump") && !isJumping)
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        isJumping = true;
    }

    //луч, направленный вперед от игрока
    Vector3 horizontalForward = new Vector3(transform.forward.x, 0, transform.forward.z);
    Ray ray = new Ray(transform.position, horizontalForward);
    RaycastHit hit;

    if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit, rayLength)) // Устанавливаем максимальную длину луча
    {
        Debug.Log("Raycast hit: " + hit.collider.name);
        if (!hit.collider.CompareTag("Wall") && !hit.collider.CompareTag("Floor") && !hit.collider.CompareTag("Cube"))
        {
            Destroy(hit.collider.gameObject);
        }
    }

    //рисование луча
    if (Input.GetMouseButton(0))
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + horizontalForward * rayLength); //длина луча
    }
    else
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
    }
}


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        //луч только впереди
        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }
}
