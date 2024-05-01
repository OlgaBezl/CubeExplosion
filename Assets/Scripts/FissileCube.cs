using UnityEngine;

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

        Destroy(gameObject);
    }

    public void SetSplitChance(float splitChance)
    {
        _splitChance = splitChance;
    }

    public void Explode()
    {
        _rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
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
        newCube.Explode();

        return newCube;
    }
}
