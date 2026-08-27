using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerPos;
    public Transform sacrifice;
    public float moveSpeed = 3f;   // 移动速度
    public float stayTime = 2f;    // 在目标点停留时间

    private bool isFollowing = true;   // 是否跟随玩家

    private void Start()
    {
        EventCenter.GetInstance().AddEventlistener("EyeBorn", MoveToSacriFice);
    }

    void Update()
    {
        if (isFollowing)
        {
            transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
        }
    }

    private void MoveToSacriFice()
    {
        EventCenter.GetInstance().RemoveEventlistener("EyeBorn", MoveToSacriFice);
        StartCoroutine(MoveToAndBack());
    }

    IEnumerator MoveToAndBack()
    {
        isFollowing = false;

        // 平滑移到目标点
        Vector3 target = new Vector3(sacrifice.position.x, sacrifice.position.y, -10);
        while (Vector2.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;

        // 停留一会
        yield return new WaitForSeconds(stayTime);

        // 平滑移回玩家当前位置
        Vector3 back = new Vector3(playerPos.position.x, playerPos.position.y, -10);
        while (Vector2.Distance(transform.position, back) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, back, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = back;

        // 恢复跟随
        isFollowing = true;
    }
}