using System;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    private Material spriteMaterial;
    private Color spriteColor;
    [SerializeField] private Transform shield;
    private Material shieldeMaterial;
    private Color shieldColor;

    [SerializeField] private AnimationCurve spriteOpacity;
    [SerializeField] private AnimationCurve shieldOpacity;
    [SerializeField] private float speedAnimation = 0.1f;
    private float time = 0;

    private Health health;
    private Transform camera;

    void Start()
    {
        health = GetComponentInParent<Health>();
        camera = Camera.main.transform;

        sprite.transform.LookAt(camera);

        spriteMaterial = sprite.GetComponentInChildren<Renderer>().material;
        spriteColor = spriteMaterial.color;
        shieldeMaterial = shield.GetComponent<Renderer>().material;
        shieldColor = shieldeMaterial.color;

        health.OnDamage += OnDamage;
    }

    private void OnDamage(Bullet bullet)
    {
        shield.transform.LookAt(bullet.transform);
        time = 0;
    }

    void Update()
    {
        time += speedAnimation * Time.deltaTime;
        if (time > 1)
            return;

        spriteColor.a = spriteOpacity.Evaluate(time);
        spriteMaterial.color = spriteColor;

        shieldColor.a = shieldOpacity.Evaluate(time);
        shieldeMaterial.color = shieldColor;
    }
}
