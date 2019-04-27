using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.GetInstanceID()+""+ this.GetInstanceID());
        if (other.gameObject.layer == 11)
        {
            Debug.Log("enter");
            Product p = other.gameObject.GetComponentInParent<Product>();
            if (p.stuck)
            {
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Product p = other.gameObject.GetComponentInParent<Product>();
        }
    }
}
