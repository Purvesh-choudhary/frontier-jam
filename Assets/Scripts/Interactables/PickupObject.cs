using UnityEngine;

public class PickupObject : MonoBehaviour, IPickupable, IInteractable
{

    [SerializeField] Rigidbody rigidbody;
    // [SerializeField] bool isHeld = false;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact()
    {

    }

    public void OnPickup(Transform holdpoint)
    {
        rigidbody.isKinematic = true;
        transform.SetParent(holdpoint);
        transform.localPosition = Vector3.zero;
    }

    public void OnThrow()
    {

        transform.SetParent(null);
        rigidbody.isKinematic = false;
    }

   
}
