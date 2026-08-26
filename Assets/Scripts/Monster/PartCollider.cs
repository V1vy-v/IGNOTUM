using UnityEngine;

/// <summary>
/// 挂载到子物体碰撞器的脚本，用于处理不同部位受伤逻辑
/// </summary>
public class PartCollider : MonoBehaviour
{
    private BaseMonster monster;
    private int playerArrowLayer;

    void Start()
    {
        monster = GetComponentInParent<BaseMonster>();
        playerArrowLayer = LayerMask.NameToLayer("PlayerArrow");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(11);
        if (other.gameObject.layer != playerArrowLayer) return;
        monster.Wound(LayerMask.LayerToName(gameObject.layer));
        print(LayerMask.LayerToName(gameObject.layer) + "受伤");
    }
}