using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionLine : MonoBehaviour
{
    public static ProductionLine Instance;

    public float spawnRate = 1f;

    public Transform[] ProductionLines ;
    public List<Product> childsproducts = new List<Product>();
    public Product product;
    
    private void Awake()
    {
        Instance = this;
        StartCoroutine(Production());
    }
  
    public IEnumerator Production()
    {
        while(true)
        {
            if (transform.childCount < 30f)
            {
                if (!Product.isWaiting)
                {
                    Product child = Instantiate(product, ProductionLines[0].transform.position, Quaternion.identity, transform) as Product;
                    childsproducts.Add(child);
                }
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
