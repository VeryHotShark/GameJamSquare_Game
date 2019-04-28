using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public static GameObject first;
    public static GameObject second;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Product>().useSecondWaypoint == true)
        {
            first = other.gameObject;
            print(first);
        }
        if(other.GetComponent<Product>().useSecondWaypoint == false)
        {
            
            second = other.gameObject;
            print(second);
        }
    }


    public static void RemoveProducts()
    {
        first = null;

        second = null;
    }
}
