using UnityEngine;

public class PickupObject : MonoBehaviour, IPickupable, IInteractable
{

    [SerializeField] Rigidbody rigidbody;
    [SerializeField] bool isheld = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Interact()
    {

    }

    public void OnPickup(Transform holdpoint)
    {
        isheld = true;
        rigidbody.isKinematic = true;
        transform.SetParent(holdpoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

    }

    public void OnThrow(Vector3 throwForce)
    {
        isheld = false;
        rigidbody.isKinematic = false;
        transform.SetParent(null);
        rigidbody.AddForce(throwForce ,ForceMode.Impulse);
    }

   
}
