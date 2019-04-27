using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedWorkers : MonoBehaviour
{   public int numberofworkers = 4;
    public List<Worker> childsworkers = new List<Worker>();
    public Worker worker;
    void Start()
    {
        for(int i =0;i<numberofworkers;i++)
        {
            Worker child = Instantiate(worker, Vector3.zero, Quaternion.identity, transform) as Worker;
            childsworkers.Add(child);
            Randomizing_stats(i);
        }
    }
    void Randomizing_stats(int j)
    {
        transform.GetChild(j).GetComponent<Worker>().dexterity = Random.Range(1, 4);
        transform.GetChild(j).GetComponent<Worker>().intelligence = Random.Range(1, 4);
        transform.GetChild(j).GetComponent<Worker>().strength = Random.Range(1, 4);
    }
}
