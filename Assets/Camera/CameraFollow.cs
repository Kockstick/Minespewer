using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smooth = 3;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = FindAnyObjectByType<Player>().transform;
    }

    void LateUpdate()
    {
        if (playerTransform == null)
            return;

        var lerp = smooth * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, lerp);
    }
}
