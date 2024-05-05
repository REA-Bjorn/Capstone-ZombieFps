using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Headpos;
    [SerializeField] float faceSpeed;

    private Vector3 playerDir;
    private float baseSpeed;

    private void Start()
    {
        baseSpeed = agent.speed;
    }

    void FacePlayer()
    {
        playerDir = GameManager.Instance.Player.position - Headpos.position;
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceSpeed);
    }

    public void Movement()
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh && !agent.isStopped)
        {
            if (agent.remainingDistance > 40f)
            {
                UpdateMoveSpeed(25f);
            }
            else
            {
                UpdateMoveSpeed(baseSpeed);
            }

            agent.SetDestination(GameManager.Instance.Player.position);
            FacePlayer();
        }
    }

    public void UpdateMoveSpeed(float speed)
    {
        agent.speed = speed;
    }
}
