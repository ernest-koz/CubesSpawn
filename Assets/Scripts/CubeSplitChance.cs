using System;
using UnityEngine;

[RequireComponent(typeof(CubeClickInput))]

public class CubeSplitChance : MonoBehaviour
{
    private const float InitialSplitChance = 1f;
    private const float ChanceReductionFactor = 2f;

    [SerializeField] private float _currentSplitChance = InitialSplitChance;

    private CubeClickInput _input;

    public float CurrentSplitChance => _currentSplitChance;

    public event Action<float> SplitSucceeded;
    public event Action SplitFailed;

    private void Awake()
    {
        _input = GetComponent<CubeClickInput>();
    }

    private void OnEnable()
    {
        _input.Clicked += CheckSplit;
    }

    private void OnDisable()
    {
        _input.Clicked -= CheckSplit;
    }

    public void Initialize(float splitChance)
    {
        _currentSplitChance = splitChance;
    }

    private void CheckSplit()
    {
        float roll = UnityEngine.Random.value;

        if (roll <= _currentSplitChance)
        {
            float nextSplitChance = _currentSplitChance / ChanceReductionFactor;
            SplitSucceeded?.Invoke(nextSplitChance);
        }
        else
        {
            SplitFailed?.Invoke();
        }
    }
}