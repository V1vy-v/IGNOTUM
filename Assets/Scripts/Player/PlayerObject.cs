using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //玩家属性
    public float hp;
    public float sanity;
    public int Speed;

    //当前复活点
    private Vector2 revivePos = new Vector2(0,0);
    //当前瞄准点
    private Vector3 tarPos;
    //是否在射箭
    private bool isShooting = false;

    //组件
    public Animator animator;
    public CapsuleCollider2D cc;
    public Rigidbody2D body;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private float h, v;
    // Update is called once per frame
    void Update()
    {
        //上下左右移动
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        body.velocity = new Vector2(h * Speed, v * Speed);
        if(h != 0)
        {
            animator.SetBool("Move", true);
            if (!isShooting)
            {
                if (h > 0) sr.flipX = true;
                else sr.flipX = false;
            }
        }
        else
            animator.SetBool("Move", false);

        //攻击，按下瞄准，抬起射出
        if (Input.GetMouseButton(0))
        {
            tarPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            //射箭的动画，动画结束自动调用创建箭的方法
            animator.SetTrigger("Atk");

            isShooting = true;
            if (tarPos.x < transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;
        }
    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    /// <param name="dmg"></param>
    public void Wound(int dmg)
    {
        //受伤逻辑
        hp -= dmg;
        //带动
    }
    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void DeadAndRevive()
    {
        //播放死亡动画

        //在复活点复活

    }
    public void BuildArrow()
    {
        print("射出一根箭");


        isShooting = false;
    }
}
