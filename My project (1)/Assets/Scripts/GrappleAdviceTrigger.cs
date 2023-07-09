using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAdviceTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager pm = other.gameObject.GetComponent<PlayerManager>();
            pm.passedGrappleTrigger = true;
            pm.HandleAdvices();
        }
    }
}
