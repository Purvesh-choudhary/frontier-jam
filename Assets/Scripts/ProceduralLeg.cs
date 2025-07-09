using System.Collections;
using UnityEngine;

public class ProceduralLeg : MonoBehaviour
{
    public Transform legTarget;       // The IK target
    public Transform legHome;         // Where the foot should be
    public float stepDistance = 0.3f;
    public float moveSpeed = 5f;
    public float stepHeight = 0.1f;
    public ProceduralLeg otherLeg;
    public LayerMask groundLayer;

    [HideInInspector]
    public bool isMoving = false;

    void Update()
    {
        float distance = Vector3.Distance(legTarget.position, legHome.position);

        if (!isMoving && !otherLeg.isMoving && distance > stepDistance)
        {
            Vector3 targetPos = GetGroundPoint(legHome.position);
            StartCoroutine(MoveLeg(targetPos));
        }
    }

    Vector3 GetGroundPoint(Vector3 origin)
    {
        if (Physics.Raycast(origin + Vector3.up, Vector3.down, out RaycastHit hit, 2f, groundLayer))
            return hit.point;

        return origin;
    }

    IEnumerator MoveLeg(Vector3 targetPos)
    {
        isMoving = true;
        Vector3 start = legTarget.position;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            Vector3 foot = Vector3.Lerp(start, targetPos, t);
            foot.y += Mathf.Sin(t * Mathf.PI) * stepHeight;
            legTarget.position = foot;
            yield return null;
        }

        legTarget.position = targetPos;
        isMoving = false;
    }
}
