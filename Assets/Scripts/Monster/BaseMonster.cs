using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 怪物基类，只处理所有怪物共有的行为
/// </summary>
public abstract class BaseMonster : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected NavMeshAgent agent; // 寻路组件
    public PlayerObject playerObject;

    protected float moveSpeed = 1.0f;
    [SerializeField] protected float attackRange;//攻击范围

    //动画参数哈希
    protected int walkHash;
    protected int deadHash;

    protected float distance;

    protected enum MonsterState { Idle, Walk }
    protected MonsterState state = MonsterState.Idle;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = FindObjectOfType<PlayerObject>();
        agent = GetComponent<NavMeshAgent>();
        InitializeAnimationHashes();
        agent.speed = moveSpeed;
        agent.acceleration = 2f;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected virtual void InitializeAnimationHashes()
    {
        walkHash = Animator.StringToHash("IsWalk");
        deadHash = Animator.StringToHash("Dead");
    }

    protected virtual void Update()
    {
        if (playerObject == null) return;
        ChangeState();
        CalMove();
        SetAnimator();
        TryAttack();
    }


    /// <summary>
    /// 切换状态
    /// </summary>
    protected virtual void ChangeState()
    {
        distance = Vector2.Distance(transform.position, playerObject.transform.position);
        state = distance < attackRange * 1.5f ? MonsterState.Idle : MonsterState.Walk;
    }

    /// <summary>
    /// 处理移动逻辑
    /// </summary>
    protected virtual void CalMove()
    {
        if (state == MonsterState.Idle)
        {
            print("进入攻击范围");
            agent.isStopped = true;
            rb.velocity = Vector2.zero;
            return;
        }
        //Vector2 direction = (playerObject.transform.position - transform.position).normalized;
        //rb.velocity = direction * moveSpeed;
        agent.isStopped = false;
        agent.SetDestination(playerObject.transform.position);
    }

    /// <summary>
    /// 动画控制
    /// </summary>
    protected virtual void SetAnimator()
    {
        animator.SetBool(walkHash, state == MonsterState.Walk);
        if (state == MonsterState.Walk)
        {
            // 使用 localScale 翻转
            Vector3 scale = transform.localScale;
            if (playerObject.transform.position.x < transform.position.x)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    /// <summary>
    /// 延迟移除 死亡渐隐
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="sprite"></param>
    /// <returns></returns>
    protected virtual IEnumerator DestroyAfterDelay(float delay, SpriteRenderer sprite)
    {
        yield return new WaitForSeconds(delay);
        Color originalColor = sprite.color;
        float startAlpha = originalColor.a;
        float fadeDuration = 1f;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(startAlpha, 0f, t);
            sprite.color = newColor;
            yield return null;
        }
        Color finalColor = sprite.color;
        finalColor.a = 0f;
        sprite.color = finalColor;
        Destroy(gameObject);
    }
    public abstract void Wound(string partTag);
    public abstract void TryAttack();
    public abstract void ResetMonster();
}