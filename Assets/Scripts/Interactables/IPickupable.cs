using UnityEngine;

public interface IPickupable
{
    void OnPickup(Transform holdpoint);
    void OnThrow();
}