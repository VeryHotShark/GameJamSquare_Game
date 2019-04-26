using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionLine : MonoBehaviour
{
    float ConveyerSpeed;
    Vector3[] ProductionLines;
    public GameObject Material;
    void Update()
    {
        
    }
    public IEnumerator Production()
    {
        while(true)
        {
            Instantiate(Material, ProductionLines[0], Quaternion.identity,transform);
        }

    }
}
