using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    Animator animator;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float speed = 1.5f;
    public float runSpeed = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float dashTime;
    public float dashSpeed;
    [SerializeField]
    public float distanceBetweenImages;
    public float dashCooldown;
    [SerializeField]
    private float activeTime = 0f;
    private float timeActivated;
    
    private float dashTimeLeft;
    private float lastImageZpos;
    private float ImageRotation;
    private float lastDash = -100;

    public GameObject targetObject;
    private Vector3 spawnPosition;
    
    bool isGrounded;
    bool isMoving;
    bool isRun;
    public bool toggleAfterImage;

    private bool isDashing;
    Vector3 velocity;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        bool isJumping = animator.GetBool("isJumping");
        //bool isAttacking = animator.GetBool("isAttacking");

        if (isGrounded && isMoving && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }

        else if (isGrounded && !isMoving && isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        if (isGrounded && isRun && !isRunning)
        {
            animator.SetBool("isRunning", true);
        }

        else if (isGrounded && !isRun && isRunning)
        {
            animator.SetBool("isRunning", false);
        }

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

        else if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }

        /*if (isGrounded && Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("isSalute", true);
        }

        else if (isGrounded && Input.GetKeyDown(KeyCode.J) | isRunning | isWalking | isJumping)
        {
            animator.SetBool("isSalute", false);
        }*/

        /*if (isGrounded && Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("isMagic", true);
        }

        else if (isGrounded && Input.GetKeyDown(KeyCode.K) | isRunning | isWalking | isJumping)
        {
            animator.SetBool("isMagic", false);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("isAttacking", true);
        }

        else if (isGrounded && Input.GetKeyDown(KeyCode.I) | isRunning | isWalking | isJumping)
        {
            animator.SetBool("isAttacking", false);
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        handleAnimation();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            isMoving = true;

            if (Input.GetKey(KeyCode.LeftShift) && isMoving==true)
            {
                controller.Move(moveDir.normalized * runSpeed * Time.deltaTime);
                isRun = true;
                if (toggleAfterImage)
                {
                    if (Time.time >= (timeActivated + dashCooldown))
                    {
                        attemptToDash();
                    }

                }
            }

            else 
            {
                isRun = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                controller.Move(moveDir.normalized * runSpeed);
                isRun = true;
                if (toggleAfterImage)
                {
                    if (Time.time >= (timeActivated + dashCooldown))
                    {
                        attemptToDash();
                    }

                }
            }
        }
        
        else
        {
            isMoving = false;
            isRun = false;
        }

        
    }

    private void attemptToDash()
    {
        isDashing = true;
        timeActivated = Time.time;
        spawnPosition = targetObject.transform.position;
        Quaternion spawnRotation = targetObject.transform.rotation;
        //var bomb = PlayerAfterImagePool.Instance.GetFromPool();
        //lastImageXpos = bomb.transform.position.x + 2;

        //var imageModel = PlayerAfterImagePool.Instance.GetFromPool();
        //imageModel.transform.position = spawnPosition;
        //    imageModel.transform.rotation = spawnRotation;
    }

    private void checkDash()
    {
        if (isDashing)
        {
            if (Mathf.Abs(transform.position.x - lastImageZpos) > distanceBetweenImages)
            {
                //var bomb = PlayerAfterImagePool.Instance.GetFromPool();
                //lastImageZpos = bomb.transform.position.z + 2;
                
            }
        }
    }

}
