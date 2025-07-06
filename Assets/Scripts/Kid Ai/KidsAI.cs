using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KidsAI : MonoBehaviour
{

    // Simple FSM: Idle → Patrol → Chase → Catch
    // Alert radius + line of sight
    // On catch: call CageSystem.CagePlayer()

    KidState currentState;

    public Transform[] patrolPoints;
    public Transform player;

    public float patrolSpeed = 3f;
    public float chaseSpeed = 6f;

    public float detectionRange = 5f;
    public float searchTime = 3f;

    public NavMeshAgent agent;


    public Transform fovPoint;
    [SerializeField] float viewAngle = 45f;
    public float viewDistance = 6f;
    [SerializeField] LayerMask viewLayer;

    public float catctDistance = 1f;
    public Transform chickenHolder;
    public Transform cageTransform;
    public float cageDistance = 1f;
    public bool isChickenInCage = false;




    void Start()
    {
        SwitchState(new KidPatrolState(this));
    }

    void Update()
    {
        currentState?.Update();
    }

    public void SwitchState(KidState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - fovPoint.position).normalized;

        float distance = Vector3.Distance(fovPoint.position, player.position);
        if (distance > viewDistance)
            return false;

        float angle = Vector3.Angle(fovPoint.forward, dirToPlayer);
        if (angle > viewAngle / 2f)
            return false;

        if (Physics.Raycast(fovPoint.position, dirToPlayer, out RaycastHit hit, viewDistance, viewLayer))
        {
            if (hit.transform != player)
                return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (transform == null) return;

        Gizmos.color = Color.magenta;
        Vector3 leftRay = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightRay = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        Gizmos.DrawRay(transform.position, leftRay * viewDistance);
        Gizmos.DrawRay(transform.position, rightRay * viewDistance);
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
}
