using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour
{
    [HideInInspector] public int level = 1;
    [HideInInspector] public int score = 0;
    [HideInInspector] public int scoreToNextLvl = 0;
    private readonly int upLvlMultipler = 100;
    [HideInInspector] public int points = 0;
    private Health health;

    public delegate void OnChangeScore_EventHalder(int score, int maxScore);
    public OnChangeScore_EventHalder OnChangeScore;

    void Start()
    {
        health = GetComponent<Health>();
        score = 0;
        scoreToNextLvl = level * level * upLvlMultipler;
        GetComponentInChildren<Mortar>().SetOwner(this);
        OnStart();
    }

    protected abstract void OnStart();

    public void OnKill(Entity enemy)
    {
        upScore(enemy.scoreToNextLvl / 2);
    }

    private void upScore(int value)
    {
        score += value;
        while (score >= scoreToNextLvl)
        {
            level++;
            points++;
            health.SetMaxHealth(level);
            score -= scoreToNextLvl;
            scoreToNextLvl = level * level * upLvlMultipler;
            //Up level animation
        }
        if (OnChangeScore != null) OnChangeScore(score, scoreToNextLvl);
    }
}
