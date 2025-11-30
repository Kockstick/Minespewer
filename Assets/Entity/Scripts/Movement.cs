using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private Vector3 moveDir = Vector3.zero;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        moveDir = direction.normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDir * speed);
    }
}
