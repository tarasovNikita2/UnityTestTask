using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meta : Virus
{
    public Rigidbody2D rigidbody;
    public GameObject RedVirus;
    void Start()
    {
        Buster.BusterActivateEvent += DestroyVirus;
        animator = GetComponent<Animator>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1, 1));
        SubscribeOnEvent();
    }
    
    private void FixedUpdate()
    {
        if (hp == 0 && !alreadyDestroyed)
        {
            DestroyVirus();
        }

        rigidbody.velocity = direction.normalized * speed;
    }
    
    private void DestroyVirus()
    {
        animator.SetTrigger("Death");
        Instantiate(RedVirus, transform.position, Quaternion.identity);
        alreadyDestroyed = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Buster.BusterActivateEvent -= DestroyVirus;
    }
}
