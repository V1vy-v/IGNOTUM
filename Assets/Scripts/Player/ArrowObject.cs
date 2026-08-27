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
        // 命中处理：伤害、插在墙上等
        if (other.gameObject.layer == LayerMask.NameToLayer("Head"))
        {
            other.GetComponent<MonsterGiant>().Wound("Head");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Heart"))
        {
            other.GetComponent<MonsterGiant>().Wound("Heart");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Eye"))
        {
            other.GetComponent<MonsterEye>().Wound("Eye");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            return;
        else
        {
            rb.velocity = Vector2.zero;
        }

        Destroy(gameObject);
    }
}