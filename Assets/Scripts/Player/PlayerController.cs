using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sneakSpeed = 2f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInput playerInput;

    private CharacterController _controller;
    private Vector3 _velocity;
    private Vector2 _inputMove;
    private bool _isGrounded;
    private bool _isSneaking;
    
    private float _turnSmoothVelocity;
    private float _turnSmoothTime = 0.1f;
    private bool _wasGroundedLastFrame = true;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerInput.actions["Sneak"].performed += ctx =>_isSneaking = true;
        playerInput.actions["Sneak"].canceled += ctx => _isSneaking = false;

    }

    private void Update()
    {
        HandleGravity();
        HandleMovement();

        // // Set air state
        if (!_isGrounded)
        {
            if (animator != null)
                animator.SetBool("IsInAir", true);
        }

        //  // Detect Fall : grounded → falling
        // if (_wasGroundedLastFrame && !_isGrounded)
        // {
        //     if (animator != null)
        //     {
        //         animator.SetBool("IsInAir", true);
        //         animator.ResetTrigger("Land"); // Just in case
        //     }
        // }

        // Detect landing : air → grounded
        if (!_wasGroundedLastFrame && _isGrounded)
        {
            if (animator != null)
            {
                animator.SetBool("IsInAir", false);
                animator.SetTrigger("Land");
            }
        }

        _wasGroundedLastFrame = _isGrounded;
    }

    public void OnMove(InputValue value)
    {
        _inputMove = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (_isGrounded && value.isPressed)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (animator != null)
            {
                animator.ResetTrigger("Land");
                animator.SetTrigger("JumpStart");
                animator.SetBool("IsInAir", true);  // we'll reset this on landing

            }
        }
    }
    // public void OnSneak(InputValue value)
    // {
    //     _isSneaking = value.isPressed;
    //     if (value.isPressed)
    //         Debug.Log("Sprint STARTED");
    //     else
    //         Debug.Log("Sprint ENDED");
            
    // }

    private void HandleMovement()
    {
        _isGrounded = _controller.isGrounded;
        float _currentMoveSpeed = _isSneaking ? sneakSpeed : moveSpeed;

        Vector3 direction = new Vector3(_inputMove.x, 0f, _inputMove.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _currentMoveSpeed * Time.deltaTime);

            if (animator != null)
            {
                if (_isSneaking)
                {
                    animator.SetBool("IsSneaking", true);
                    animator.SetBool("isRunning", false);

                }
                else
                {
                    animator.SetBool("isRunning", true);
                    animator.SetBool("IsSneaking", false);
                }
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("IsSneaking", false);
            }
                
        }
    }

    private void HandleGravity()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // for fix ground falling  
        }

        if (!_isGrounded && _velocity.y < -0.1f)
        {
            animator.SetBool("IsInAir", true); // Falling state
        }

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
