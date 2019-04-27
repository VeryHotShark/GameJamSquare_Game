﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : MonoBehaviour
{
    public float workTime = 2f;
    public float finalWorkSpeed;

    public Mesh changemesh;
    public Material ChangeMaterial;
    [SerializeField]int requiredIntelligence = 2;
    [SerializeField]int requiredStrength = 2;
    [SerializeField]int requiredDexterity = 2;

    bool workSlow;
    bool workNormal;
    bool workFast;

    [SerializeField] Worker m_currentWorker;
    [SerializeField] WorkerAnimations m_workerAnimation;

    public void Start()
    {
        m_workerAnimation.transform.position = this.transform.position - Vector3.up * 0.5f;
        m_workerAnimation.transform.rotation = this.transform.rotation;
    }

    public void OnProductEnter()
    {

    }

    public void RemoveWorker()
    {
        //m_workerAnimation = null;
        m_currentWorker = null;
        workSlow = false;
        workNormal = false;
        workFast = false;
    }

    public void AddWorker(Worker worker)
    {
        m_currentWorker = worker;
        SetWorkerSpeed();
    }

    public void CompareStats()
    {
        if(m_currentWorker != null)
        {
            int counter = 0;

            if(m_currentWorker.intelligence > requiredIntelligence)
                counter++;

            if(m_currentWorker.strength > requiredStrength)
                counter++;

            if(m_currentWorker.dexterity > requiredDexterity)
                counter++;

            switch(counter)
            {
                case 0 :
                    workSlow = true;
                break;
                case 1 :
                    workSlow = true;
                break;
                case 2:
                    workNormal = true;
                break;
                case 3 :
                    workFast = true;
                break;
            }
        }
    }

    public void SetWorkerSpeed()
    {
         finalWorkSpeed = workSlow ? 0.75f : workNormal ? 1f : 1.25f;

        m_currentWorker.SetWorkSpeed(finalWorkSpeed);
    }
   
    public void TriggerAnimation()
    {
        m_workerAnimation.anim.SetInteger("animIndex", m_workerAnimation.index);
        m_workerAnimation.anim.SetTrigger("playAnim");
    }

}
