using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private LayerMask _hittableLayer;
    [SerializeField] private GameEvent _onHeadHit;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow, 2f); 

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _hittableLayer))
            {
                Debug.Log("Луч попал в объект: " + hit.collider.gameObject.name);

                ImpactData.LastImpactPoint = hit.point;
                ImpactData.LastImpactNormal = hit.normal;

                if (_onHeadHit != null)
                {
                    Debug.Log("Вызываю событие OnHeadHit!");
                    _onHeadHit.Raise();
                }
                else
                {
                    Debug.LogError("Событие OnHeadHit не назначено в инспекторе!");
                }
            }
            else
            {
                Debug.Log("Луч ни во что не попал");
            }
        }
    }
    
    public static class ImpactData
    {
        public static Vector3 LastImpactPoint { get; set; }
        public static Vector3 LastImpactNormal { get; set; }
    }
}