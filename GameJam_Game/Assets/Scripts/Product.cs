using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{

    public int workZoneCounter;
    MeshFilter myMesh;
    MeshRenderer myMaterial;
    public static bool isWaiting;
    public LayerMask workZoneLayer;
    public bool activepause;

    public float moveSpeed;
    public float threshold;
    public float rayY;
    public float quality = 100;
    int currentIndex = 0;
    int targetIndex = 1;
    float initialSpeed;
    public bool stuck;

    public LayerMask productLayer;
    public Vector3 rayCastTarget;

    bool castRay;

    public bool useSecondWaypoint;
    public bool useThirdWaypoint;
    Transform[] waypoints;
    Transform[] waypoints2;
    Transform[] waypoints3;
    Vector3 dir;
    Vector3 lastWaypoint;
    RaycastHit hit;

    WorkPlace m_currentWorkPlace;

    public void Start()
    {
        myMesh = GetComponent<MeshFilter>();
        myMaterial = GetComponent<MeshRenderer>();

        initialSpeed = moveSpeed;
        if(useSecondWaypoint)
        {
            waypoints = ProductionLine.Instance.ProductionLines2;
        }
        else if ( useThirdWaypoint)
        {
            waypoints = ProductionLine.Instance.ProductionLines3;
        }
        else
        {
            waypoints = ProductionLine.Instance.ProductionLines;
        }
        lastWaypoint = waypoints[currentIndex].position;
        GetNextWaypoint();
        //activepause = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DragAndDrop>().ActivePause;
    }

    void GetPreviousWaypoint()
    {
        lastWaypoint = waypoints[currentIndex - 1].position;
    }

    public void SetSpeed()
    {
        Debug.Log("t");
        moveSpeed = initialSpeed;
    }

    void GetNextWaypoint()
    {
        dir = (waypoints[targetIndex].position - waypoints[currentIndex].position).normalized;
    }
    void ResettingProduct()
    {
        if (transform.position == waypoints[waypoints.Length - 1].position)
        {
            transform.position = waypoints[0].position;
            //Mesh Reset
        }
    }
    void Update()
    {
            if (moveSpeed == 0.0f && !isWaiting)
            {
                stuck = true;
            }
            Debug.DrawLine(transform.position, lastWaypoint);
            if (activepause == false || !stuck)

                transform.Translate(dir * Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, waypoints[waypoints.Length - 1].position) < threshold)
            {
                moveSpeed = 0;
            }
            if (Vector3.Distance(transform.position, waypoints[targetIndex].position) < threshold)
            {
                SnapToPoint();
                IncreaseIndex();
                GetNextWaypoint();
                GetPreviousWaypoint();
                return;
            }

            if (stuck)
            {
                if (Physics.Raycast(transform.position, dir, out hit, 2.5f, productLayer))
                {
                    Product p = hit.transform.GetComponent<Product>();
                    if (hit.transform.gameObject.layer == 11)
                    {
                        moveSpeed = 0f;
                        if (!p.stuck && stuck)
                        {
                            moveSpeed = initialSpeed;
                            stuck = false;
                        }
                    }
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, dir, out hit, 2.0f, productLayer))
                {
                    Product p = hit.transform.GetComponent<Product>();
                    if (hit.transform.gameObject.layer == 11)
                    {
                        moveSpeed = 0f;
                        if (!p.stuck && stuck)
                        {
                            moveSpeed = initialSpeed;
                            stuck = false;
                        }
                    }
                }
            }
        
    }

    void SnapToPoint()
    {
        transform.position = waypoints[targetIndex].position;
    }

    void IncreaseIndex()
    {
        if (targetIndex == waypoints.Length - 1) { }
        else if (targetIndex + 1< ProductionLine.Instance.ProductionLines.Length)
        {
            currentIndex++;
            targetIndex = currentIndex + 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 || other.gameObject.layer == 10)
        {
            if (other.gameObject.layer == 10)
            {
                myMesh.mesh = other.GetComponentInParent<WorkPlace>().changemesh;
                myMaterial.material = other.GetComponentInParent<WorkPlace>().ChangeMaterial;
            }
            if (1 < other.gameObject.layer == 1 < workZoneLayer)
            {
                TriggerWorkAnimation(other);

                transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
                m_currentWorkPlace = other.GetComponentInParent<WorkPlace>();
                if (m_currentWorkPlace.finalWorkSpeed < 1)
                {
                    quality -= quality * .15f;
                }
                else if (m_currentWorkPlace.finalWorkSpeed > 1)
                {
                    quality += quality * .15f;
                }
                if (quality > 100)
                    quality = 100;
                StartCoroutine(WaitRoutine());
                return;
            }
        }
    }

    void TriggerWorkAnimation(Collider other)
    {
        WorkPlace workPlace = other.GetComponentInParent<WorkPlace>();
        workPlace.TriggerAnimation();
    }

    private void OnTriggerExit(Collider other)
    {
        workZoneCounter++;
        
        m_currentWorkPlace = null;
        stuck = false;
    }

    IEnumerator WaitRoutine()
    {
        isWaiting = true;
       // List<Product> productsBefore = new List<Product>();
        moveSpeed = 0f;

        //MakeProductsBeforeWait(productsBefore);

        yield return new WaitForSeconds(m_currentWorkPlace.workTime);
        /*
        foreach (var product in productsBefore)
        {
            product.moveSpeed = initMoveSpeed;
        }
        */
        stuck = false;
        yield return new WaitForSeconds(0.01f);
        moveSpeed = initialSpeed;
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
