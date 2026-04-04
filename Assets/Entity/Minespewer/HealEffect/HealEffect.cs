using System;
using UnityEngine;

public class HealEffect : MonoBehaviour
{
    private Health playerHealth;
    private ParticleSystem particles;

    private void Start()
    {
        playerHealth = GetComponentInParent<Health>();
        particles = GetComponent<ParticleSystem>();

        playerHealth.OnChangeHealth += OnChangeHealth;
    }

    private void OnChangeHealth(int health, int? lastHealth = null)
    {
        Debug.Log($"Health changed: {health} (last: {lastHealth})");
        if (lastHealth == null || health <= lastHealth)
            return;

        particles.Play();
    }
}
