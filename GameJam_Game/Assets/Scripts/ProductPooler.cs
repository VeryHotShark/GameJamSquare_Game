using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPooler : MonoBehaviour
{
    public static ProductPooler  Instance;

    public int size = 20;

    public Product productPrefab;

    public Queue<Product> productQueue = new Queue<Product>();

    void Awake()
    {
        Instance = this;
        CreatePool();
    }

    void CreatePool()
    {
        for (int i = 0; i < size; i++)
        {
            Product product = Instantiate(productPrefab,transform.position,Quaternion.identity) as Product;

            product.transform.parent = transform;
            product.gameObject.SetActive(false);
            //product.Init();

            productQueue.Enqueue(product);
        }
    }

    // Update is called once per frame
    public Product GetPooledObject(Vector3 pos,Quaternion rot)
    {
        if(productQueue.Count > 0)
        {
            Product product = productQueue.Dequeue();
            product.gameObject.SetActive(true);

            product.transform.position = pos;
            product.transform.rotation = rot;

            productQueue.Enqueue(product);
            //product.Reset();
            return product;
        }

        return null;
    }
}
