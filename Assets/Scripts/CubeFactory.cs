using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minSplitCubes = 2;
    [SerializeField] private int _maxSplitCubesExclusive = 7;
    [SerializeField] private float _scaleReductionFactor = 2f;
    [SerializeField] private float _splitYOffsetMin = 0.5f;
    [SerializeField] private float _splitYOffsetMax = 1.5f;

    public Cube Create(Vector3 position, Quaternion rotation, Vector3 scale, float splitChance)
    {
        Cube cube = Instantiate(_cubePrefab, position, rotation);
        cube.Initialize(scale, splitChance);
        return cube;
    }

    public void SpawnSplit(Vector3 originPosition, Vector3 parentScale, float splitChance)
    {
        Vector3 newScale = parentScale / _scaleReductionFactor;
        int count = Random.Range(_minSplitCubes, _maxSplitCubesExclusive);

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-newScale.x, newScale.x),
                Random.Range(_splitYOffsetMin, _splitYOffsetMax),
                Random.Range(-newScale.z, newScale.z));

            Create(originPosition + offset, Random.rotation, newScale, splitChance);
        }
    }
}
