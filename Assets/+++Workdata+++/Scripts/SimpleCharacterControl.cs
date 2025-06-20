using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class SimpleCharacterControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    private float _direction = 0f;

    public Animator animator;
    private float horizontal;
    private bool _isFacingRight = true;

    private Rigidbody2D rb;
    
    [SerializeField] private Transform transformCheckGround;
    [SerializeField] private LayerMask layerGround;
    
    [SerializeField] private Diamondmanager diamondManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TimerScript timerScript;
    
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = false;
    }

    void Update()
    {
        FlipSprite();
        
        bool isGrounded =Physics2D.OverlapCircle(transformCheckGround.position, 0.1f, layerGround);
        animator.SetBool("isGrounded", isGrounded);
        
        //Basic Character Movement Code
        if (canMove)
        {
            _direction = 0f;
            if (Keyboard.current.aKey.isPressed)
            {
                _direction = -1f;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                _direction = 1;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
            
            rb.linearVelocity = new Vector2(_direction * moveSpeed, rb.linearVelocity.y);

            animator.SetFloat("Speed", Mathf.Abs(_direction));
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetFloat("Speed", 0);
        }
    }
    //Flip Sprite while moving Code
    void FlipSprite()
    {
        if (_direction > 0 && !_isFacingRight || _direction < 0 && _isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    //Jump Force
    void Jump()
    {
       animator.SetTrigger("Jump");
        
        if (Physics2D.OverlapCircle(transformCheckGround.position, 0.1f, layerGround))
            rb.linearVelocity = new Vector2(0, jumpForce);
    }
    
    //ifCollision Manager
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision!");

        if (other.CompareTag("Diamond"))
        {
            Debug.Log("Its a Diamond");
            Destroy(other.gameObject);
            diamondManager.AddDiamonds(1);
        }

        else if (other.CompareTag("Obstacle"))
        {
            Debug.Log("you touched the obstacle");
            uiManager.ShowPanelLost();
            rb.linearVelocity = Vector2.zero;
            canMove = false;
            timerScript.StopTimer();
        }
        
        else if (other.CompareTag("WinFlag"))
        {
            Debug.Log("You win!");
            uiManager.ShowPanelWin();
            rb.linearVelocity = Vector2.zero;
            canMove = false;
            timerScript.StopTimer();
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}