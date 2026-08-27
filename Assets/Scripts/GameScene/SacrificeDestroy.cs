using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //订阅大眼触发
        EventCenter.GetInstance().AddEventlistener("EysBorn", DestroySelf);
    }

    private void DestroySelf()
    {
        gameObject.SetActive(false);
        EventCenter.GetInstance().RemoveEventlistener("EysBorn", DestroySelf);
    }
}
