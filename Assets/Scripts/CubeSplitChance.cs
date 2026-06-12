using UnityEngine;

public class CubeSplitChance : MonoBehaviour
{
    public const float InitialSplitChance = 1f;
    private const float ChanceReductionFactor = 2f;

    [SerializeField] private float _currentSplitChance = InitialSplitChance;

    public bool CanSplit => Random.value <= _currentSplitChance;
    public float NextChance => _currentSplitChance / ChanceReductionFactor;

    public void Initialize(float splitChance)
    {
        _currentSplitChance = splitChance;
    }
}
