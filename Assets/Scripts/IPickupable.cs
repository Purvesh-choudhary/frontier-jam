using UnityEngine;

public interface IPickupable
{
    void OnPickup(Transform holdpoint);
    void OnThrow(Vector3 throwForce);
}