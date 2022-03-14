using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRb;
    public float speed = 500f;
    public float maxLifeTime = 10f;
    private void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }
    public void Project(Vector2 direction)
    {
        bulletRb.AddForce(direction * speed);
        Destroy(this.gameObject, maxLifeTime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
