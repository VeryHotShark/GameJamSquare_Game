using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    Vector3 currentTarget;
    bool Travel = true;
    float WaitInStation = .1f;
    float MySpeed =4;
    int parentlength;
    Vector3 parentposition;
    Mesh mymesh;
    public int i = 1;

    ProductionLine productionLine;

    int myIndex;

    void Awake()
    {
        productionLine = transform.parent.GetComponent<ProductionLine>();

        currentTarget = productionLine.ProductionLines[1].transform.position;
        parentlength = productionLine.ProductionLines.Count;
        mymesh = GetComponent<Mesh>();
        myIndex = productionLine.childsproducts.IndexOf(transform.gameObject);
        if (myIndex == -1)
            myIndex = 0;
        print(myIndex);
        
    }
    void FixedUpdate()
    {
       
        if (Vector3.Distance(transform.position, productionLine.childsproducts[myIndex - 1].transform.position) <1)
        {
            Travel = false;
        }
            else
        {
            Travel = true;
        }
        float dist = Vector3.Distance(transform.position, currentTarget);
        if (dist<.1f)
        {
            i += 1;
            if (i == parentlength)
            {
                i = 0;
            }
            StartCoroutine(WaitForSomeTime(WaitInStation));
            currentTarget = transform.parent.GetComponent<ProductionLine>().ProductionLines[i].transform.position;
        }
        
        
        Vector3 dir = (currentTarget - transform.position).normalized;
        if (Travel)
            transform.Translate(dir * Time.deltaTime * MySpeed);


    }

    IEnumerator WaitForSomeTime(float amount)
    {
        Travel = false; 
        yield return new WaitForSeconds(amount);
        Travel = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "workstation")
        {
            GetComponent<MeshFilter>().mesh = other.GetComponent<WorkStation>().Mesh;
        }
    }
}
