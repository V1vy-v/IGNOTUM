using System.Collections;
using UnityEngine;

/// <summary>
/// 怪物基类，只处理所有怪物共有的行为
/// </summary>
public abstract class BaseMonster : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected PlayerObject playerObject;

    [SerializeField] protected float moveSpeed = 3f;//移动参数
    [SerializeField] protected float attackRange = 3f;//攻击范围

    //动画参数哈希
    protected int walkHash;
    protected int deadHash;

    protected enum MonsterState { Idle, Walk }
    protected MonsterState state = MonsterState.Idle;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = FindObjectOfType<PlayerObject>();
        InitializeAnimationHashes();
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
        float distance = Vector2.Distance(transform.position, playerObject.transform.position);
        state = distance < attackRange ? MonsterState.Idle : MonsterState.Walk;
    }

    /// <summary>
    /// 处理移动逻辑
    /// </summary>
    protected virtual void CalMove()
    {
        if (state == MonsterState.Idle)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = (playerObject.transform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    /// <summary>
    /// 动画控制
    /// </summary>
    protected virtual void SetAnimator()
    {
        animator.SetBool(walkHash, state == MonsterState.Walk);
        if (state == MonsterState.Walk)
        {
            spriteRenderer.flipX = playerObject.transform.position.x - transform.position.x < 0;
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
}