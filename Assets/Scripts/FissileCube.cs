using UnityEngine;
using UnityEngine.UIElements;

public class FissileCube : MonoBehaviour
{
    [SerializeField] private int _minCubesCount = 2;
    [SerializeField] private int _maxCubesCount = 6;
    [SerializeField] private float _scaleFactor = 0.5f;
    [SerializeField] private float _splitChanceFactor = 0.5f;

    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;

    [SerializeField] private MaterialCreator _materialCreator;
    [SerializeField] private ChanceCalculator _chanceCalculator;
    [SerializeField] private Transform _container;
    [SerializeField] private Rigidbody _rigidbody;

    private float _splitChance = 100;

    private void OnMouseDown()
    {
        if (_chanceCalculator.CheckChanceSuccess(_splitChance))
        {
            SpawnSmallCubes();
        }
        else
        {
            Explode();
        }

        Destroy(gameObject);
    }

    public void SetSplitChance(float splitChance)
    {
        _splitChance = splitChance;
    }

    public void AddExplosionForce(float force, Vector3 position, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if(hit.TryGetComponent(out FissileCube cube))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance == 0) continue;

                float finalExplosionForce = _explosionForce / distance / transform.localScale.x;
                cube.AddExplosionForce(finalExplosionForce, transform.position, _explosionRadius);
            }
        }
    }

    private void SpawnSmallCubes()
    {
        int cubesCount = Random.Range(_minCubesCount, _maxCubesCount + 1);

        for (int i = 0; i < cubesCount; i++)
        {
            SpawnCube();
        }
    }

    private FissileCube SpawnCube()
    {
        var newCube = Instantiate(this, _container);
        newCube.transform.localScale = transform.localScale * _scaleFactor;

        MeshRenderer renderer = newCube.GetComponent<MeshRenderer>();
        renderer.material = _materialCreator.CreateRandomMaterial();

        newCube.SetSplitChance(_splitChance * _splitChanceFactor);

        return newCube;
    }
}
