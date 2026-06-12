using UnityEngine;

public class CubeSplitExecutor : MonoBehaviour
{
    [SerializeField] private InputRaycaster _inputRaycaster;
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private CubeExplosion _cubeExplosion;

    private void OnEnable()
    {
        _inputRaycaster.ClickableClicked += OnClickableClicked;
    }

    private void OnDisable()
    {
        _inputRaycaster.ClickableClicked -= OnClickableClicked;
    }

    private void OnClickableClicked(IClickable clickable)
    {
        if (clickable is not Cube cube)
            return;

        if (cube.CubeCanSplit)
        {
            _cubeFactory.SpawnSplit(cube.transform.position, cube.transform.localScale, cube.CubeSplitNextChance);
        }
        else
        {
            _cubeExplosion.Explode(cube.transform.position, cube.transform.localScale);
        }

        Destroy(cube.gameObject);
    }
}
