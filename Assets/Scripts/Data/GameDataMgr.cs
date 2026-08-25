using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    //单例模式
    private static GameDataMgr instance=new GameDataMgr();
    public static GameDataMgr Instance=>instance;

    //音乐相关
    public MusicData musicData;
    GameDataMgr()
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
    }

    /// <summary>
    /// 存储音乐数据
    /// </summary>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData,"MusicData");
    }
}
