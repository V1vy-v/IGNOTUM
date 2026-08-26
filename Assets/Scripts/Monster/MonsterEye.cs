using System.Collections;
using UnityEngine;

public class MonsterEye : BaseMonster
{
    [SerializeField] private int eyeHp = 1;

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
            Debug.Log("¹ÖÎï¹¥»÷");
            other.GetComponent<PlayerObject>().Wound(9999);
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
            print("¹ÖÎïËÀÍö");
            // Ö´ÐÐËÀÍöÂß¼­
            // animator.SetBool(deadHash, true);
            // StartCoroutine(DestroyAfterDelay(2f, spriteRenderer));
        }
    }

    public override void TryAttack()
    {
    }
}