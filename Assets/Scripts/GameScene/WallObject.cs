using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObject : MonoBehaviour
{
    private BoxCollider2D c;
    private bool isUpdated = false;
    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<BoxCollider2D>();
        //订阅巨人死亡事件和大眼触发事件
        EventCenter.GetInstance().AddEventlistener("GiantDead", Unlock);
        EventCenter.GetInstance().AddEventlistener("EyeBorn", Lock);
    }

    private void Unlock()
    {
        c.isTrigger = true;
    }
    private void Lock()
    {
        c.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && 
            other.transform.position.x > transform.position.x &&
            !isUpdated)
        {
            //发布复活点更新事件
            EventCenter.GetInstance().EventTrigger("UpdateRevivePos");
            isUpdated = true;
        }
    }
}
