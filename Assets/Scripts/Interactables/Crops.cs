using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour, IInteractable
{

    [SerializeField] ParticleSystem cropsParticle;
    [SerializeField] float shakeIntensity = 1f;
    [SerializeField] int perKickDestructAmount, MaxDestructionAmount;
    int currentDestrucionamount;

    public void Interact()
    {
        CameraShake.Instance.ShakeCamera(shakeIntensity, 1f);
        StartCoroutine(KickCrops());
    }

    IEnumerator KickCrops()
    {
        yield return new WaitForSeconds(1.5f);
        cropsParticle.Play();
        if (currentDestrucionamount > MaxDestructionAmount) {
            DestructionManager.Instance.Destruct(perKickDestructAmount);
        }
    }

}
