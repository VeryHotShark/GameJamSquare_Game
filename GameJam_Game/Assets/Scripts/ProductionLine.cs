using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionLine : MonoBehaviour
{
    public static ProductionLine Instance;

    public float spawnRate = 1f;
    float flexiblespawn;

    public Transform[] ProductionLines ;
    public List<Product> childsproducts = new List<Product>();
    public Product product;
    private Coroutine waypointHandle;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(Production());
    }
    public IEnumerator Production()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            if (transform.childCount < 30)
            {
                if (!Product.isWaiting&&!DragAndDrop.ActivePause)
                {
                    
                    Product child = Instantiate(product, ProductionLines[0].transform.position, Quaternion.identity, transform) as Product;
                    childsproducts.Add(child);
                }
            }
            
        }
    }
}
