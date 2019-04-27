using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnimations : MonoBehaviour
{
    public int index;
    public Animator anim;

    private void Start()
    {
        anim.SetInteger("animIndex", index);
    }
}
