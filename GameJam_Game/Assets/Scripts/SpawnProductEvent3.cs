﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProductEvent3 : MonoBehaviour
{
    public void SpawnSand3()
    {
        if (Connector.first.GetComponent<Product>().useSecondWaypoint != false && Connector.second.GetComponent<Product>().useSecondWaypoint != true)
        {
            Debug.Log("asd");
            Connector.first.SetActive(false);
            Connector.second.SetActive(false);
            Connector.RemoveProducts();
            ProductionLine.Instance.SpawnProduct3();
        }
    }
}
