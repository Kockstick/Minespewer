using System;
using UnityEngine;

public class HealthProgress : MonoBehaviour
{
    [SerializeField]
    private GameObject progressObj;
    private Material progressMaterial;
    private Material borderMaterial;
    private Health playerHealth;

    private int maxHealth;
    private int currentHealth;

    void Start()
    {
        playerHealth = GetComponentInParent<Health>();
        maxHealth = playerHealth.GetMaxHealth();
        currentHealth = playerHealth.GetHealth();

        playerHealth.OnChangeHealth += OnChangeHealth;
        playerHealth.OnChangeMaxHealth += OnChangeMaxHealth;

        progressMaterial = progressObj.GetComponentInChildren<Renderer>().material;
        UpdateProgress();
    }

    private void OnChangeMaxHealth(int health)
    {
        maxHealth = health;
        UpdateProgress();
    }

    private void OnChangeHealth(int health)
    {
        currentHealth = health;
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        if (maxHealth <= 0 || progressObj == null || progressMaterial == null)
            return;

        float fill = Mathf.Clamp01((float)currentHealth / maxHealth);

        var t = progressObj.transform;
        var scale = t.localScale;
        scale.x = fill;
        t.localScale = scale;

        float tilingX = Mathf.Max(fill, 0.0001f);
        Vector2 tiling = progressMaterial.mainTextureScale;
        tiling.x = tilingX;
        progressMaterial.mainTextureScale = tiling;
    }
}
