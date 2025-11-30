using System;
using TMPro;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro pointsText;

    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
        player.OnChangeScore += OnChangeScore;
        UpdateCharacters();
    }

    private void OnChangeScore(int score, int maxScore)
    {
        UpdateCharacters();
    }

    private void UpdateCharacters()
    {
        scoreText.text = player.score.ToString() + "/" + player.scoreToNextLvl.ToString();
        pointsText.text = player.points.ToString();
    }
}
