using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObject : MonoBehaviour
{
    private BoxCollider2D collider2D;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        //订阅巨人死亡事件和大眼触发事件
        EventCenter.GetInstance().AddEventlistener("GaintDead", Unlock);
        EventCenter.GetInstance().AddEventlistener("EyeBorn", Lock);
    }

    private void Unlock()
    {
        collider2D.isTrigger = true;
    }
    private void Lock()
    {
        collider2D.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && 
            other.transform.position.x > transform.position.x)
        {
            //发布复活点更新事件
            EventCenter.GetInstance().EventTrigger("UpdateRevivePos");
        }
    }
}
