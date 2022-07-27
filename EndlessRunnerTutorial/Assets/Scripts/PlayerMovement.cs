using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;

public class PlayerMovement : MonoBehaviour {

    bool alive = false;

    [SerializeField, Tooltip("Canvas utama")] GameObject mainCanvas;
    [SerializeField, Tooltip("Canvas Game Over")] GameObject gameOverCanvas;

    FirebaseFirestore db;

    int lenghtDoc = 0;

    int lowestScore;


    public float speed = 5;
    private float leftBoundary = -4f, rightBoundary = 4f;
    [SerializeField] public Rigidbody rb;

    [SerializeField] public GameManager gameManager;

    private Animator animator;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 1;

    public float speedIncreasePerPoint = 0.1f;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    private void FixedUpdate ()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;

        // If else buat ngatur gak lebih dari arenanya (Boundaries)
        if (rb.position.x <= leftBoundary) {
            rb.position = new Vector3(leftBoundary, rb.position.y, rb.position.z);
        }
        else if (rb.position.x >= rightBoundary) {
            rb.position = new Vector3(rightBoundary, rb.position.y, rb.position.z);
        }
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update () {
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
            Jump();
            animator.SetBool("isJump", true);
        } else {
            animator.SetBool("isJump", false);
        }

        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            animator.SetBool("isCrouch", true);
        } else {
            animator.SetBool("isCrouch", false);
        }

        if (transform.position.y < -5) {
            Die();
        }
	}

    public void Die ()
    {
        alive = false;
        mainCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        String nama = PlayerPrefs.GetString("userName") == "" ? Environment.UserName : PlayerPrefs.GetString("userName");
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "nama", nama},
            { "score", gameManager.score }
        };
        if (gameManager.score != 0) {
            db.Collection("scores").AddAsync(data);
        }
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Jump () {
        // Check if we grounded 
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height/2) + 0.1f, groundMask);

        if (isGrounded) {
        rb.AddForce(Vector3.up * jumpForce);
        }

    }

    void Awake() {
        db = FirebaseFirestore.DefaultInstance;
        // Ambil Collection mana yang mau di pakai
        CollectionReference scoreRef = db.Collection("scores");
        // Urutkan berdasarkan score dan batasi sampai 10 item
        Query query = scoreRef.OrderByDescending("score").Limit(10);

        
    }

    void Start() {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        animator = GetComponent<Animator>();
        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        print(Time.time);
        yield return new WaitForSeconds(2);
        alive = true;
    }
}