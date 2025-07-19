using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClientAnimation : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        animator.SetTrigger("Ide");
    }

    public void PlayEntrance()
    {
        animator.SetTrigger("Entrance");
    }

    public void PlayLeave()
    {
        animator.SetTrigger("Leave");
    }

    public void PlayAngry()
    {
        animator.SetTrigger("Angry");
    }

    public void PlayHappy()
    {
        animator.SetTrigger("Happy");
    }
}
