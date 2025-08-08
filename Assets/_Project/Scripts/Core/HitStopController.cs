using System.Collections;
using UnityEngine;

public class HitStopController : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("На сколько замедлится время (0 = полная остановка, 1 = без изменений).")]
    [SerializeField] private float _timeScale = 0.1f;
    [Tooltip("Длительность заморозки в реальных секундах.")]
    [SerializeField] private float _duration = 0.07f;

    private Coroutine _hitStopCoroutine;
    
    public void TriggerHitStop()
    {
        if (_hitStopCoroutine != null)
        {
            StopCoroutine(_hitStopCoroutine);
        }
        _hitStopCoroutine = StartCoroutine(HitStopRoutine());
    }

    private IEnumerator HitStopRoutine()
    {
        Time.timeScale = _timeScale;
        yield return new WaitForSecondsRealtime(_duration);
        Time.timeScale = 1.0f;
    }
}