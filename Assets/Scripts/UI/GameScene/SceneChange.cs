using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    //祭祀场景
    public GameObject sacrificeScene;

    void Start()
    {
        //订阅触发大眼事件
        EventCenter.GetInstance().AddEventlistener("???", SacrificeChange);
    }

    private void SacrificeChange()
    {
        sacrificeScene.SetActive(false);
        //取消订阅触发大眼事件
        EventCenter.GetInstance().RemoveEventlistener("???", SacrificeChange);
    }
}
