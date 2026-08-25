using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginSceneMusic : MonoBehaviour
{
    //提供给外部接口
    private static BeginSceneMusic instance;
    public static BeginSceneMusic Instance=>instance;

    //对象上挂载的音频脚本
    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //得到对象上的全部音频脚本
        audioSources = GetComponents<AudioSource>();
        //遍历全部音频脚本控制其音乐
        foreach (AudioSource source in audioSources)
        {
            source.mute = !GameDataMgr.Instance.musicData.isMusic;
            source.volume=GameDataMgr.Instance.musicData.musicValue;
        }
    }

    //更新音源数据
    public void updateMusic()
    {
        //遍历全部音频脚本控制其音乐
        foreach (AudioSource source in audioSources)
        {
            source.mute = !GameDataMgr.Instance.musicData.isMusic;
            source.volume = GameDataMgr.Instance.musicData.musicValue;
        }
    }
}
