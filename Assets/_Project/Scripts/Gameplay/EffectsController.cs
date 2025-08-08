using System.Collections;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private HeadData _headData;
    
    [Header("Visuals")]
    [SerializeField] private ParticleSystem _hitParticlesPrefab;
    [SerializeField] private Renderer _headRenderer;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;

    private Material _headMaterialInstance;
    private HeadController _headController;

    private void Awake()
    {
        _headController = GetComponent<HeadController>();
        _headMaterialInstance = _headRenderer.material;
    }
    
    public void PlayHitEffects()
    {
        // Звук
        if (_headData.HitSounds.Length > 0)
        {
            _audioSource.PlayOneShot(_headData.HitSounds[Random.Range(0, _headData.HitSounds.Length)]);
        }

        // Партиклы
        if (_hitParticlesPrefab != null)
        {
            Instantiate(_hitParticlesPrefab, InputController.ImpactData.LastImpactPoint, Quaternion.LookRotation(InputController.ImpactData.LastImpactNormal));
        }

        // Обновление шейдера
        float healthPercent = _headController.HealthPercentage;
        _headMaterialInstance.SetFloat("_Damage", 1 - healthPercent);
    }
}