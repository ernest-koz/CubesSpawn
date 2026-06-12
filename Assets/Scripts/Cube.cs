using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour, IClickable
{
    public const float InitialSplitChance = 1f;

    [SerializeField] private float _currentSplitChance = InitialSplitChance;
    [SerializeField] private float _chanceReductionFactor = 2f;
    private Renderer _renderer;

    public bool CanSplit => Random.value <= _currentSplitChance;
    public float NextSplitChance => _currentSplitChance / _chanceReductionFactor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(Vector3 scale, float splitChance)
    {
        transform.localScale = scale;
        _currentSplitChance = splitChance;
        _renderer.material.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f, 1f, 1f);
    }
}
