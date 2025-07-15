using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UncleAi : MonoBehaviour
{

    [SerializeField] Transform homePoint;
    [SerializeField] Collider homeCollider;
    [SerializeField] float remainingDistanceMax = 3f;
    NavMeshAgent agent;

    void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        agent = GetComponent<NavMeshAgent>();
    }

    void OnGameStateChanged(GameState state)
    {
        if (state == GameState.DayPlaying)
        {
            agent.SetDestination(homePoint.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == homeCollider)
        {
            Destroy(gameObject);
        }
    }
}
