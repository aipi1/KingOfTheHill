using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles input from player and manages player behaviour as well as
/// OnGameOver method
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject powerupIndicator;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private AudioClip powerupSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip powerHitSound;
    [SerializeField] private AudioClip gameOverSound;
    private float verticalInput;
    private float speed = 5.0f;
    private float powerupStrength = 15.0f;
    private int powerUpCount = 0;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private AudioSource playerAudio;
    private Vector3 powerupPosOffset = new Vector3(0, -0.5f, 0);
    private bool hasPowerup = false;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameUI.OnEscapePressed();
        }

        if (!GameUI.isGamePaused)
        {
            verticalInput = Input.GetAxis("Vertical");
            powerupIndicator.transform.position = transform.position + powerupPosOffset;
            if (transform.position.y < -10 && !isGameOver)
            {
                OnGameOver();
            }
        }
    }

    void FixedUpdate()
    {
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            playerAudio.PlayOneShot(powerupSound, 1.0f);
            Destroy(other.gameObject);
            hasPowerup = true;
            powerUpCount++;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerUpCount--;
        if (powerUpCount == 0)
        {
            hasPowerup = false;
            powerupIndicator.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            playerAudio.PlayOneShot(powerHitSound, 1.0f);
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayerDirection = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayerDirection * powerupStrength, ForceMode.Impulse);
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(hitSound, 1.0f);
        }
    }
    // If player picks up Powerup while already colliding with Enemy
    // OnCollisionStay handles this situation
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            playerAudio.PlayOneShot(powerHitSound, 1.0f);
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayerDirection = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayerDirection * powerupStrength, ForceMode.Impulse);
        }
    }

    private void OnGameOver()
    {
        playerAudio.PlayOneShot(gameOverSound, 1.0f);
        isGameOver = true;
        PlayerStatsHandler.Instance.score = SpawnManager.WaveNumber - 1;

        PlayerStatsHandler.Instance.LoadBestScore();
        if (PlayerStatsHandler.Instance.score > PlayerStatsHandler.Instance.BestScore)
        {
            PlayerStatsHandler.Instance.SaveBestScore();
            PlayerStatsHandler.Instance.LoadBestScore();
        }
        gameUI.UpdateBestScore();
        gameUI.ShowEndgameScreen();
    }
}
