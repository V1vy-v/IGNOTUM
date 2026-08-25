using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{
}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}
/// <summary>
/// 事件中心
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, IEventInfo> dic = new Dictionary<string, IEventInfo>(); 

    /// <summary>
    /// 通过事件名监听添加的函数
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public void AddEventlistener(string eventName,UnityAction action)
    {
        if (dic.ContainsKey(eventName))
        {
            (dic[eventName] as EventInfo).actions -= action;//先移除再添加，确保唯一
            (dic[eventName] as EventInfo).actions += action;
        }
        else
        {
            dic.Add(eventName, new EventInfo(action));
        }
    }
    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public void RemoveEventlistener(string eventName, UnityAction action)
    {
        if (dic.ContainsKey(eventName))
        {
            (dic[eventName] as EventInfo).actions -= action;
        }
    }

    /// <summary>
    /// 带泛型的函数监听
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public void AddEventlistener<T>(string eventName,UnityAction<T> action)
    {
        if (dic.ContainsKey(eventName))
        {
            (dic[eventName] as EventInfo<T>).actions -= action;
            (dic[eventName] as EventInfo<T>).actions += action;
        }
        else
        {
            dic.Add(eventName, new EventInfo<T>(action));
        }
    }
    public void RemoveEventlistener<T>(string eventName, UnityAction<T> action)
    {
        if (dic.ContainsKey(eventName))
        {
            (dic[eventName] as EventInfo<T>).actions -= action;
        }
    }

    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">哪一个名字的事件触发了</param>
    public void EventTrigger<T>(string name, T info)
    {
        //有没有对应的事件监听
        //有的情况
        if (dic.ContainsKey(name))
        {
            if ((dic[name] as EventInfo<T>).actions != null)
                (dic[name] as EventInfo<T>).actions.Invoke(info);
        }
    }


    /// <summary>
    /// 事件触发（不需要参数的）
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(string name)
    {
        //有没有对应的事件监听
        if (dic.ContainsKey(name))
        {
            if ((dic[name] as EventInfo).actions != null)
                (dic[name] as EventInfo).actions.Invoke();
        }
    }

    public void Clear()
    {
        dic.Clear();
    }
}
