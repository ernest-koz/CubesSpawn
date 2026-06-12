using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const int MinSpawnCount = 2;
    private const int MaxSpawnCountExclusive = 7;
    private const float SpawnRangeHorizontal = 3f;
    private const float SpawnHeightMin = 2f;
    private const float SpawnHeightMax = 4f;

    [SerializeField] private CubeFactory _cubeFactory;

    private void Start()
    {
        if (_cubeFactory == null)
        {
            Debug.LogError($"CubeFactory is not assigned on {gameObject.name}", gameObject);
            return;
        }

        int cubesCount = Random.Range(MinSpawnCount, MaxSpawnCountExclusive);

        for (int i = 0; i < cubesCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-SpawnRangeHorizontal, SpawnRangeHorizontal),
                Random.Range(SpawnHeightMin, SpawnHeightMax),
                Random.Range(-SpawnRangeHorizontal, SpawnRangeHorizontal));

            _cubeFactory.Create(spawnPosition, Random.rotation, Vector3.one, CubeSplitChance.InitialSplitChance);
        }
    }
}
