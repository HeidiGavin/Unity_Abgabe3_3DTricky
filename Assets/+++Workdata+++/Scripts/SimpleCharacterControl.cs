using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCharacterControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 _moveDirection = new Vector3(0, 0, 0);

    private float horizontal;
    private bool isFacingRight = true;
    public Animator animator;
    private float jumpPower = 4f;
    private bool isJumping = false;
    
    public bool canMove = true;

    private Rigidbody2D rb;
    
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
        
        if (canMove)
        {
            _moveDirection = Vector3.zero;

            if (Keyboard.current.aKey.isPressed)
            {
                _moveDirection.y = -1;
            }
            
            if (Keyboard.current.dKey.isPressed)
            {
                _moveDirection.y = 1;
            }

            _moveDirection = _moveDirection.normalized;

            transform.Translate(_moveDirection * Time.deltaTime * moveSpeed);
            
            animator.SetFloat("Speed", _moveDirection.magnitude);
        }
    }
}
