using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeSplitChance))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class CubeSplitExecutor : MonoBehaviour
{
    private const int MinSpawnCount = 2;
    private const int MaxSpawnCountExclusive = 7;
    private const float ScaleReductionFactor = 2f;
    private const float VerticalSpawnOffsetMin = 0.5f;
    private const float VerticalSpawnOffsetMax = 1.5f;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _explosionForce = 6f;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _upwardsModifier = 0.3f;

    private CubeSplitChance _splitChance;
    private bool _isProcessing;

    private void Awake()
    {
        _splitChance = GetComponent<CubeSplitChance>();
    }

    private void OnEnable()
    {
        _splitChance.SplitSucceeded += HandleSplitSucceeded;
        _splitChance.SplitFailed += HandleSplitFailed;
    }

    private void OnDisable()
    {
        _splitChance.SplitSucceeded -= HandleSplitSucceeded;
        _splitChance.SplitFailed -= HandleSplitFailed;
    }

    private void HandleSplitSucceeded(float nextSplitChance)
    {
        if (_isProcessing)
            return;

        if (_cubePrefab == null)
            return;

        _isProcessing = true;

        Vector3 spawnCenter = transform.position;
        Vector3 newScale = transform.localScale / ScaleReductionFactor;
        int cubesCount = UnityEngine.Random.Range(MinSpawnCount, MaxSpawnCountExclusive);

        List<Rigidbody> spawnedRigidbodies = new List<Rigidbody>();

        for (int i = 0; i < cubesCount; i++)
        {
            Vector3 spawnOffset = GetSpawnOffset(newScale);
            GameObject newCube = Instantiate(_cubePrefab, spawnCenter + spawnOffset, UnityEngine.Random.rotation);

            PrepareCube(newCube, newScale, nextSplitChance, spawnedRigidbodies);
        }

        ApplyExplosionTo(spawnedRigidbodies, spawnCenter);
        Destroy(gameObject);
    }

    private void HandleSplitFailed()
    {
        if (_isProcessing)
            return;

        _isProcessing = true;
        Destroy(gameObject);
    }

    private Vector3 GetSpawnOffset(Vector3 newScale)
    {
        return new Vector3(
            UnityEngine.Random.Range(-newScale.x, newScale.x),
            UnityEngine.Random.Range(VerticalSpawnOffsetMin, VerticalSpawnOffsetMax),
            UnityEngine.Random.Range(-newScale.z, newScale.z));
    }

    private void PrepareCube(
        GameObject newCube,
        Vector3 newScale,
        float nextSplitChance,
        List<Rigidbody> spawnedRigidbodies)
    {
        newCube.transform.localScale = newScale;

        CubeSplitChance splitChance = newCube.GetComponent<CubeSplitChance>();
        Renderer cubeRenderer = newCube.GetComponent<Renderer>();
        Rigidbody cubeRigidbody = newCube.GetComponent<Rigidbody>();

        if (splitChance != null)
            splitChance.Initialize(nextSplitChance);

        if (cubeRenderer != null)
            cubeRenderer.material.color = UnityEngine.Random.ColorHSV();

        if (cubeRigidbody != null)
            spawnedRigidbodies.Add(cubeRigidbody);
    }

    private void ApplyExplosionTo(List<Rigidbody> rigidbodies, Vector3 explosionPosition)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(
                _explosionForce,
                explosionPosition,
                _explosionRadius,
                _upwardsModifier,
                ForceMode.Impulse);
        }
    }
}