using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Vector3 playerDir;
    private Vector3 startingPos;

    [SerializeField] Transform Headpos;
    [SerializeField] float faceSpeed;
    [SerializeField] float roamTimer;
    [SerializeField] float roamDis;

    float playerAngle;
    float ogStopingDis;

    bool choseDis;
    
    private void Start()
    {
        startingPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        ogStopingDis = agent.stoppingDistance;
        player = GameManager.instance.Player;
    }

    private bool playerIsVis()
    {
        agent.stoppingDistance = ogStopingDis;
        playerDir = GameManager.instance.Player.position - Headpos.position;
        playerAngle = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);
        Debug.DrawRay(Headpos.position, playerDir);

        RaycastHit hit;
        if (Physics.Raycast(Headpos.position , playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && playerAngle<=90f)
            {
                agent.SetDestination(GameManager.instance.Player.position);
                if (agent.remainingDistance<=agent.stoppingDistance)
                {
                    FacePlayer();
                    //Debug.Log("agent.remainingDistance" + agent.remainingDistance + "agent.stoppingDistance" + agent.stoppingDistance);
                }
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }

    void FacePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceSpeed);
    }

    public IEnumerator Roam()
    {
        if (!choseDis && agent.remainingDistance<1.75f)
        {
            choseDis = true;
            agent.stoppingDistance = 0f;
            yield return new WaitForSeconds(roamTimer);
            Vector3 ranPos = Random.insideUnitSphere * roamDis;
            ranPos += startingPos;
            NavMeshHit hit;
            NavMesh.SamplePosition(ranPos, out hit, roamDis, 1);
            agent.SetDestination(hit.position);
            choseDis =false;
        }
    }

    public void Movement()
    {
        if (agent.isActiveAndEnabled)
        {
            if (!playerIsVis())
            {
                StartCoroutine(Roam());

            }
            else if (agent.destination != player.position)
            {
                StartCoroutine(Roam());
            }
        }
    }
}
