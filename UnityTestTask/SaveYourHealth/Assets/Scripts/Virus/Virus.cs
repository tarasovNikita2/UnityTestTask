using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    protected Animator animator;
    protected Vector2 direction;
    protected int speed = 1;
    protected int hp = 4;

    protected bool alreadyDestroyed = false;

    protected void SubscribeOnEvent()
    {
        Blue.DestroyActionEvent += IncreaseHealth;
    }
    private void OnMouseDown()
    {
        if (!alreadyDestroyed)
        {
            animator.SetTrigger("TakeHit");
            hp--;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Vertical":
            {
                direction.y *= -1;
                break;
            }
            case "Horizontal":
            {
                direction.x *= -1;
                break;
            }
            default:
                direction.x *= -1;
                direction.y *= -1;
                break;
        }
    }

    private void IncreaseHealth()
    {
        hp += 2;
    }
}
