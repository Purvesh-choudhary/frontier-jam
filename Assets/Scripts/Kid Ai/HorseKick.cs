using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseKick : MonoBehaviour
{

    [SerializeField] Vector3 kickDirection;
    [SerializeField] float kickForce;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cage"))
        {
            StartCoroutine(OpenByKick(other));
        }
    }

    IEnumerator OpenByKick(Collider other)
    {
        yield return new WaitForSeconds(2f);
        other.GetComponent<Animator>().SetTrigger("Open");
        other.GetComponent<Rigidbody>().AddForce(kickDirection * kickForce);
    }
}
