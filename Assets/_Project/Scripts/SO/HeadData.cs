using UnityEngine;

[CreateAssetMenu(fileName = "HeadData", menuName = "Gameplay/Head Data")]
public class HeadData : ScriptableObject
{
    [Header("Health")]
    public float MaxHealth = 100f;

    [Header("Impact")]
    public float ImpactForce = 50f;
    
    [Header("Sounds")]
    public AudioClip[] HitSounds;
}