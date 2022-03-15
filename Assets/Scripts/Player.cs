using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public Bullet bulletPrefab;
    private bool thrusting;
    private float turnDirection;
    public float thrustSpeed;
    public float turnSpeed;
    private GameManager gameManager;
    private float horizontalInput;
    private float verticalInput;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }


    // void Update()
    // {
    //     thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    //     if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    //     {
    //         turnDirection = 1.0f;
    //     }
    //     else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    //     {
    //         turnDirection = -1.0f;
    //     }
    //     else
    //     {
    //         turnDirection = 0f;
    //     }
    //     if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Shoot();
    //     }
    // }
    // private void FixedUpdate()
    // {
    //     if (thrusting)
    //     {
    //         playerRb.AddForce(transform.up * thrustSpeed);
    //     }
    //     if (turnDirection != 0)
    //     {
    //         playerRb.AddTorque(turnDirection * turnSpeed);
    //     }
    // }
    private void Start()
    {

    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(transform.up * verticalInput * thrustSpeed * Time.deltaTime);
        playerRb.AddTorque(horizontalInput * turnSpeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);
            gameManager.PlayerDied();
        }
    }
}
