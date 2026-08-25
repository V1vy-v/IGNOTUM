using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button BtnStart;

    /// <summary>
    /// 面板控件初始化
    /// </summary>
    public override void Init()
    {
        //开始按钮初始化
        BtnStart.onClick.AddListener(()=>
        {
            UIManager.Instance.HidePanel<BeginPanel>();
            SceneManager.LoadScene("SampleScene");
        });
    }
}
