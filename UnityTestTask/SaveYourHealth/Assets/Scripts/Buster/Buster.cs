using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buster : MonoBehaviour
{
    public delegate void BusterActivate();
    public static event BusterActivate BusterActivateEvent;
    private void OnMouseDown()
    {
        BusterActivateEvent?.Invoke();
        Destroy(gameObject);
    }
}
