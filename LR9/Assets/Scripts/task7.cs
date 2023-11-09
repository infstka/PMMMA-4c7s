using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task7 : MonoBehaviour
{
    private MeshRenderer render; // для управления цветом объекта
    private float minX, maxX, minZ, maxZ; // границы объекта

    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<MeshRenderer> ();
       
        minX = render.bounds.min.x;
        maxX = render.bounds.max.x;
        minZ = render.bounds.min.z;
        maxZ = render.bounds.max.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // если нажата ЛКМ
        {
            float newX = Random.Range (minX, maxX);
            float newZ = Random.Range (minZ, maxZ);
            float newY = gameObject.transform.position.y + Random.Range(1, 10); // случайная высота от 1 до 10

            GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
            sphere.transform.position = new Vector3 (newX, newY, newZ);

            // случайный цвет
            Renderer sphereRender = sphere.GetComponent<Renderer>();
            sphereRender.material.color = new Color(Random.value, Random.value, Random.value);
        }

        if (Input.GetKeyDown(KeyCode.Space)) // если нажат пробел
        {
            float newX = Random.Range (minX, maxX);
            float newZ = Random.Range (minZ, maxZ);
            float newY = gameObject.transform.position.y + 5;

            GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
            cube.transform.position = new Vector3 (newX, newY, newZ);

            // случайный цвет
            Renderer cubeRender = cube.GetComponent<Renderer>();
            cubeRender.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
