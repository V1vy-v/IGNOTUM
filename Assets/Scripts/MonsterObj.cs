using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
/// <summary>
/// 怪物脚本 处理怪物逻辑，状态等
/// </summary>
public class MonsterObj : MonoBehaviour
{
    //伤害判定碰撞器
    public Collider2D normalHeadCollider;//常态心脏位置
    public Collider2D normalHeartCollider;//常态头部位置
    public Collider2D newHeadCollider;
    public Collider2D newHeartCollider;
    public MonsterData monsterData;

    private float attackOffset = 5;//怪物攻击间隔 
    private float currentAttackTime = -1;//上次攻击时间
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerObject playerObject;//玩家组件
    private SpriteRenderer spriteRenderer;

    private Vector2 moveDirection;//移动向量
    private float moveSpeed = 3f;//移动速度

    private int attackHash;
    private int walkHash;
    private float attackRange = 3f;//攻击范围

    enum MonsterState
    {
        Idle,
        Walk
    }
    private MonsterState state = MonsterState.Idle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator != null)
        {
            attackHash = Animator.StringToHash("Attack");
            walkHash = Animator.StringToHash("IsWalk");
        }
        else
        {
            Debug.LogError($"{name}未找到animator组件");
        }
    }

    /// <summary>
    /// 获取与玩家距离以改变状态
    /// </summary>
    private void ChangeState()
    {
        if (playerObject == null)
            return;
        if (Vector2.Distance(this.transform.position, playerObject.gameObject.transform.position) < attackRange)
        {
            state = MonsterState.Idle;
        }
        else
        {
            state = MonsterState.Walk;
        }
    }
    private void SetAnimator()
    {
        if (playerObject == null)
            return;
        if (state == MonsterState.Idle)
        {
            animator.SetBool(walkHash,false);
        }
        else
        {
            animator.SetBool(walkHash, true);
            spriteRenderer.flipX = playerObject.transform.position.x - this.transform.position.x < 0;
        }
    }
    /// <summary>
    /// 计算运动速度
    /// </summary>
    private void CalMove()
    {
        if (state == MonsterState.Idle)
            return;

        moveDirection = (playerObject.transform.position - transform.position).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

    private void Update()
    {
        ChangeState();
        CalMove();
        SetAnimator();
    }

    /// <summary>
    /// 处理怪物攻击逻辑
    /// </summary>
    public void Attack()
    {
        if (playerObject == null)
            return;
        if(state == MonsterState.Idle && Time.time - currentAttackTime > attackOffset)//只有在靠近玩家时才攻击
        {
            animator.SetBool(attackHash,true);
            currentAttackTime = Time.time;
        }
    }
    /// <summary>
    /// 处理怪物受伤逻辑
    /// </summary>
    public void OnPartHit(string partTag)
    {
        switch (partTag) 
        {
            case "Heart":
                if (monsterData.heartHp < 0)
                    return;
                monsterData.heartHp--;
                
                break;
            case "Head":
                if (monsterData.headHp < 0)
                    return;
                monsterData.headHp--;
                break;
        }
        if (monsterData.IsDead())
        {
            //执行死亡逻辑
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("击中玩家");
            // 调用玩家扣血
            other.GetComponent<PlayerObject>().Wound(100);//数值随便填的
        }
    }
    //由动画调用 动态更改碰撞器位置
    public void ChangeColliderOnAnimation()
    {
        
    }
}
