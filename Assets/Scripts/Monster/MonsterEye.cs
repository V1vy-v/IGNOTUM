using System.Collections;
using UnityEngine;

public class MonsterEye : BaseMonster
{
    [SerializeField] private int eyeHp = 1;
    [SerializeField] private Vector2 bornPos = new Vector2(30, 0);

    protected override void Start()
    {
        base.Start();
        //订阅玩家死亡事件
        EventCenter.GetInstance().AddEventlistener("PlayerDead", ResetMonster);
        ////订阅大眼触发
        //EventCenter.GetInstance().AddEventlistener("EyeBorn", ResetMonster);
    }

    protected override void ChangeState()
    {
        state = MonsterState.Walk;
    }

    protected override void SetAnimator()
    {
        Vector3 scale = transform.localScale;
        if (playerObject.transform.position.x < transform.position.x)
            scale.x = Mathf.Abs(scale.x);
        else
            scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (other.gameObject.layer == playerLayer)
        {
            Debug.Log("玩家受到攻击");
            other.GetComponent<PlayerObject>().PlayerAndRevive();
        }
    }

    public override void Wound(string partTag)
    {
        switch (partTag)
        {
            case "Eye":
                if (eyeHp <= 0) return;
                eyeHp--;
                break;
        }

        if (eyeHp <= 0)
        {
            print("大眼死亡");
            gameObject.SetActive(false);
            // 执行死亡逻辑
            // animator.SetBool(deadHash, true);
            // StartCoroutine(DestroyAfterDelay(2f, spriteRenderer));
            //发布大眼死亡事件
            EventCenter.GetInstance().EventTrigger("EyeDead");
        }
    }

    public override void TryAttack()
    {
    }
    public override void ResetMonster()
    {
        //gameObject.SetActive(true);
        eyeHp = 1;
        transform.position = bornPos;
        //其它

    }
}