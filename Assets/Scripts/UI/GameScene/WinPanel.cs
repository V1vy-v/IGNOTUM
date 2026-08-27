using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : BasePanel
{
    public Button BtnSure;

    public override void Init()
    {
        Invoke(nameof(StopGame), 0.6f);
        BtnSure.onClick.AddListener(()=>
        {
            UIManager.Instance.HidePanel<WinPanel>();
            SceneManager.LoadScene("BeginScene");
            Time.timeScale = 1f;
        });
    }

    private void StopGame()
    {
        Time.timeScale = 0;
    }
}
