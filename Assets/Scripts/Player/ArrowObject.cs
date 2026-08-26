using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObject : MonoBehaviour
{
    private float speed = 15f;
    public float damage = 10f;
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
        if(true)
        {

        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        Destroy(gameObject);
    }
}