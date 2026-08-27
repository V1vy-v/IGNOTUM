using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin multiChannelPerlin;
    //震动持续时间
    private float shakeTime;
    //每次震动时间
    private float shakeTimeTotal;
    //震动强度
    private float shakeIntensity;
    public void Start()
    {
        cinemachineVirtualCamera = this.GetComponent<CinemachineVirtualCamera>();
        multiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shakeIntensity = 5f;
        shakeTimeTotal = 0.5f;
    }

    private void Update()
    {

        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0,shakeIntensity,shakeTime / shakeTimeTotal);
        }
    }

    public void ShakeCamera()
    {
        shakeTime = shakeTimeTotal;
        multiChannelPerlin.m_AmplitudeGain =  shakeIntensity;
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
