using UnityEngine;

public class DeathExplosion : MonoBehaviour
{
    private SphereCollider sphereCollider;
    float forceImpulse = 10f;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null || !other.transform.parent.TryGetComponent(out Entity entity))
            return;

        var rb = entity.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Entity has no Rigidbody component.");
            return;
        }

        Vector3 explosionDirection = (entity.transform.position - transform.position).normalized;
        rb.AddForce(explosionDirection * -forceImpulse, ForceMode.Impulse);
    }

    private void DeleteOnEndAnimation()
    {
        Destroy(gameObject);
    }
}
