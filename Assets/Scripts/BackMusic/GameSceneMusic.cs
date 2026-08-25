using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMusic : MonoBehaviour
{
    //提供给外部的接口
    private static GameSceneMusic instance;
    public static GameSceneMusic Instance=>instance;

    //获取音频组件(一共两个)
    public AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //根据设置界面的情况控制音频数据
        foreach (AudioSource source in audioSources)
        {
            source.mute = !GameDataMgr.Instance.musicData.isMusic;
            source.volume = GameDataMgr.Instance.musicData.musicValue;
        }
    }

    /// <summary>
    /// 在场景中遇到不同情况需要切换背景音乐，这是背景音乐切换函数，只需传入一个音频文件的名字即可
    /// 因为HartBeat是固定的，所以改变另外一个音频组件的音源文件即可（也就是audioSources[1]）
    /// </summary>
    public void ChangeBackMusic(string musicAddress)
    {
        if(Resources.Load<AudioClip>("Music/" + musicAddress)==null)
        {
            Debug.LogError("找不到音乐");
            return;
        }
        audioSources[1].clip = Resources.Load<AudioClip>("Music/"+musicAddress);
        audioSources[1].mute= !GameDataMgr.Instance.musicData.isMusic;
    }

    /// <summary>
    /// 控制hartbeat音频（即audioSources[0]）是否随着时间加速
    /// </summary>
    /// <param name="isOpen"></param>
    public void HartBeatFast(bool isOpen)
    {
        //音乐加速
        if(isOpen)
        {
            audioSources[0].pitch = 1.15f;
        }
        //音乐减速
        else
        {
            audioSources[0].pitch = 1.0f;
        }
    }
}
