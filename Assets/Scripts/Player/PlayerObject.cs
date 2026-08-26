using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //玩家属性
    private bool isDead;
    public int Speed;

    //两个复活点
    public Transform revivePos1, revivePos2;
    //当前复活点
    private Vector2 revivePos = new Vector2(0,0);
    //当前瞄准点
    private Vector3 tarPos;
    //是否在射箭
    private bool isShooting = false;
    //射箭点
    public Transform firePos;
    //箭
    private GameObject arrowObj;

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

        //订阅玩家死亡事件
        EventCenter.GetInstance().AddEventlistener("PlayerDead", PlayerDead);
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

        //攻击，按下射出
        if(Input.GetMouseButtonDown(0))
        {
            tarPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
    /// 发布玩家死亡事件
    /// </summary>
    private void PlayerAndRevive()
    {
        EventCenter.GetInstance().EventTrigger("PlayerDead");
        //在复活点复活,触发死亡事件
        transform.position = revivePos;
        isDead = false;
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void PlayerDead()
    {
        //播放死亡动画
    }
    public void BuildArrow()
    {
        arrowObj = Instantiate(Resources.Load<GameObject>("prefab/arrow"), firePos.position, Quaternion.identity);
        arrowObj.GetComponent<ArrowObject>().Fire(tarPos - firePos.position);

        isShooting = false;
    }
}
