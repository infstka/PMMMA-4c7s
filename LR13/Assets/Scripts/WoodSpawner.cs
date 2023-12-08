using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpawner : MonoBehaviour
{
    public GameObject woodPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 SpawnerPosition = new Vector3();
        
        for (int i = 0; i < 10; i++)
        {
             SpawnerPosition.x = Random.Range(-1.5f, 1.5f);
             SpawnerPosition.y += Random.Range(2f, 4f);
             
             Instantiate(woodPrefab, SpawnerPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
