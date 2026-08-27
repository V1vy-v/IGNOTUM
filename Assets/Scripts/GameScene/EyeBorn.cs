using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBorn : MonoBehaviour
{
    bool isDead = false;

    private void Start()
    {
        EventCenter.GetInstance().AddEventlistener("EyeDead", eyeIsDead);
    }
    private void eyeIsDead()
    {
        EventCenter.GetInstance().RemoveEventlistener("EyeDead", eyeIsDead);
        isDead = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && 
            other.transform.position.x < transform.position.x &&
            !isDead)
        {
            //发布大眼触发事件
            EventCenter.GetInstance().EventTrigger("EyeBorn");
        }
    }
}
