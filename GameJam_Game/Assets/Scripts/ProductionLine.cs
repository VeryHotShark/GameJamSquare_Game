using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionLine : MonoBehaviour
{
    public float ConveyerSpeed;
    public GameObject[] ProductionLines;
    public GameObject Material;
    private void Awake()
    {
        StartCoroutine(Production());
    }
    private void Update()
    {
        
        if (Vector3.Distance(ProductionLines[ProductionLines.Length-1].transform.position,transform.GetChild(0).transform.position) < .25f)
        {
            StopAllCoroutines();
        }
    }
    public IEnumerator Production()
    {
        while(true)
        {
            Instantiate(Material, ProductionLines[0].transform.position, Quaternion.identity,transform);
            yield return new WaitForSeconds(1f);
        }

    }
}
