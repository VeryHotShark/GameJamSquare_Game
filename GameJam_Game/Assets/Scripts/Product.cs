using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{

    public int workZoneCounter;

    public static bool isWaiting;
    public LayerMask workZoneLayer;

    public float moveSpeed;
    public float threshold;

    public int quality;
    int currentIndex = 0;
    int targetIndex = 1;

    Transform[] waypoints;

    Vector3 dir;

    WorkPlace m_currentWorkPlace;

    public void Start()
    {
        waypoints = ProductionLine.Instance.ProductionLines;
        GetNextWaypoint();
    }

    void GetNextWaypoint()
    {
        dir = (waypoints[targetIndex].position - waypoints[currentIndex].position).normalized;
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, waypoints[targetIndex].position) < threshold)
        {
            SnapToPoint();
            IncreaseIndex();
            GetNextWaypoint();
            return;
        }
    }

    void SnapToPoint()
    {
        transform.position = waypoints[targetIndex].position;
    }

    void IncreaseIndex()
    {
        if (targetIndex + 1 < ProductionLine.Instance.ProductionLines.Length)
        {
            currentIndex++;
            targetIndex = currentIndex + 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(1 < other.gameObject.layer ==  1 < workZoneLayer)
        {
            transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            m_currentWorkPlace = other.GetComponentInParent<WorkPlace>();
            StartCoroutine(WaitRoutine());
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        workZoneCounter++;
        m_currentWorkPlace = null;  
    }

    IEnumerator WaitRoutine()
    {
        isWaiting = true;
        List<Product> productsBefore = new List<Product>();
        float initMoveSpeed = moveSpeed;
        moveSpeed = 0f;

        MakeProductsBeforeWait(productsBefore);

        yield return new WaitForSeconds(m_currentWorkPlace.workTime);

        foreach (var product in productsBefore)
        {
            product.moveSpeed = initMoveSpeed;
        }

        moveSpeed = initMoveSpeed;
        isWaiting = false;
    }

    void MakeProductsBeforeWait(List<Product> products)
    {
        List<Product> productsList = ProductionLine.Instance.childsproducts;

        int myIndex = productsList.IndexOf(this);
        int listCount = productsList.Count;

        for (int i = myIndex; i < listCount; i++)
        {
            if(productsList[i].workZoneCounter == workZoneCounter)
                products.Add(productsList[i]);
        }

        foreach (var product in products)
        {
            product.moveSpeed = 0f;
        }

    }

}
