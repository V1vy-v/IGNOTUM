using UnityEngine;

/// <summary>
/// 挂载到子物体碰撞器的脚本，用于处理不同部位受伤逻辑
/// </summary>
public class PartCollider : MonoBehaviour
{
    private MonsterObj monster;

    void Start()
    {
        monster = GetComponentInParent<MonsterObj>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerArrow")) return;

        monster.OnPartHit(gameObject.tag);
    }
}