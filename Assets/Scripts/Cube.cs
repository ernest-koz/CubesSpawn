using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour, IClickable
{
    public const float InitialSplitChance = 1f;
    private const float ChanceReductionFactor = 2f;

    [SerializeField] private float _currentSplitChance = InitialSplitChance;

    public bool CubeCanSplit => Random.value <= _currentSplitChance;
    public float CubeSplitNextChance => _currentSplitChance / ChanceReductionFactor;

    public void HandleClick()
    {
    }

    public void Initialize(Vector3 scale, float splitChance)
    {
        transform.localScale = scale;
        _currentSplitChance = splitChance;
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f, 1f, 1f);
    }
}
