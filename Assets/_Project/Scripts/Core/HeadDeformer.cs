using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class HeadDeformer : MonoBehaviour
{
    [Header("Настройки деформации")]
    [Tooltip("Радиус, в котором вершины будут подвержены деформации.")]
    [SerializeField] private float _deformationRadius = 0.5f;
    [Tooltip("Сила, с которой вершины будут вдавлены внутрь.")]
    [SerializeField] private float _deformationStrength = 0.2f;

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private Vector3[] _deformedVertices;
    
    private MeshCollider _meshCollider;

    void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();

        _mesh = _meshFilter.mesh = Instantiate(_meshFilter.mesh);
        
        _deformedVertices = _mesh.vertices;
    }

    public void ApplyDeformation()
    {
        Vector3 impactPoint = InputController.ImpactData.LastImpactPoint;
        Vector3 impactNormal = InputController.ImpactData.LastImpactNormal;
        
        Vector3 localImpactPoint = transform.InverseTransformPoint(impactPoint);
        
        Vector3 localImpactDirection = transform.InverseTransformDirection(impactNormal);

        for (int i = 0; i < _deformedVertices.Length; i++)
        {
            float distance = Vector3.Distance(_deformedVertices[i], localImpactPoint);

            if (distance < _deformationRadius)
            {
                float falloff = 1 - (distance / _deformationRadius);
                
                Vector3 displacement = localImpactDirection * -1 * _deformationStrength * falloff;
                
                _deformedVertices[i] += displacement;
            }
        }
        
        _mesh.vertices = _deformedVertices;
        _mesh.RecalculateNormals();

        if (_meshCollider != null)
        {
            _meshCollider.sharedMesh = null;
            _meshCollider.sharedMesh = _mesh;
        }
    }
}