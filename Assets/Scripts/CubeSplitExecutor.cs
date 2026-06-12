using UnityEngine;

[RequireComponent(typeof(CubeSplitChance))]
[RequireComponent(typeof(CubeExplosion))]
public class CubeSplitExecutor : MonoBehaviour, IClickable
{
    private CubeSplitChance _splitChance;
    private CubeExplosion _explosion;
    private CubeFactory _cubeFactory;
    private bool _isProcessing;

    private void Awake()
    {
        _splitChance = GetComponent<CubeSplitChance>();
        _explosion = GetComponent<CubeExplosion>();
    }

    private void Start()
    {
        _cubeFactory = FindObjectOfType<CubeFactory>();
    }

    public void HandleClick()
    {
        if (_isProcessing)
            return;

        _isProcessing = true;

        if (_splitChance.CubeCanSplit)
        {
            if (_cubeFactory == null)
            {
                Debug.LogError($"CubeFactory not found in scene!", gameObject);
                return;
            }

            _cubeFactory.SpawnSplit(transform.position, transform.localScale, _splitChance.CubeSplitNextChance);
        }
        else
        {
            _explosion.Explode();
        }

        Destroy(gameObject);
    }
}
