using System;
using UnityEngine;

[RequireComponent(typeof(CubeClickInput))]

public class CubeSplitChance : MonoBehaviour
{
    public const float InitialSplitChance = 1f;
    private const float ChanceReductionFactor = 2f;

    [SerializeField] private float _currentSplitChance = InitialSplitChance;

    private CubeClickInput _input;

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
        if (UnityEngine.Random.value <= _currentSplitChance)
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