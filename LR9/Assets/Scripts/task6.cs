using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task6 : MonoBehaviour
{
    public Transform targetObject; // ссылка на объект
    public Vector3 cameraOffset; // смещение камеры относительно объекта

    void Update()
    {
        transform.position = targetObject.position + cameraOffset;
    }
}
