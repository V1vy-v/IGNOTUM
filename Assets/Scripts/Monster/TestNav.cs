using UnityEngine;
using UnityEngine.AI;

public class TestNav : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            // 调试信息
            Debug.Log($"路径存在: {agent.hasPath}, 剩余距离: {agent.remainingDistance}");
        }
    }
}