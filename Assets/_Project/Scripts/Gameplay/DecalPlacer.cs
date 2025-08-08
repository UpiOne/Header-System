using UnityEngine;

public class DecalPlacer : MonoBehaviour
{
    [Header("Настройки декали")]
    [SerializeField] private GameObject _woundDecalPrefab;
    [Tooltip("Насколько далеко от поверхности будет создана декаль, чтобы избежать мерцания.")]
    [SerializeField] private float _surfaceOffset = 0.05f;

    [Header("Вариативность")]
    [SerializeField] private float _minSize = 0.2f;
    [SerializeField] private float _maxSize = 0.4f;

    public void PlaceDecal()
    {
        if (_woundDecalPrefab == null)
        {
            Debug.LogError("Префаб декали не назначен в DecalPlacer!");
            return;
        }

        // Получаем данные об ударе из вашего InputController
        Vector3 impactPoint = InputController.ImpactData.LastImpactPoint;
        Vector3 impactNormal = InputController.ImpactData.LastImpactNormal;

        // Рассчитываем позицию для создания проектора.
        // Мы создаем его чуть-чуть НАД поверхностью, чтобы избежать Z-fighting (мерцания).
        Vector3 decalPosition = impactPoint + impactNormal * _surfaceOffset;

        // Рассчитываем ориентацию. Проектор должен "смотреть" на поверхность.
        // Нормаль смотрит ОТ поверхности, поэтому мы используем -impactNormal.
        Quaternion decalRotation = Quaternion.LookRotation(-impactNormal);
        
        GameObject decalObject = Instantiate(_woundDecalPrefab, decalPosition, decalRotation);

        // Добавляем вариативности, чтобы раны выглядели по-разному
        Projector decalProjector = decalObject.GetComponent<Projector>();
        if (decalProjector != null)
        {
            decalProjector.orthographicSize = Random.Range(_minSize, _maxSize);
        }

        // Добавим случайный поворот вокруг оси, чтобы текстура не всегда была под одним углом
        decalObject.transform.Rotate(0, 0, Random.Range(0f, 360f));

        // Уничтожаем декаль через некоторое время, чтобы не засорять сцену
        Destroy(decalObject, 9f); 
    }
}