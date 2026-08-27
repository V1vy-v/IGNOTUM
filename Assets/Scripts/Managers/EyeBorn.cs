using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBorn : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && 
            other.transform.position.x < transform.position.x)
        {
            //发布大眼触发事件
            EventCenter.GetInstance().EventTrigger("EyeBorn");
        }
    }
}
