using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载在相机上的脚本，游戏开始时动态加载开始场景
/// </summary>
public class main : MonoBehaviour
{
    
    void Start()
    {
        UIManager.Instance.ShowPanel<BeginPanel>();
    }
}
