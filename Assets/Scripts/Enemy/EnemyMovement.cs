using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Headpos;
    [SerializeField] float faceSpeed;

    private Vector3 playerDir;
    private Vector3 distractedPos = new Vector3(10000, 10000, 1000);

    void FacePlayer()
    {
        playerDir = GameManager.Instance.Player.position - Headpos.position;
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceSpeed);
    }

    public void Movement()
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            if (!agent.isStopped)
            {
                agent.SetDestination(GameManager.Instance.Player.position);
            }

            FacePlayer();
        }
        else
        {
            Debug.Log("Agent Active&Enabled : " + agent.isActiveAndEnabled + " Agent On Mesh: " + agent.isOnNavMesh);
        }
    }

    public void DistractedMovement()
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            if (!agent.isStopped)
            {
                agent.SetDestination(distractedPos);
            }

            //FacePlayer();
        }
    }

    public void UpdateMoveSpeed(float speed)
    {
        agent.speed = speed;
    }
}
