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
            other.GetComponent<Product>().moveSpeed = 0;
            print(first);
        }
        if(other.GetComponent<Product>().useSecondWaypoint == false)
        {
            
            second = other.gameObject;
            other.GetComponent<Product>().moveSpeed = 0;
            print(second);
        }
    }


    public static void RemoveProducts()
    {
        first = null;

        second = null;
    }
}
