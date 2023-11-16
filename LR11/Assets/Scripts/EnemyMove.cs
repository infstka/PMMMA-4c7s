using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    private LineRenderer lr; 
    private float rayLength = 35.0f; //длина луча

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = gameObject.AddComponent<LineRenderer>(); 
        lr.startColor = lr.endColor = Color.red; 
        lr.startWidth = lr.endWidth = 1f; 
    }

    void Update()
    {
        //луч впереди игрока
        Vector3 horizontalForward = new Vector3(transform.forward.x, 0, transform.forward.z);
        Ray ray = new Ray(transform.position, horizontalForward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength) && !hit.collider.CompareTag("Wall") && !hit.collider.CompareTag("Floor")) 

        {
            Debug.Log("Enemy found: " + hit.collider.name);
            if (!hit.collider.CompareTag("Wall") && !hit.collider.CompareTag("Floor"))
            {
                Vector3 directionToMoveAway = transform.position - hit.transform.position;
                rb.AddForce(directionToMoveAway.normalized * speed, ForceMode.Impulse);
            }
        }

        //рисование луча
        //lr.SetPosition(0, transform.position);
        //lr.SetPosition(1, transform.position + horizontalForward * rayLength);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Random.Range(-1f, 1f), 0.0f, Random.Range(-1f, 1f));

        rb.AddForce(movement * speed);

        //луч всегда только впереди
        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }
}
