using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject yellowCubePrefab;
    public GameObject spherePrefab;
    public int cubeCount = 15;
    private int greenCubeCount;

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        // Находим все объекты на сцене
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
          // Проверяем, содержит ли имя объекта "(Clone)"
          if (obj.name.Contains("(Clone)"))
          {
           // Если да, то удаляем объект
           Destroy(obj);
          }
         } 

        // Создаем новые кубы и сферу
        SpawnCubes();
        SpawnSphere();
    }

    void SpawnCubes()
    {
        greenCubeCount = cubeCount;

        for (int i = 0; i < cubeCount; i++)
        {
            // Генерируем случайные координаты для куба
            float x = Random.Range(-90.0f, 190.0f);
            float y = 0.5f;
            float z = Random.Range(-210.0f, -495.0f);

            // Создаем новый зеленый куб
            GameObject greenCube = Instantiate(cubePrefab, new Vector3(x, y, z), Quaternion.identity);
            greenCube.tag = "GreenCube";

            // Изменяем цвет куба на зеленый
            greenCube.GetComponent<Renderer>().material.color = Color.green;

            // Генерируем случайные координаты для куба
            x = Random.Range(-90.0f, 190.0f);
            y = 0.5f;
            z = Random.Range(-210.0f, -495.0f);

            // Создаем новый красный куб
            GameObject redCube = Instantiate(cubePrefab, new Vector3(x, y, z), Quaternion.identity);
            redCube.tag = "RedCube";

            // Изменяем цвет куба на красный
            redCube.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void SpawnSphere()
    {
        // Генерируем случайные координаты для сферы
        float x = Random.Range(-90.0f, 190.0f);
        float y = 0.5f;
        float z = Random.Range(-210.0f, -495.0f);

        // Создаем новую сферу
        GameObject sphere = Instantiate(spherePrefab, new Vector3(x, y, z), Quaternion.identity);
        sphere.tag = "Player";
        sphere.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void DecreaseGreenCubeCount()
    {
        greenCubeCount--;

        if (greenCubeCount <= 0)
        {    
            // Находим все объекты на сцене
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
            {
              // Проверяем, содержит ли имя объекта "(Clone)"
              if (obj.name.Contains("(Clone)"))
              {
               // Если да, то удаляем объект
               Destroy(obj);
              }
             }

            SpawnSphere();
            // Создаем желтый куб по центру плоскости
            GameObject yellowCube = Instantiate(yellowCubePrefab, new Vector3(50, 5f, -350), Quaternion.identity);
            yellowCube.GetComponent<Renderer>().material.color = Color.yellow;
            yellowCube.tag = "YellowCube";
            
        }
    }
}