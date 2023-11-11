using UnityEngine;

public class task2_2 : MonoBehaviour
{
    public float pushForce = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction.y = 0;  // keep the force horizontal

            rb.AddForce(direction.normalized * pushForce, ForceMode.Impulse);
        }
    }
}
