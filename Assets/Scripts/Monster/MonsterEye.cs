using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MonsterEye : BaseMonster
{

    [SerializeField] private int eyeHp = 1;

    private float currentAttackTime = -1f;
    private int attackHash;

    protected override void Start()
    {
        base.Start();
        attackHash = Animator.StringToHash("Attack");

        attackRange = 0f;
    }

    protected override void Update()
    {
        base.Update();

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

    public override void TryAttack()
    {
        //×·ÖðÕ½£¬Ö»´¦ÀíÅö×²Âß¼­
    }
}
