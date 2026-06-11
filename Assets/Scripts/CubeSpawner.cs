using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const int MinSpawnCount = 2;
    private const int MaxSpawnCountExclusive = 7;
    private const float SpawnRangeHorizontal = 3f;
    private const float SpawnHeightMin = 2f;
    private const float SpawnHeightMax = 4f;
    private const float InitialSplitChance = 1f;

    [SerializeField] private GameObject _cubePrefab;

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

            GameObject cube = Instantiate(_cubePrefab, spawnPosition, UnityEngine.Random.rotation);
            PrepareCube(cube, Vector3.one, InitialSplitChance);
        }
    }

    private void PrepareCube(GameObject cube, Vector3 scale, float splitChance)
    {
        cube.transform.localScale = scale;

        CubeSplitChance cubeSplitChance = cube.GetComponent<CubeSplitChance>();
        Renderer cubeRenderer = cube.GetComponent<Renderer>();

        if (cubeSplitChance != null)
            cubeSplitChance.Initialize(splitChance);

        if (cubeRenderer != null)
            cubeRenderer.material.color = UnityEngine.Random.ColorHSV();
    }
}