using UnityEngine;
using DG.Tweening;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 0.2f;
    [SerializeField] private float _shakeStrength = 0.1f;
    [SerializeField] private int _vibrato = 10;
    
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = transform;
    }
    
    public void Shake()
    {
        _cameraTransform.DOComplete();
        _cameraTransform.DOShakePosition(_shakeDuration, _shakeStrength, _vibrato, 90, false, true);
    }
}