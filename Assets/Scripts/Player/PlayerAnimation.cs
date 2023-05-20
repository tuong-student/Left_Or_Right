using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class PlayerAnimation : MonoBehaviorInstance<PlayerAnimation>
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sr;

    public void RunUp()
    {
        anim.SetBool("Up", true);
        anim.SetBool("Running", true);
    }

    public void RunSide()
    {
        anim.SetBool("Up", false);
        anim.SetBool("Running", true);
    }

    public void Stop()
    {
        anim.SetBool("Running", false);
    }

    public void Opposite(bool value)
    {
        sr.flipX = value;
    }
}
