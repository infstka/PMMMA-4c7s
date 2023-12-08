using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wood : MonoBehaviour
{
    public float forceJump;
    public static int score = 0; 
    public Text scoreText; 
    public AudioSource audioSource; 
    public AudioClip collisionSound;
    
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    
    public void OnCollisionEnter2D(Collision2D col)
    {
       if (col.relativeVelocity.y < 0)
       {
           PlayerController.instance.rb.velocity = Vector2.up * forceJump;
           score += 10; // Увеличьте счет на 10
           scoreText.text = "Score: " + score.ToString();  
           audioSource.PlayOneShot(collisionSound);        
       }
    }
    
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.name == "destroy")
        {
            float RandX = Random.Range(-1.7f, 1.7f);
            float RandY = Random.Range(transform.position.y + 20f, transform.position.y + 22f);
            
            transform.position = new Vector3(RandX, RandY, 0);
        }
    }
}
