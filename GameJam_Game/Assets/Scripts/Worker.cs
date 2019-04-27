using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{

    [SerializeField] float m_workSpeed;

    public string workerName;
    [Range(1,3)] public int intelligence = 1;
    [Range(1,3)] public int strength = 1;
    [Range(1,3)] public int dexterity = 1;

    [SerializeField]WorkPlace m_currentWorkPlace;

    public void AddWorkPlace(WorkPlace workPlace)
    {
        m_currentWorkPlace = workPlace;
    }

    public void RemoveWorkPlace()
    {
        m_currentWorkPlace = null;
    }

    public void SetWorkSpeed(float speed)
    {
        m_workSpeed =  speed;
    }

}
