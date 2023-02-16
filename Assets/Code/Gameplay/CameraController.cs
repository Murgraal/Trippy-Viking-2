using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject followedObject;

    [SerializeField]
    private Camera centerCam;
    [SerializeField]
    private Camera upperCam;
    [SerializeField]
    private Camera lowerCam;

    public static Vector3 StageDimensions = new Vector3(Screen.width, 0, 0);
    public static Vector3 StageDimensions2 = new Vector3(Screen.width, Screen.height, 0);

    private void Start()
    {
        centerCam.transform.position = Camera.main.transform.position;
        var mainCamOrthoZize = Camera.main.orthographicSize;
        
        lowerCam.orthographicSize = mainCamOrthoZize;
        upperCam.orthographicSize = mainCamOrthoZize;
        centerCam.orthographicSize = mainCamOrthoZize;
        
        lowerCam.transform.position = centerCam.transform.position + (Vector3) Vector2.down * centerCam.orthographicSize * 2;
        upperCam.transform.position = centerCam.transform.position - (Vector3) Vector2.down * centerCam.orthographicSize * 2;
    }
    
}
