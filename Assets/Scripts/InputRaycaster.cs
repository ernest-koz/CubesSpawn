using System;
using UnityEngine;

public class InputRaycaster : MonoBehaviour
{
    public event Action<IClickable> ClickableClicked;

    [SerializeField] private LayerMask _cubeLayer = -1;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _cubeLayer))
        {
            if (hit.collider.TryGetComponent(out IClickable clickable))
            {
                ClickableClicked?.Invoke(clickable);
            }
        }
    }
}
