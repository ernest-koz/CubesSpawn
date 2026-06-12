using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCountExclusive = 7;
    [SerializeField] private float _spawnRangeHorizontal = 3f;
    [SerializeField] private float _spawnHeightMin = 2f;
    [SerializeField] private float _spawnHeightMax = 4f;

    private void Start()
    {
        if (_cubeFactory == null)
        {
            Debug.LogError($"CubeFactory is not assigned on {gameObject.name}", gameObject);
            return;
        }

        int cubesCount = Random.Range(_minSpawnCount, _maxSpawnCountExclusive);

        for (int i = 0; i < cubesCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-_spawnRangeHorizontal, _spawnRangeHorizontal),
                Random.Range(_spawnHeightMin, _spawnHeightMax),
                Random.Range(-_spawnRangeHorizontal, _spawnRangeHorizontal));

            _cubeFactory.Create(spawnPosition, Random.rotation, Vector3.one, Cube.InitialSplitChance);
        }
    }
}
