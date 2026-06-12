using UnityEngine;

public class CubeSplitExecutor : MonoBehaviour
{
    [SerializeField] private InputRaycaster _inputRaycaster;
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private CubeExplosion _cubeExplosion;

    private void OnEnable()
    {
        if (_inputRaycaster == null)
        {
            Debug.LogError($"InputRaycaster is not assigned on {gameObject.name}", gameObject);
            return;
        }

        _inputRaycaster.ClickableClicked += OnClickableClicked;
    }

    private void OnDisable()
    {
        if (_inputRaycaster == null)
            return;

        _inputRaycaster.ClickableClicked -= OnClickableClicked;
    }

    private void OnClickableClicked(IClickable clickable)
    {
        if (clickable is not Cube cube)
            return;

        if (cube.CanSplit)
        {
            _cubeFactory.SpawnSplit(cube.transform.position, cube.transform.localScale, cube.NextSplitChance);
        }
        else
        {
            _cubeExplosion.Explode(cube.transform.position, cube.transform.localScale);
        }

        Destroy(cube.gameObject);
    }
}
