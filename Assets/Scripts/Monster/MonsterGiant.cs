using UnityEngine;

public class MonsterGiant : BaseMonster
{
    [SerializeField] private int heartHp = 1;
    [SerializeField] private int headHp = 1;

    //攻击间隔时间
    [SerializeField] private float attackOffset = 5f;

    private float currentAttackTime = -1f;
    private int attackHash;

    //受伤判定碰撞体
    public Collider2D normalHeadCollider;
    public Collider2D normalHeartCollider;
    public Collider2D newHeadCollider;
    public Collider2D newHeartCollider;

    protected override void Start()
    {
        base.Start();
        attackHash = Animator.StringToHash("Attack");
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

    // 受伤逻辑
    public override void Wound(string partTag)
    {
        switch (partTag)
        {
            case "Heart":
                if (heartHp <= 0) return;
                heartHp--;
                break;
            case "Head":
                if (headHp <= 0) return;
                headHp--;
                break;
            default:
                return;
        }

        if (heartHp <= 0 && headHp <= 0)
        {
            animator.SetBool(deadHash, true);
            StartCoroutine(DestroyAfterDelay(2f, spriteRenderer));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 这里调用玩家受伤方法
            other.GetComponent<PlayerObject>().Wound(100);
        }
    }

    // 由动画事件调用，用于切换碰撞体
    public void ChangeColliderOnAnimation()
    {
    }
}