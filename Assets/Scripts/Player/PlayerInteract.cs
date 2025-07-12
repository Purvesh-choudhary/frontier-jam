using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Transform interactOrigin;
    [SerializeField] float interactRange = 1.5f;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] GameObject interactUI;

    [SerializeField] Transform holdpoint;
    [SerializeField] float throwForce = 5f;
    [SerializeField] IPickupable heldObject;

    Animator animator;

    // [Header("Trajectory Visualization")]
    // [SerializeField] LineRenderer trajectoryLine;
    // [SerializeField] int trajectoryPoints = 30;
    // [SerializeField] float trajectoryTimeStep = 0.1f;
    // [SerializeField] float maxTrajectoryTime = 3f;
    // [SerializeField] LayerMask obstacleLayer = -1; // What layers can stop the trajectory
    // [SerializeField] bool showTrajectory = true;

    private void Start()
    {
        // trajectoryLine.enabled = false;
        animator = GetComponentInChildren<Animator>();
    }
        

    private void Update()
    {
        // trajectoryLine.positionCount = trajectoryPoints;        
        // To show UI In Game for interactable Objects  
        Collider[] colliders = Physics.OverlapSphere(interactOrigin.position, interactRange, interactableLayer);
        if (colliders.Length > 0)
            interactUI.SetActive(true);
        else
            interactUI.SetActive(false);

        // Show trajectory when holding an object
        // if (heldObject != null && showTrajectory)
        // {
        //     ShowTrajectory();
        //     trajectoryLine.enabled = true;
        // }
        // else
        // {
        //     trajectoryLine.enabled = false;
        // }
    }

    // void ShowTrajectory()
    // {
    //     Vector3 startPos = holdpoint.position;
    //     Vector3 startVelocity = interactOrigin.forward * throwForce;

    //     Vector3[] points = new Vector3[trajectoryPoints];

    //     for (int i = 0; i < trajectoryPoints; i++)
    //     {
    //         float time = i * trajectoryTimeStep;

    //         // Stop if we've exceeded max time
    //         if (time > maxTrajectoryTime)
    //         {
    //             // Fill remaining points with the last valid position
    //             for (int j = i; j < trajectoryPoints; j++)
    //             {
    //                 points[j] = points[i - 1];
    //             }
    //             break;
    //         }

    //         // Calculate position using physics formula: s = ut + 0.5at²
    //         Vector3 point = startPos + startVelocity * time + 0.5f * Physics.gravity * time * time;

    //         // Check for collision with obstacles
    //         if (i > 0)
    //         {
    //             Vector3 direction = point - points[i - 1];
    //             float distance = direction.magnitude;

    //             if (Physics.Raycast(points[i - 1], direction.normalized, distance, obstacleLayer))
    //             {
    //                 // Hit something, stop trajectory here
    //                 RaycastHit hit;
    //                 Physics.Raycast(points[i - 1], direction.normalized, out hit, distance, obstacleLayer);
    //                 point = hit.point;

    //                 // Fill remaining points with hit position
    //                 for (int j = i; j < trajectoryPoints; j++)
    //                 {
    //                     points[j] = point;
    //                 }
    //                 break;
    //             }
    //         }

    //         points[i] = point;
    //     }

    //     trajectoryLine.positionCount = trajectoryPoints;
    //     trajectoryLine.SetPositions(points);
    // }

    public void OnInteract()
    {
        TryInteract();
        animator.SetTrigger("OnInteract");
    }

    void TryInteract()
    {
        Collider[] colliders = Physics.OverlapSphere(interactOrigin.position, interactRange, interactableLayer);

        float closestDistance = Mathf.Infinity;
        IInteractable closestInteractable = null;

        foreach (Collider collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(interactOrigin.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }
        
        if (heldObject != null)
        {
            heldObject.OnThrow();
            heldObject = null;
        }
        else if (closestInteractable != null)
        {
            closestInteractable.Interact();

            IPickupable pickupable = closestInteractable as IPickupable;
            if (pickupable != null)
            {
                pickupable.OnPickup(holdpoint);
                heldObject = pickupable;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (interactOrigin != null)
            Gizmos.DrawWireSphere(interactOrigin.position, interactRange);
    }
}