using System;
using TMPro;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro pointsText;

    private Color scoreBaseColor;
    private Color pointsBaseColor;

    [SerializeField] private float idleDelay = 4f;
    private float idleTimer = 0f;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float fadeInDuration = 0.15f;

    private float currentAlpha = 1f;

    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private float fadeInTimer = 0f;
    private float fadeOutTimer = 0f;
    private float fadeInStartAlpha = 0f;

    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
        player.OnChangeScore += OnChangeScore;

        scoreBaseColor = scoreText.color;
        pointsBaseColor = pointsText.color;

        currentAlpha = 1f;
        SetAlpha(currentAlpha);

        UpdateCharacters();
    }

    void Update()
    {
        idleTimer += Time.deltaTime;

        if (isFadingIn)
        {
            fadeInTimer += Time.deltaTime;
            float t = Mathf.Clamp01(fadeInTimer / fadeInDuration);
            currentAlpha = Mathf.Lerp(fadeInStartAlpha, 1f, t);
            SetAlpha(currentAlpha);

            fadeOutTimer = 0f;
            isFadingOut = false;

            if (t >= 1f)
            {
                isFadingIn = false;
            }

            return;
        }

        if (idleTimer > idleDelay)
        {
            if (!isFadingOut)
            {
                isFadingOut = true;
            }
        }

        if (isFadingOut)
        {
            fadeOutTimer += Time.deltaTime;
            float t = Mathf.Clamp01(fadeOutTimer / fadeOutDuration);
            currentAlpha = Mathf.Lerp(1f, 0f, t);
            SetAlpha(currentAlpha);

            if (t >= 1f)
            {
                isFadingOut = false;
            }
        }
    }

    private void OnChangeScore(int score, int maxScore)
    {
        UpdateCharacters();
    }

    private void UpdateCharacters()
    {
        scoreText.text = player.score.ToString() + "/" + player.scoreToNextLvl.ToString();
        pointsText.text = player.points.ToString();

        idleTimer = 0f;
        isFadingOut = false;
        fadeOutTimer = 0f;

        isFadingIn = true;
        fadeInTimer = 0f;
        fadeInStartAlpha = currentAlpha;
    }


    private void SetAlpha(float a)
    {
        var c1 = scoreBaseColor;
        c1.a = a;
        scoreText.color = c1;

        var c2 = pointsBaseColor;
        c2.a = a;
        pointsText.color = c2;
    }

    private void OnDestroy()
    {
        if (player != null)
            player.OnChangeScore -= OnChangeScore;
    }
}