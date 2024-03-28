using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private Transform player;
    private Vector3 playerDir;
    private Vector3 startingPos;

    [SerializeField] Transform Headpos;
    [SerializeField] float faceSpeed;

    float playerAngle;
    float ogStopingDis;

    bool choseDis;

    private void Start()
    {
        startingPos = transform.position;
        ogStopingDis = agent.stoppingDistance;
        player = GameManager.instance.Player;
    }

    void FacePlayer()
    {
        playerDir = GameManager.instance.Player.position - Headpos.position;
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceSpeed);
    }

    public void Movement()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.SetDestination(GameManager.instance.Player.position);
            FacePlayer();
        }
    }
}
