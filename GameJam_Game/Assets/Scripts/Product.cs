using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    Vector3 currentTarget;
    Vector3 direction;
    bool Travel;
    float dist;
    public int i = 1;
    void Start()
    {
        Debug.Log(transform.parent.GetComponent<ProductionLine>().ProductionLines.Length - 1);
        
    }
    void Update()
    {
        currentTarget = transform.parent.GetComponent<ProductionLine>().ProductionLines[i].transform.position;
        dist = Vector3.Distance(transform.position, currentTarget);
        if (dist<.05f)
        {
           StartCoroutine( WaitForSomeTime(.5f));
            i += 1;
        }
        if(i == transform.parent.GetComponent<ProductionLine>().ProductionLines.Length)
        {
            transform.position = transform.parent.GetComponent<ProductionLine>().ProductionLines[0].transform.position;
        }
        if (i > transform.parent.GetComponent<ProductionLine>().ProductionLines.Length - 1)
            i = 0;

        Vector3 dir = (currentTarget - transform.position).normalized;
        if(Travel)
        transform.Translate(dir * Time.deltaTime* transform.parent.GetComponent<ProductionLine>().ConveyerSpeed);
    }

    IEnumerator WaitForSomeTime(float amount)
    {
        Travel = false;
        yield return new WaitForSeconds(amount);
        Travel = true;
    }
}
