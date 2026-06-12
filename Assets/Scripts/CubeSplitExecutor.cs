using UnityEngine;

[RequireComponent(typeof(CubeSplitChance))]
[RequireComponent(typeof(CubeExplosion))]

public class CubeSplitExecutor : MonoBehaviour
{
    private const int MinSplitCubes = 2;
    private const int MaxSplitCubesExclusive = 7;

    [SerializeField] private Cube _cubePrefab;

    private CubeSplitChance _splitChance;
    private CubeExplosion _explosion;
    private bool _isProcessing;

    private void Awake()
    {
        _splitChance = GetComponent<CubeSplitChance>();
        _explosion = GetComponent<CubeExplosion>();
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
        {
            Debug.LogError($"Cube prefab is not assigned on {gameObject.name}", gameObject);
            return;
        }

        _isProcessing = true;

        Vector3 newScale = transform.localScale / 2f;
        int cubesCount = Random.Range(MinSplitCubes, MaxSplitCubesExclusive);

        for (int i = 0; i < cubesCount; i++)
        {
            Vector3 spawnOffset = new Vector3(
                UnityEngine.Random.Range(-newScale.x, newScale.x),
                UnityEngine.Random.Range(0.5f, 1.5f),
                UnityEngine.Random.Range(-newScale.z, newScale.z));

            Cube newCube = Instantiate(
                _cubePrefab,
                transform.position + spawnOffset,
                UnityEngine.Random.rotation);

            newCube.Initialize(newScale, nextSplitChance);
        }

        Destroy(gameObject);
    }

    private void HandleSplitFailed()
    {
        if (_isProcessing)
            return;

        _isProcessing = true;

        _explosion.Explode();
        Destroy(gameObject);
    }
}