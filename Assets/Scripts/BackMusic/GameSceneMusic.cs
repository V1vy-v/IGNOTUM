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

    //当前心跳协程引用，用来停止渐变
    private Coroutine _heartBeatCor;

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
        audioSources[1].Play();
    }

    /// <summary>
    /// 控制heartbeat音频是否开启加速渐变
    /// true：在指定时间内 pitch从1 → 1.5
    /// false：在指定时间内 pitch从当前 → 1.0
    /// </summary>
    /// <param name="isOpen">是否开启加速</param>
    /// <param name="duration">渐变耗时，秒</param>
    public void HartBeatFast(bool isOpen, float duration = 10.0f)
    {
        if (audioSources == null || audioSources.Length < 1) return;

        // 如果上一个渐变还在跑，先停止
        if (_heartBeatCor != null)
        {
            StopCoroutine(_heartBeatCor);
            _heartBeatCor = null;
        }

        float targetPitch = isOpen ? 1.3f : 0.8f;
        _heartBeatCor = StartCoroutine(PitchSmoothCor(audioSources[0], targetPitch, duration));
    }

    /// <summary>
    /// 协程：平滑修改AudioSource pitch
    /// </summary>
    IEnumerator PitchSmoothCor(AudioSource source, float targetPitch, float time)
    {
        float startPitch = source.pitch;
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / time);
            source.pitch = Mathf.Lerp(startPitch, targetPitch, progress);
            yield return null;
        }
        source.pitch = targetPitch;
        _heartBeatCor = null;
    }


    //更新音源数据
    public void updateMusic()
    {
        audioSources[0].mute = !GameDataMgr.Instance.musicData.isMusic;
        audioSources[0].volume = GameDataMgr.Instance.musicData.musicValue;
        audioSources[1].mute = !GameDataMgr.Instance.musicData.isMusic;
        audioSources[1].volume = GameDataMgr.Instance.musicData.musicValue;
    }
}
