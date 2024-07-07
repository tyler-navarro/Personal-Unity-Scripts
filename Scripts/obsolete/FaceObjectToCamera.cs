using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class FaceObjectToCamera : MonoBehaviour
{
    void Update()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform);
        }
        else
        {
            Debug.LogWarning("Main camera is not found. Make sure you have a main camera in the scene.");
        }
    }
}