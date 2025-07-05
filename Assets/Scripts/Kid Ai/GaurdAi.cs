// using System;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.AI;

// public class GaurdAi : MonoBehaviour
// {

//     // GaurdState currentState;

//     public Transform[] patrolPoints;
//     public Transform player;

//     public float patrolSpeed = 3f, chaseSpeed=6f;

//     public float detectionRange = 5f;
//     public float searchTime = 3f;

//     public NavMeshAgent agent;


//     [SerializeField] Transform torchTransform;
//     [SerializeField] float viewAngle = 45f;
//     [SerializeField] float viewDistance = 6f;
//     [SerializeField] LayerMask viewObstacles;



//     void Start()
//     {
//         SwitchState(new GaurdPatrolState(this));
//     }

//     void Update()
//     {
//         currentState?.Update();
//     }

//     public void SwitchState(GaurdState newState)
//     {
//         currentState?.Exit();
//         currentState = newState;
//         currentState?.Exit();
//     }

//     public bool CanSeePlayer()
//     {
//         Vector3 dirToPlayer = (player.position - torchTransform.position).normalized;

//         float distance = Vector3.Distance(torchTransform.position, player.position);
//         if (distance > viewDistance)
//             return false;

//         float angle = Vector3.Angle(torchTransform.forward, dirToPlayer);
//         if (angle > viewAngle / 2f)
//             return false;

//         if (Physics.Raycast(torchTransform.position, dirToPlayer, out RaycastHit hit, viewDistance, viewObstacles))
//         {
//             if (hit.transform != player)
//                 return false;
//         }

//         return true;
//     }

//     private void OnDrawGizmosSelected()
//     {
//         if (torchTransform == null) return;

//         Gizmos.color = Color.yellow;
//         Vector3 leftRay = Quaternion.Euler(0, -viewAngle / 2f, 0) * torchTransform.forward;
//         Vector3 rightRay = Quaternion.Euler(0, viewAngle / 2f, 0) * torchTransform.forward;

//         Gizmos.DrawRay(torchTransform.position, leftRay * viewDistance);
//         Gizmos.DrawRay(torchTransform.position, rightRay * viewDistance);
//         Gizmos.DrawWireSphere(torchTransform.position, viewDistance);
//     }


// }
