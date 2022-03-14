using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D asteroidRb;
    public Sprite[] sprites;
    public float size = 1;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50f;
    public float maxLifeTime = 30f;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        asteroidRb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        this.transform.localScale = Vector3.one * size;
        asteroidRb.mass = this.size;
    }
    public void SetTrajectory(Vector2 direction)
    {
        asteroidRb.AddForce(direction * speed);
        Destroy(gameObject, maxLifeTime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if ((size / 2) > minSize)
            {
                CreatSplit();
                CreatSplit();
            }
            Destroy(this.gameObject);
        }
    }
    private void CreatSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroids half = Instantiate(this, position, this.transform.rotation);
        half.size = size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
