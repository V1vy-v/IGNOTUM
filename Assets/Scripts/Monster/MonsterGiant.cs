using UnityEngine;
using UnityEngine.AI;
public class MonsterGiant : BaseMonster
{

    [SerializeField] private int heartHp = 2;
    [SerializeField] private int headHp = 2;
    [SerializeField] private Vector2 bornPos = new Vector2(-112, -11);

    //攻击间隔时间
    [SerializeField] private float attackOffset = 2f;

    //private CinemachineImpulseSource impulseSource;//
    private float currentAttackTime = -5f;
    private int clawAttackHash;
    private int trampleHash;

    //受伤判定碰撞体(父物体)
    public GameObject normalHeadColliderObj;
    public GameObject normalHeartColliderObj;
    //脚部碰撞体父物体
    //public GameObject FeetColliderObj;
    //爪子碰撞体
    public GameObject ClawColliderObj;


    protected override void Start()
    {
        base.Start();

        //normalHeadColliderObj.SetActive(true);
        //normalHeartColliderObj.SetActive(true);
        //ClawColliderObj.SetActive(false);
        //FeetColliderObj?.SetActive(false);
        ClawColliderObj.SetActive(false);

        attackRange = 4f;

        clawAttackHash = Animator.StringToHash("ClawAtk");
        //订阅玩家死亡事件
        EventCenter.GetInstance().AddEventlistener("PlayerDead", ResetMonster);
    }

    public override void TryAttack()
    {
        if (playerObject == null) return;
        if (state == MonsterState.Idle && Time.time - currentAttackTime > attackOffset)
        {
            //if (distance < attackRange)
            //{
            //    animator.SetBool(trampleHash, true);
            //    currentAttackTime = Time.time;
            //    return;
            //}
            if (distance < attackRange * 2)
            {
                animator.SetTrigger(clawAttackHash);
                currentAttackTime = Time.time;
                return;
            }
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
                print("心脏受伤");
                break;
            case "Head":
                if (headHp <= 0) return;
                headHp--;
                print("头部受伤");
                break;
            default:
                return;
        }

        if (heartHp <= 0 && headHp <= 0)
        {
            //发布巨人死亡
            EventCenter.GetInstance().EventTrigger("GiantDead");

            print("巨人死亡");
            //this.gameObject.SetActive(false);
            //animator.SetBool(deadHash, true);
            StartCoroutine(DestroyAfterDelay(2f, spriteRenderer));

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 这里调用玩家受伤方法
            Debug.Log("玩家受到攻击");
            other.GetComponent<PlayerObject>().PlayerAndRevive();
        }
    }

    // 由动画事件调用，用于切换碰撞体
    public void OnClawAtkAnimation()
    {
        print("设置碰撞体");
        ClawColliderObj.SetActive(true);
    }

    public void OnTrampleAnimation()
    {
       // FeetColliderObj.SetActive(true);
    }
    public void ResetAfterAnimation()
    {
        print("重置碰撞体");
        //ClawColliderObj.SetActive(false);
        //FeetColliderObj.SetActive(false);
        ClawColliderObj.SetActive(false);
    }
    public override void ResetMonster()
    {
        headHp = heartHp = 2;
        transform.position = bornPos;
        //重置触发器等等
        ResetAfterAnimation();

    }
}