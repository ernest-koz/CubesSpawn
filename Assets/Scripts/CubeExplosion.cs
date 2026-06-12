using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _baseForce = 10f;
    [SerializeField] private float _baseRadius = 5f;
    [SerializeField] private float _upwardsModifier = 0.3f;
    [SerializeField] private LayerMask _cubeLayer = -1;
    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    public void Explode(Vector3 position, Vector3 scale)
    {
        PlayEffectAt(position);

        float sizeMultiplier = 1f / Mathf.Max(scale.x, scale.y, scale.z);
        float force = _baseForce * sizeMultiplier;
        float radius = _baseRadius * sizeMultiplier;

        Collider[] hits = Physics.OverlapSphere(position, radius, _cubeLayer);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody == null)
                continue;

            hit.attachedRigidbody.AddExplosionForce(force, position, radius, _upwardsModifier, ForceMode.Impulse);
        }
    }

    private void PlayEffectAt(Vector3 position)
    {
        if (_explosionEffectPrefab == null)
            return;

        ParticleSystem effect = Instantiate(_explosionEffectPrefab, position, Quaternion.identity);

        ParticleSystem.MainModule main = effect.main;
        float totalDuration = main.duration + main.startLifetime.constantMax;

        effect.Play();
        Destroy(effect.gameObject, totalDuration);
    }
}
