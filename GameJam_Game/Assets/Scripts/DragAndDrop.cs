﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] float m_groundOffset = 0.2f;
    [SerializeField] float m_yGrabOffset = 2;
    [Space]
    [SerializeField] LayerMask draggableLayer;
    [SerializeField] LayerMask workZoneLayer;
    [Space]
    [SerializeField] Transform m_currentSelection;
    [SerializeField] Transform m_landIndicator;

    [SerializeField] Transform m_currentWorkZone;
    Vector3 m_initWorkerPos;

    Camera m_cam;

    Ray m_camRay;
    RaycastHit m_camRayHitInfo;

    Ray m_workerRay;
    RaycastHit m_workerHitInfo;

    float m_selectionZValue;

    WorkPlace workPlace;
    Worker worker;

    bool isHolding;

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
    }

    void GetComponents()
    {
        m_cam = GetComponent<Camera>();
    }

    void Update()
    {
        CheckForMouseOver();

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }

        if (m_currentSelection != null)
        {
            UpdateSelectionPosition();

            if (Input.GetMouseButtonUp(0))
            {
                OnMouseRelease();
            }
        }

    }

    void CheckForMouseOver()
    {
        m_camRay = m_cam.ScreenPointToRay(Input.mousePosition);

        bool hitSomething = Physics.Raycast(m_camRay, out m_camRayHitInfo, 100f, draggableLayer);

        if(hitSomething && !isHolding)
        {
            worker = m_camRayHitInfo.transform.GetComponent<Worker>();

            if(worker!= null)
                PopUpInfoController.Instance.ShowWorkersInfo(worker.strength,worker.dexterity,worker.intelligence,worker.name);
        }
        else if(!hitSomething && !isHolding)
        {
            PopUpInfoController.Instance.CloseWorkersInfo();
            return;
        }
    }
    
    void OnMouseRelease()
    {
        isHolding = false;
        bool hitWorkZone = CheckIfHitZone();

        if(hitWorkZone)
        {
            AssignWorkerToWorkPlace();
            SnapToWorkZone();
        }
        else
        {
            m_currentSelection.position = m_initWorkerPos;
            m_landIndicator.position = new Vector3(m_initWorkerPos.x,m_landIndicator.position.y,m_initWorkerPos.z);
        }
        //m_currentSelection.position -= Vector3.up * m_yGrabOffset;
        m_currentSelection = null;

    }

    bool CheckIfHitZone()
    {
        m_workerRay = new Ray(m_currentSelection.position,Vector3.down);

        bool hitWorkZone = Physics.Raycast(m_workerRay, out m_workerHitInfo, 10f, workZoneLayer);

        return hitWorkZone;
    }

    void SnapToWorkZone()
    {
        m_currentSelection.position = new Vector3(m_workerHitInfo.transform.position.x,m_initWorkerPos.y,m_workerHitInfo.transform.position.z);
        m_landIndicator.position = new Vector3(m_workerHitInfo.transform.position.x,m_landIndicator.position.y,m_workerHitInfo.transform.position.z);
    }

    void AssignWorkerToWorkPlace()
    {
        if(workPlace != null)
            workPlace.RemoveWorker();

        worker = m_camRayHitInfo.transform.GetComponent<Worker>();
        workPlace = m_workerHitInfo.transform.GetComponent<WorkPlace>();

        workPlace.AddWorker(worker);
        worker.AddWorkPlace(workPlace);

        workPlace.CompareStats();
        workPlace.SetWorkerSpeed();
    }

    void OnMouseClick()
    {
        isHolding = true;
        CreateRay();
    }

    void CreateRay()
    {
        m_camRay = m_cam.ScreenPointToRay(Input.mousePosition);

        bool hitSomething = Physics.Raycast(m_camRay, out m_camRayHitInfo, 100f, draggableLayer);

        if (hitSomething)
        {
            m_currentSelection = m_camRayHitInfo.transform;
            m_initWorkerPos = m_currentSelection.transform.position;
            m_landIndicator.position = new Vector3(m_currentSelection.position.x,m_groundOffset,m_currentSelection.position.z);
            m_currentSelection.position += Vector3.up * m_yGrabOffset;

            worker = m_camRayHitInfo.transform.GetComponent<Worker>();

            if(worker != null)
                PopUpInfoController.Instance.ShowWorkersInfo(worker.strength,worker.dexterity,worker.intelligence,worker.name);
        }
    }

    void UpdateSelectionPosition()
    {
        Vector3 mouseWorldPos = Input.mousePosition;
        m_selectionZValue = m_cam.WorldToScreenPoint(m_currentSelection.position).z;

        mouseWorldPos.z = m_selectionZValue;
        mouseWorldPos = m_cam.ScreenToWorldPoint(mouseWorldPos);

        m_currentSelection.position = new Vector3(mouseWorldPos.x,m_currentSelection.position.y,mouseWorldPos.z);
        m_landIndicator.position = new Vector3(mouseWorldPos.x,m_landIndicator.position.y,mouseWorldPos.z);
    }
}
