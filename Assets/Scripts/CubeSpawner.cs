using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const int MinSpawnCount = 2;
    private const int MaxSpawnCountExclusive = 7;
    private const float SpawnRangeHorizontal = 3f;
    private const float SpawnHeightMin = 2f;
    private const float SpawnHeightMax = 4f;

    [SerializeField] private Cube _cubePrefab;

    private void Start()
    {
        if (_cubePrefab == null)
            return;

        int cubesCount = UnityEngine.Random.Range(MinSpawnCount, MaxSpawnCountExclusive);

        for (int i = 0; i < cubesCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                UnityEngine.Random.Range(-SpawnRangeHorizontal, SpawnRangeHorizontal),
                UnityEngine.Random.Range(SpawnHeightMin, SpawnHeightMax),
                UnityEngine.Random.Range(-SpawnRangeHorizontal, SpawnRangeHorizontal));

            Cube cube = Instantiate(_cubePrefab, spawnPosition, UnityEngine.Random.rotation);
            cube.Initialize(Vector3.one, CubeSplitChance.InitialSplitChance);
        }
    }
}