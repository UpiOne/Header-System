using UnityEngine;

public class HeadController : MonoBehaviour
{
    [SerializeField] private HeadData _headData;
    [SerializeField] private GameEvent _onHeadDamaged;

    public float CurrentHealth { get; private set; }
    public float HealthPercentage => CurrentHealth / _headData.MaxHealth;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        CurrentHealth = _headData.MaxHealth;
    }
    
    public void TakeDamage()
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= 10; // Условный урон
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        
        Vector3 forceDirection = (transform.position - InputController.ImpactData.LastImpactPoint).normalized;
        _onHeadDamaged.Raise();
    }
}