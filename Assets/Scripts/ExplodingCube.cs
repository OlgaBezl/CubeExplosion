using UnityEngine;

public class ExplodingCube : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private Rigidbody _rigidbody;

    public void AddExplosionForce(float force, Vector3 position, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }

    public void ExplodeWithShockwave()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out ExplodingCube cube))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance == 0) continue;

                float finalExplosionForce = _explosionForce / distance / transform.localScale.x;
                cube.AddExplosionForce(finalExplosionForce, transform.position, _explosionRadius);
            }
        }
    }

}
