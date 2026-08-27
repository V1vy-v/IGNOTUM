using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    public CinemachineVirtualCamera lowShakeCamera;
    public CinemachineVirtualCamera highShakeCamera;

    public void NormalCamera()
    {
        highShakeCamera.gameObject.SetActive(false);
        lowShakeCamera.gameObject.SetActive(true);
    }

    public void ShakeCamera()
    {
        highShakeCamera.gameObject.SetActive(true);
        lowShakeCamera.gameObject.SetActive(false);
    }
}


