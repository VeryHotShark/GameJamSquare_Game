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
    public Product product;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(Production());
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
        Product child = ProductPooler.Instance.GetPooledObject(ProductionLines[0].transform.position - Vector3.up * yOffset, Quaternion.identity);
        childsproducts.Add(child);
    }
}
