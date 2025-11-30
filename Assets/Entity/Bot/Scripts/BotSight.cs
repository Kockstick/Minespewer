using UnityEngine;

public class BotSight : MonoBehaviour
{
    private Rigidbody rb;
    private Entity target;
    private Rigidbody targetRb;

    [SerializeField] private float smooth = 5;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        if (target == null)
            return;

        var lerp = smooth * Time.deltaTime;
        var targetPosition = target.transform.position + targetRb.linearVelocity * 2;
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerp);
    }

    public void SetTarget(Entity enemy)
    {
        target = enemy;
        targetRb = target.GetComponent<Rigidbody>();
    }
}