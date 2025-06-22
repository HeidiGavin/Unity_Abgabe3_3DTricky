using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class SimpleCharacterControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // movement speed of the character
    [SerializeField] private float jumpForce = 5f; // force applied for jumping
    private float _direction = 0f; // direction of movement (-1 left, 1 righht)

    public Animator animator; // reference to Animator for controlling animations
    private float horizontal; // horizontal input
    private bool _isFacingRight = true; // tracks direction character is facing

    private Rigidbody2D rb; // Rigidbody2D component reference for physics
    
    //For Ground Check and Jump
    [SerializeField] private Transform transformCheckGround; // point to check if grounded
    [SerializeField] private LayerMask layerGround; // Ground layer reference
    
    //For In-Game UI and Tags
    [SerializeField] private Collecatblesmanager collectableManager; // Collectibles manager reference
    [SerializeField] private UIManager uiManager; // UI manager reference
    [SerializeField] private TimerScript timerScript; // Timer script reference
    
    private bool canMove = true; // control whether character can move
    
    private AudioManager audioManager; // Audio Manager reference

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = false; // disable movement at start
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        FlipSprite(); // sprite flipping
        
        // check is character is grounded
        bool isGrounded =Physics2D.OverlapCircle(transformCheckGround.position, 0.1f, layerGround);
        animator.SetBool("isGrounded", isGrounded); // update animator state
        
        //Basic Character Movement
        if (canMove) // if character is allowed to move
        {
            _direction = 0f; // reset direction
            if (Keyboard.current.aKey.isPressed) // move left with A
            {
                _direction = -1f;
            }

            if (Keyboard.current.dKey.isPressed) // move right with D
            {
                _direction = 1;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame) // jump on space press
            {
                Jump();
            }
            
            rb.linearVelocity = new Vector2(_direction * moveSpeed, rb.linearVelocity.y); // apply horizontal velocity
            animator.SetFloat("Speed", Mathf.Abs(_direction)); // update speed animator
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // stop horizontal movement
            animator.SetFloat("Speed", 0); // stop movement animation
        }
    }
    //Flip Sprite based on movement direction
    void FlipSprite()
    {
        if (_direction > 0 && !_isFacingRight || _direction < 0 && _isFacingRight)
        {
            _isFacingRight = !_isFacingRight; // invert facing direction
            Vector3 localScale = transform.localScale; // get local scale 
            localScale.x *= -1f; // flip horizontally
            transform.localScale = localScale; // apply new scale
        }
    }
    //Jump Logic
    void Jump()
    {
       animator.SetTrigger("Jump"); // trigger jump animation
        
       // apply vertical force if grounded
       if (Physics2D.OverlapCircle(transformCheckGround.position, 0.1f, layerGround))
       {
           rb.linearVelocity = new Vector2(0, jumpForce);
           audioManager?.PlayJumpSound();
       }

    }
    
    // Collision Tag Manager (collision detection with triggers)
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision!"); 

        if (other.CompareTag("Coin")) // coin pickup
        {
            Debug.Log("Its a Coin");
            Destroy(other.gameObject);
            collectableManager.AddCoins(10);
            audioManager?.PlayCoinSound();
            
        }
        
        else if (other.CompareTag("Diamond")) // diamond pickup
        {
            Debug.Log("Its a Diamond");
            Destroy(other.gameObject);
            collectableManager.AddCoins(20);
            audioManager?.PlayCoinSound();
        }

        else if (other.CompareTag("Obstacle")) // obstacle hit
        {
            Debug.Log("you touched the obstacle");
            uiManager.ShowPanelLost();
            rb.linearVelocity = Vector2.zero;
            canMove = false;
            timerScript.StopTimer();
        }
        
        else if (other.CompareTag("WinFlag")) // win condition
        {
            Debug.Log("You win!");
            rb.linearVelocity = Vector2.zero;
            canMove = false;
            timerScript.StopTimer();
            //add animation later
            StartCoroutine(WinDelay(3));
        }
    }
    
    // CanMove Bool (public method to enable/disable movement)
    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    // Coroutine to delay win panel
    private IEnumerator WinDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        int coinCount = collectableManager.coins;
        int diamondCount = collectableManager.diamonds;
        float timeTaken = timerScript.GetTimer();
        
        uiManager.ShowPanelWinScore(coinCount, diamondCount, timeTaken);
    }
}