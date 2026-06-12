using UnityEngine;

[RequireComponent(typeof(CubeSplitChance))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private CubeSplitChance _splitChance;
    private Renderer _renderer;

    private void Awake()
    {
        _splitChance = GetComponent<CubeSplitChance>();
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(Vector3 scale, float splitChance)
    {
        transform.localScale = scale;
        _splitChance.Initialize(splitChance);
        Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f, 1f, 1f);
        _renderer.material.color = randomColor;
    }
}
