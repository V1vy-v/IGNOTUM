using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MonsterEye : BaseMonster
{

    [SerializeField] private int eyeHp = 1;

    public Collider2D normalHeadCollider;
    public Collider2D normalHeartCollider;
    public Collider2D newHeadCollider;
    public Collider2D newHeartCollider;

    //¹¥»÷¼ä¸ôÊ±¼ä
    [SerializeField] private float attackOffset = 5f;

    private float currentAttackTime = -1f;
    private int attackHash;

    protected override void Start()
    {
        base.Start();
        attackHash = Animator.StringToHash("Attack");
    }

    protected override void Update()
    {
        base.Update();

    }
    public override void TryAttack()
    {
        if (playerObject == null) return;
        if (state == MonsterState.Idle && Time.time - currentAttackTime > attackOffset)
        {
            animator.SetBool(attackHash, true);
            currentAttackTime = Time.time;
        }
    }

    public override void Wound(string partTag)
    {
        switch (partTag)
        {
            case "Eye":
                if (eyeHp <= 0)
                    return;
                eyeHp--;
                break;
        }
        if(eyeHp <= 0)
        {
            //Ö´ÐÐËÀÍöÂß¼­
            animator.SetBool(deadHash, true);
            StartCoroutine(DestroyAfterDelay(2f, spriteRenderer));
        }
    }

   
}
