using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProductEvent : MonoBehaviour
{
    public void SpawnSand()
    {
        ProductionLine.Instance.SpawnProduct();

    }
}
