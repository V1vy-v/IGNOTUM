using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //玩家属性
    public float hp;
    public float sanity;
    public int Speed = 5;

    //当前复活点
    private Vector2 revivePos = new Vector2(0,0);

    //组件
    public Animator animator;
    public CapsuleCollider2D cc;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        body = GetComponent<Rigidbody2D>();
    }


    private float h, v;
    // Update is called once per frame
    void Update()
    {
        //上下左右移动
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(h * Speed, v * Speed);
    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    /// <param name="dmg"></param>
    public void Wound(int dmg)
    {

    }
    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void DeadAndRevive()
    {
        //播放死亡动画

        //在复活点复活

    }
}
