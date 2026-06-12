using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    private const int MinSplitCubes = 2;
    private const int MaxSplitCubesExclusive = 7;
    private const float ScaleReductionFactor = 2f;

    [SerializeField] private Cube _cubePrefab;

    public Cube Create(Vector3 position, Quaternion rotation, Vector3 scale, float splitChance)
    {
        Cube cube = Instantiate(_cubePrefab, position, rotation);
        cube.Initialize(scale, splitChance);
        return cube;
    }

    public void SpawnSplit(Vector3 originPosition, Vector3 parentScale, float splitChance)
    {
        Vector3 newScale = parentScale / ScaleReductionFactor;
        int count = Random.Range(MinSplitCubes, MaxSplitCubesExclusive);

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-newScale.x, newScale.x),
                Random.Range(0.5f, 1.5f),
                Random.Range(-newScale.z, newScale.z));

            Create(originPosition + offset, Random.rotation, newScale, splitChance);
        }
    }
}
