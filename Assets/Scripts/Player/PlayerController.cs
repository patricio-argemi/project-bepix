using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float dashSpeed = 1f;
    [SerializeField] private LayerMask[] jumpableLayers;
    
    private InputActions inputActions;
    private Rigidbody2D playerRigidBody;
    private Collider2D playerCollider;
    private Animator playerAnimator;    
    private Vector2 movement;
    private bool isDashing;
    private bool isFacingBackwards;

    public bool isInMidAir { get; private set; }

    #region Initial config methods
    private void Awake()
    {        
        inputActions = new InputActions();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();        
    }

    void Start()
    {
        //Input Actions        
        inputActions.Player.Jump.performed += context => Jump();
        inputActions.Player.Dash.performed += context => Dash();        
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
    #endregion

    #region Loop    
    void FixedUpdate()
    {
        //PlayerMovement
        Vector2 movementInputValue = inputActions.Player.Move.ReadValue<Vector2>();
        float normalizedMovementInput = NormalizeInputHelper.NormalizeFloats(movementInputValue.x, movementSpeed);
        movement = new Vector2(normalizedMovementInput * 10, 0);

        //Debug.Log(movement);        

        isInMidAir = !IsGrounded();
        Debug.Log(isInMidAir);

        //movement
        playerRigidBody.AddForce(movement, ForceMode2D.Force);

        if (playerRigidBody.velocity.magnitude > maxSpeed)
        {
            playerRigidBody.velocity = playerRigidBody.velocity.normalized * maxSpeed;
        }                

        //look at
        if (movement.x > 0 && isFacingBackwards || movement.x < 0 && !isFacingBackwards)
        {
            Flip();
        }

        //fall
        if (playerRigidBody.velocity.y < 0)
        {
            Fall();
        }                    

        //Dashing
        if (isDashing)
        {
            playerRigidBody.AddForce(new Vector2(NormalizeInputHelper.NormalizeFloat(playerRigidBody.velocity.x * dashSpeed), 0), ForceMode2D.Impulse);
            isDashing = !isDashing;
        }

        //Animation triggers        
        playerAnimator.SetBool("isMoving", movement.x != 0);
        playerAnimator.SetBool("isGrounded", IsGrounded());
    }
    #endregion

    #region private methods   
    private void Jump()
    {       
        if (IsGrounded())
        {
            playerAnimator.SetTrigger("isJumping");
            playerRigidBody.AddForce(new Vector2(0, jumpSpeed / 10), ForceMode2D.Impulse);            
            Debug.Log(string.Format("Jumping with {0} velocity", playerRigidBody.velocity.y));
        }
    }

    private void Fall()
    {
        playerAnimator.SetTrigger("isFalling");
    }

    private bool IsGrounded()
    {        
        var jumpableLayersList = jumpableLayers.ToList();
        return jumpableLayersList.Any(jumpableLayer => playerCollider.IsTouchingLayers(jumpableLayer));              
    }

    private void Flip()
    {
        isFacingBackwards = !isFacingBackwards;
        Vector3 invertedTransform = transform.localScale;
        invertedTransform.x *= -1;
        transform.localScale = invertedTransform;
    }

    private void Dash()
    {
        Debug.Log("Dashing");
        isDashing = true;
    }
    #endregion
}
