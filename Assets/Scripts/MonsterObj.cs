using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterObj : MonoBehaviour
{
    public MonsterData monsterData;

    private float attackOffset = 5;//怪物攻击间隔 
    private float currentAttackTime = -1;//上次攻击时间
    private Animator animator;
    private PlayerObject playerObject;//玩家组件

    private float moveSpeed = 3f;//移动速度

    private int attackHash;
    private float attackRange = 3f;//攻击范围

    enum MonsterState
    {
        Idle,
        Walk
    }
    private MonsterState state = MonsterState.Idle;

    /// <summary>
    /// 获取与玩家距离以改变状态
    /// </summary>
    private void ChangeState()
    {
        //大于距离则改为walk

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        if(animator != null)
        {
            attackHash = Animator.StringToHash("Attack");
        }
        else
        {
            Debug.LogError($"{name}未找到animator组件");
        }
    }
    private void Update()
    {
        ChangeState();
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
    /// 处理怪物手上逻辑
    /// </summary>
    public void Wound()
    {

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
}
