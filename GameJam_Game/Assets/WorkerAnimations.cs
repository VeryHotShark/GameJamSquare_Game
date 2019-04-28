using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnimations : MonoBehaviour
{
    public int index;
    public Animator anim;

    [Space]
    [Space]

    public ParticleSystem fireVFX;
    public Transform mountTransform;
    ParticleSystem ps;


    private void Start()
    {
        anim.SetInteger("animIndex", index);
    }

    public void Flamethower()
    {
        ps = Instantiate(fireVFX, mountTransform.position, mountTransform.rotation) as ParticleSystem;
        ps.Play();

    }

    public void EndFlamethower()
    {
        ps.Stop();
        Destroy(ps.gameObject);
    }


}
