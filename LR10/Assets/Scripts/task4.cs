using UnityEngine;
using System.Collections;

public class task4 : MonoBehaviour 
{
    public GameObject prefab;
    
    // Instantiate the prefab somewhere between -10.0 and 10.0 on the x-z plane 
    void Start() 
    {
        Vector3 position = new Vector3(Random.Range(5.0f, 20.0f), 10, Random.Range(5.0f, 20.0f));
        Instantiate(prefab, position, Quaternion.identity);
    }
}