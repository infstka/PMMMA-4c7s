using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject spherePrefab; //префаб сферы
    public float spawnRate = 1.0f; //скорость спавна
    private float nextSpawnTime;
    private int sphereCount = 0; //счетчик сфер

    void Update()
    {
    //находим сферы с "Clone" в имени
    GameObject[] spheres = GameObject.FindGameObjectsWithTag("Enemy");
    int cloneCount = 0;
    foreach (GameObject sphere in spheres)
    {
        if (sphere.name.Contains("Clone"))
        {
            cloneCount++;
        }
    }

    //если все сферы с "Clone" в имени уничтожены, сбрасывается счетчик сфер
    if (cloneCount == 0)
    {
        sphereCount = 0;
    }
        //если пришло время для следующего спавна и количество сфер меньше 10
        if (Time.time > nextSpawnTime && sphereCount < 10)
        {
            nextSpawnTime = Time.time + spawnRate; //обновление времени следующего спавна

            GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");

            GameObject floor = floors[Random.Range(0, floors.Length)];

            Vector3 spawnPosition = new Vector3(
                Random.Range(floor.transform.position.x - floor.transform.localScale.x / 2, floor.transform.position.x + floor.transform.localScale.x / 2),
                floor.transform.position.y + floor.transform.localScale.y / 2 + 0.5f,
                Random.Range(floor.transform.position.z - floor.transform.localScale.z / 2, floor.transform.position.z + floor.transform.localScale.z / 2)
            );

            Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
            sphereCount++;
        }
    }
}
