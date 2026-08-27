using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ArrowObject : MonoBehaviour
{
    private float speed = 30f;
    public float damage = 1f;
    public float lifeTime = 5f;

    private Rigidbody2D rb;
    private Vector2 dir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>发射：设置方向，之后匀速直线飞</summary>
    public void Fire(Vector2 direction)
    {
        dir = direction.normalized;
        transform.right = dir;
        rb.velocity = dir * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Head"))
        {
            //other.GetComponentInParent<MonsterGiant>()?.Wound("Head");
        }
        else if (layer == LayerMask.NameToLayer("Heart"))
        {
            //other.GetComponentInParent<MonsterGiant>()?.Wound("Heart");
        }
        else if (layer == LayerMask.NameToLayer("Eye"))
        {
            //other.GetComponentInParent<MonsterEye>()?.Wound("Eye");
        }
        else if (layer == LayerMask.NameToLayer("Player"))
            return;                      // 忽略发射者
        else
        {
            rb.velocity = Vector2.zero;  // 撞墙等：停下
            return;                      // 停在墙上（不销毁）
        }

        Destroy(gameObject);             // 打中怪物后消失
    }
}