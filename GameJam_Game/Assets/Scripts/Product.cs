using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    Vector3 currentTarget;
    Vector3 direction;
    bool Travel = true;
    float WaitInStation;
    float MySpeed =5;
    int parentlength;
    Vector3 parentposition;
    float dist;
    Mesh mymesh;
    public int i = 1;
    void Awake()
    {
        parentlength = transform.parent.GetComponent<ProductionLine>().ProductionLines.Length;
        parentposition = transform.parent.GetComponent<ProductionLine>().ProductionLines[0].transform.position;
        mymesh = GetComponent<Mesh>();
    }
    void Update()
    {
        currentTarget = transform.parent.GetComponent<ProductionLine>().ProductionLines[i].transform.position;
        dist = Vector3.Distance(transform.position, currentTarget);
        if (dist<.05f)
        {
           StartCoroutine( WaitForSomeTime(WaitInStation));
            i += 1;
        }
        if(i == parentlength)
        {
            transform.position = parentposition ;
        }
        if (i > parentlength - 1)
            i = 0;

        Vector3 dir = (currentTarget - transform.position).normalized;
        if(Travel)
        transform.Translate(dir * Time.deltaTime* MySpeed);
    }

    IEnumerator WaitForSomeTime(float amount)
    {
        Travel = false;
        yield return new WaitForSeconds(amount);
        Travel = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("sadasd");
        if (other.tag == "workstation")
        {
            GetComponent<MeshFilter>().mesh = other.GetComponent<WorkStation>().Mesh;
        }
    }
}
