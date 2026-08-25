using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    //开始按钮
    public Button BtnStart;
    //设置按钮
    public Button BtnSetting;
    //退出按钮
    public Button BtnQuit;

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

        //设置按钮初始化
        BtnSetting.onClick.AddListener(()=>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        //退出按钮初始化
        BtnQuit.onClick.AddListener(()=>
        {
            Application.Quit();
        });
    }
}
