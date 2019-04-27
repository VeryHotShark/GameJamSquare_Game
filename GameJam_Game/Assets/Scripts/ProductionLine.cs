using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionLine : MonoBehaviour
{
    public static ProductionLine Instance;

    public float yOffset;
    public float spawnRate = 1f;

    public Transform[] ProductionLines;
    public List<Product> childsproducts = new List<Product>();
    
    [Space]

    public float yOffset2;
    public float spawnRate2 = 1f;

    public Transform[] ProductionLines2;
    public List<Product> childsproducts2 = new List<Product>();

    [Space]

    public float yOffset3;
    public float spawnRate3 = 1f;

    public Transform[] ProductionLines3;
    public List<Product> childsproducts3 = new List<Product>();

    private void Awake()
    {
        Instance = this;
        //StartCoroutine(Production());
    }

    public IEnumerator Production()
    {
        while (true)
        {
            if (/*!Product.isWaiting && */ !ConveyerStartState.productInside)
            {
                //Product child = Instantiate(product, ProductionLines[0].transform.position - Vector3.up * yOffset, Quaternion.identity, transform) as Product;
                Product child = ProductPooler.Instance.GetPooledObject(ProductionLines[0].transform.position - Vector3.up * yOffset, Quaternion.identity);
                childsproducts.Add(child);
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void SpawnProduct()
    {
        if(!ConveyerStartState.productInside)
        {

        Product child = ProductPooler.Instance.GetPooledObject(ProductionLines[0].transform.position - Vector3.up * yOffset, Quaternion.identity);
        childsproducts.Add(child);
        }
    }

    public void SpawnProduct2()
    {
        if(!ConveyerStartState2.productInside)
        {
        Product child = ProductPooler2.Instance.GetPooledObject(ProductionLines2[0].transform.position - Vector3.up * yOffset2, Quaternion.identity);
        childsproducts2.Add(child);
        }
    }

     public void SpawnProduct3()
    {
        if(!ConveyerStartState3.productInside)
        {
        Product child = ProductPooler3.Instance.GetPooledObject(ProductionLines3[0].transform.position - Vector3.up * yOffset3, Quaternion.identity);
        childsproducts3.Add(child);
        }
    }
}
