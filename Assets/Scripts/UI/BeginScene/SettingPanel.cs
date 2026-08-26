using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    //设置面板UI控件
    public Toggle TogMusic;
    public Toggle TogSound;
    public Slider MusicValue;
    public Slider SoundValue;
    public Button BtnBack;

    public override void Init()
    {
        //每次打开面板更新面板
        UpdatePanel();

        TogMusic.onValueChanged.AddListener((v) =>
        {
            TogMusic.isOn = v;
            GameDataMgr.Instance.musicData.isMusic = v;
            //更新调整后的音乐
            if(SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginSceneMusic.Instance.updateMusic();
            }
            else
            {
                GameSceneMusic.Instance.updateMusic();
            }
        });

        TogSound.onValueChanged.AddListener((v) =>
        {
            TogSound.isOn = v;
            GameDataMgr.Instance.musicData.isSound = v;
            //更新调整后的音乐
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginSceneMusic.Instance.updateMusic();
            }
            else
            {
                GameSceneMusic.Instance.updateMusic();
            }
        });

        MusicValue.onValueChanged.AddListener((v) =>
        {
            MusicValue.value = v;
            GameDataMgr.Instance.musicData.musicValue = v;
            //更新调整后的音乐
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginSceneMusic.Instance.updateMusic();
            }
            else
            {
                GameSceneMusic.Instance.updateMusic();
            }
        });

        SoundValue.onValueChanged.AddListener((v) =>
        {
            SoundValue.value = v;
            GameDataMgr.Instance.musicData.soundValue = v;
            //更新调整后的音乐
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginSceneMusic.Instance.updateMusic();
            }
            else
            {
                GameSceneMusic.Instance.updateMusic();
            }
        });

        BtnBack.onClick.AddListener(() =>
        {
            //退出设置面板时 保存已经设置过的数据
            GameDataMgr.Instance.SaveMusicData();
            //隐藏设置面板
            UIManager.Instance.HidePanel<SettingPanel>();
            Time.timeScale = 1.0f;
        });
    }

    //更新设置面板函数
    public void UpdatePanel()
    {
        TogMusic.isOn = GameDataMgr.Instance.musicData.isMusic;
        TogSound.isOn = GameDataMgr.Instance.musicData.isSound;
        MusicValue.value = GameDataMgr.Instance.musicData.musicValue;
        SoundValue.value = GameDataMgr.Instance.musicData.soundValue;
    }
}
