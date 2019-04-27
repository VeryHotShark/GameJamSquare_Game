using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerStartState : MonoBehaviour
{

    public static bool productInside;


    private void OnTriggerStay(Collider other)
    {
        if(1 < other.gameObject.layer == 1 < LayerMask.NameToLayer("Product"))
        {
            productInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(1 < other.gameObject.layer == 1 < LayerMask.NameToLayer("Product"))
            productInside = false;
    }

}
