using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Blue : Virus
{
    public delegate void DestroyAction();

    public static event DestroyAction DestroyActionEvent;
    
    public Rigidbody2D rigidbody;

    void Start()
    {
        Buster.BusterActivateEvent += DestroyVirus;
        SubscribeOnEvent();
        animator = GetComponent<Animator>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1, 1));
    }
    
    void FixedUpdate()
    {
        if (hp == 0 && !alreadyDestroyed)
        {
            DestroyVirus();
        }

        rigidbody.velocity = direction.normalized * speed;
    }
    protected void DestroyVirus()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject,animator.GetCurrentAnimatorClipInfo(0).LongLength);
        DestroyActionEvent?.Invoke();
        alreadyDestroyed = true;
        
        Buster.BusterActivateEvent -= DestroyVirus;
    }

    private void OnDestroy()
    {
        Buster.BusterActivateEvent -= DestroyVirus;
    }
}
