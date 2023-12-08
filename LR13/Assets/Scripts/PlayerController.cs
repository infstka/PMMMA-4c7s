using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    float horizontal;
    public Rigidbody2D rb;
    public AudioSource audioSource;
    public AudioClip deathSound;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android) 
        {
            horizontal = Input.acceleration.x;    
        }
        
        if (Input.acceleration.x > 0) 
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        
        if (Input.acceleration.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        
        rb.velocity = new Vector2(Input.acceleration.x * 10f, rb.velocity.y);
    }
    
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "destroy")
        {
            Wood.score = 0;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        audioSource.PlayOneShot(deathSound);
        yield return new WaitForSecondsRealtime(deathSound.length);
        SceneManager.LoadScene(0);
    }
}
