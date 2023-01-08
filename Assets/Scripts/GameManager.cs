using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Score { get; private set; }
    public int Highscore { get; private set; }

    public static GameManager instance;
    public delegate void OnScoreChangeDelegate();
    public OnScoreChangeDelegate OnScoreChang;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip harvestClip;
    [SerializeField] private AudioClip playerAttackClip;
    [SerializeField] private AudioClip enemyAttackClip;
    private AudioSource audioSource;

    public void PlaySFX(string clipString)
    {
        AudioClip clip;
        switch (clipString)
        {
            case "player_attack":
                clip = playerAttackClip;
                break;
            case "enemy_attack":
                clip = enemyAttackClip;

                break;
            case "harvest":
                clip = harvestClip;

                break;
            case "game_over":
                clip = gameOverClip;

                break;
            default:
                return;

        }

        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        Highscore = PlayerPrefs.GetInt("Highscore");
        audioSource = GetComponent<AudioSource>();
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseScore(int score)
    {
        this.Score += score;
        OnScoreChang?.Invoke();
    }

    public void NewGame()
    {
        Score = 0;
        SceneManager.LoadScene("SampleScene");
    }

    public void Close()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        PlaySFX("game_over");

        if (Score > Highscore)
        {
            PlayerPrefs.SetInt("Highscore", Score);
            Highscore = Score;
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void Reload()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
