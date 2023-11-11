using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_controller : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject gameController;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GreenCube"))
        {
            Destroy(collision.gameObject);
            gameController.GetComponent<game>().DecreaseGreenCubeCount();
        }
        else if (collision.gameObject.CompareTag("RedCube") || collision.gameObject.CompareTag("YellowCube"))
        {
            Destroy(gameObject);
            gameController.GetComponent<game>().ResetGame();
        }
        else if (collision.gameObject.CompareTag("YellowCube"))
        {
            Destroy(gameObject);
            gameController.GetComponent<game>().ResetGame();
        }
    }
}
