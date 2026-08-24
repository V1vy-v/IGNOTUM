using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerPos;

    void Update()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
    }
}
