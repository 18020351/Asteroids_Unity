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
    private void Start()
    {

    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(transform.up * verticalInput * thrustSpeed * Time.deltaTime);

        // playerRb.AddTorque(horizontalInput * turnSpeed * Time.deltaTime);
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));


        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
        // playAudio.PlayOneShot(shootSound, 1.0f);
        Sound.soundInstance.PlayAudio(Sound.soundInstance.shootAudio, 1.0f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            //playAudio.PlayOneShot(crashSound, 1.0f);
            Sound.soundInstance.PlayAudio(Sound.soundInstance.crashAudio, 1.0f);
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0.0f;
            gameManager.PlayerDied();
            this.gameObject.SetActive(false);
        }
    }

}
