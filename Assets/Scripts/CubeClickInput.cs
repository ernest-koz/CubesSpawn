using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class CubeClickInput : MonoBehaviour
{
    public event Action Clicked;

    private void OnMouseDown()
    {
        Clicked?.Invoke();
    }
}