using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Green : Virus
{
    public delegate void DestroyAction();
    public static event DestroyAction DestroyActionEvent;
    
    public delegate void DuplicateAction();
    public static event DuplicateAction DuplicateActionEvent;
    
    public Rigidbody2D rigidbody;
    public GameObject greenVirus;
    void Start()
    {
        Buster.BusterActivateEvent += DestroyVirus;
        Red.DestroyActionEvent += Duplicate;
        
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

    void Duplicate()
    {
        Instantiate(greenVirus, transform.position, Quaternion.identity);
        DuplicateActionEvent?.Invoke();
    }
    
    protected void DestroyVirus()
    {
        animator.SetTrigger("Death");
        DestroyActionEvent?.Invoke();
        alreadyDestroyed = true;
        Destroy(gameObject,animator.GetCurrentAnimatorClipInfo(0).LongLength);
    }

    private void OnDestroy()
    {
        Buster.BusterActivateEvent -= DestroyVirus;
        Red.DestroyActionEvent -= Duplicate;
    }
}
