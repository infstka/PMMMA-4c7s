using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task4 : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 5.0f; // сила прыжка
    private Rigidbody rb;
    private bool isJumping = false; 
    private Renderer rend; // для управления цветом объекта

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>(); // получить компонент Renderer
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // если нажат пробел и объект не в прыжке
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true; 
        }

        if (Input.GetKeyDown(KeyCode.Space)) // если нажат пробел
        {
            rend.material.color = new Color(Random.value, Random.value, Random.value); // изменить цвет на случайный
        }
    }
    // вызывается при столкновении с другим объектом
    void OnCollisionEnter(Collision collision)
    {
        isJumping = false; 
    }
}
