using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    [SerializeField] LayerMask draggableLayer;
    [SerializeField] Transform m_currentSelection;

    Camera m_cam;

    Ray ray;
    RaycastHit m_hitInfo;

    float m_selectionZValue;

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
    }

    void GetComponents()
    {
        m_cam = GetComponent<Camera>();
    }

    void OnMouseRelease()
    {
        m_currentSelection = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }

        if (m_currentSelection != null)
        {
            UpdateSelectionPosition();
        }

         if (Input.GetMouseButtonUp(0))
        {
            OnMouseRelease();
        }
    }

    void OnMouseClick()
    {
        CreateRay();
    }

    void CreateRay()
    {
        ray = m_cam.ScreenPointToRay(Input.mousePosition);

        bool hitSomething = Physics.Raycast(ray, out m_hitInfo, 100f, draggableLayer);

        if (hitSomething)
        {
            m_currentSelection = m_hitInfo.transform;
        }
    }

    void UpdateSelectionPosition()
    {
        Vector3 mouseWorldPos = Input.mousePosition;
        m_selectionZValue = m_cam.WorldToScreenPoint(m_currentSelection.position).z;

        mouseWorldPos.z = m_selectionZValue;
        mouseWorldPos = m_cam.ScreenToWorldPoint(mouseWorldPos);

        m_currentSelection.position = new Vector3(mouseWorldPos.x,m_currentSelection.position.y,mouseWorldPos.z);
    }
}
