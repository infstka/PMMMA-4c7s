using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private Vector3 prefabOffset;
    
    private GameObject bird;
    private ARTrackedImageManager arTIM;
    
    private void OnEnable() 
    {
        arTIM = gameObject.GetComponent<ARTrackedImageManager>();
        
        arTIM.trackedImagesChanged += OnImageChanged; 
    }
    
    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj) 
    {
        foreach (ARTrackedImage image in obj.added)
        {
            bird = Instantiate(birdPrefab, image.transform);
            bird.transform.position += prefabOffset;
        }
    }
}
