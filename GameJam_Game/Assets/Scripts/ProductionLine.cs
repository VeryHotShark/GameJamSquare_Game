using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionLine : MonoBehaviour
{
    public List<GameObject> ProductionLines = new List<GameObject>();
    List<GameObject> childsproducts = new List<GameObject>();
    public GameObject product;
    
    private void Awake()
    {
        StartCoroutine(Production());
    }
    private void Update()
    {
        
    }
    public IEnumerator Production()
    {
        while(true)
        {
            GameObject child = Instantiate(product, ProductionLines[0].transform.position, Quaternion.identity,transform) as GameObject;
            childsproducts.Add(child);
            yield return new WaitForSeconds(1f);
        }

    }
}
