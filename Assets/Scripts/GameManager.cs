using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public Text textScore;
    public Text textLives;
    public Text textGameOver;
    public Button btnRestart;
    public int lives = 3;
    public float reSpawnTime = 3f;
    public float reSpawnInvulnerabilityTime = 3f;
    private int score = 0;
    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(ReSpawn), reSpawnTime);
        }

    }
    private void ReSpawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), reSpawnInvulnerabilityTime);
    }
    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void GameOver()
    {
        textGameOver.gameObject.SetActive(true);
        btnRestart.gameObject.SetActive(true);
    }
    public void AsteroidDestroyed(Asteroids asteroids)
    {
        this.explosion.transform.position = asteroids.transform.position;
        this.explosion.Play();
        if (asteroids.size < 0.75f)
        {
            this.score += 100;
        }
        else if (asteroids.size < 1.2f)
        {
            this.score += 50;
        }
        else
        {
            this.score += 25;
        }
    }
    private void Update()
    {
        textScore.text = "Score: " + this.score;
        textLives.text = "Lives: " + lives;
    }
    public void Restart()
    {
        this.lives = 3;
        this.score = 0;
        textGameOver.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
        //player.gameObject.SetActive(true);
        ReSpawn();
    }
}
