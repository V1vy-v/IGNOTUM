using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Button BtnQuit;
    public Button BtnSetting;

    public override void Init()
    {
        BtnQuit.onClick.AddListener(() =>
        {
            Debug.Log("ÍË³öÓÎÏ·");
            Application.Quit();
        });

        BtnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
            Invoke(nameof(StopGame), 0.1f);
        });
    }

    private void StopGame()
    {
        Time.timeScale = 0;
    }
}
