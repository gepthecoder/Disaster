using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public bool IsItGroundedOrWhat;

    private CharacterController player;
    private Animator animator;

    [SerializeField]
    private float forwardMoveSpeed = 7.5f;
    [SerializeField]
    private float forwardSprintSpeed = 11.0f;
    [SerializeField]
    private float backwardMoveSpeed = 4f;
    [SerializeField]
    private float turnSpeed = 105f;
    [SerializeField]
    private float LRSpeed = 3.5f;


    public float verticalVelocity;
    public float gravity = 10f;
    public float jumpForce = 150f;
    //public float fabJumpForce = 15f;

    public float moveSpeedToUse;
    public bool isSprinting = false;

    private Gun gun;

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        gun = GetComponentInChildren<Gun>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    private void Update()
    {

        var horizontal = Input.GetAxis("Mouse X");
        var vertical = CrossPlatformInputManager.GetAxis("Vertical");
        var horizontalMovement = CrossPlatformInputManager.GetAxis("Horizontal");

        LeftRightMovement(horizontalMovement, vertical);
        animator.SetFloat("LeftRightSpeed", horizontalMovement);

        Movement(horizontal, vertical);
        animator.SetFloat("Speed", vertical);

        Jump(horizontal, vertical);

        Rotate(horizontal);
    }

    private void LeftRightMovement(float horizontalMovement, float vertical)
    {
        if (vertical == 0)
        {
            //LEFT RIGHT MOVEMENT

            if (horizontalMovement < 0.1f && horizontalMovement > -0.1f )
            {
                //stop playin animation
                animator.SetBool("CanMoveToLeftRight", false);

            }
            if (horizontalMovement < -0.1f)
            {
                //move to the left
                StartCoroutine(WaitAndMoveLeftRight("Left", horizontalMovement));
                animator.SetBool("CanMoveToLeftRight", true);
            }

            if (horizontalMovement > 0.1f)
            {
                //move to the right
                StartCoroutine(WaitAndMoveLeftRight("Right", horizontalMovement));
                animator.SetBool("CanMoveToLeftRight", true);
            }

        }
    }

    private void Movement(float horizontal, float vertical)
    {
        if (vertical > 0)
        {
            animator.SetBool("CanMoveToLeftRight", false);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (EnduranceEffectManager.PotionActivated)
                {
                    gun.enabled = false;
                    moveSpeedToUse = forwardSprintSpeed;
                    moveSpeedToUse *= 3;
                    animator.SetBool("Sprint", true);
                    player.SimpleMove(transform.forward * moveSpeedToUse * vertical);

                    isSprinting = true;
                }
                else
                {
                    if (vertical > 0)
                    {
                        gun.enabled = false;
                        moveSpeedToUse = forwardSprintSpeed;
                        animator.SetBool("Sprint", true);
                        player.SimpleMove(transform.forward * moveSpeedToUse * vertical);

                        isSprinting = true;

                    }
                    else
                    {
                        animator.SetBool("Sprint", false);
                    }
                }
            }
            else
            {
                animator.SetBool("Sprint", false);

                float SPEED = (vertical > 0) ? forwardMoveSpeed : backwardMoveSpeed;
                if (vertical > 0) { TransformForward(SPEED); }
                if(vertical < 0) { TransformBackwards(SPEED); }

                gun.enabled = true;
                isSprinting = false;
            }

        }
    }


    private void TransformForward(float SPEED)
    {
        player.SimpleMove(transform.forward * SPEED);
    }

    private void TransformBackwards(float SPEED)
    {
        player.SimpleMove(-transform.forward * SPEED);
    }


    private void Jump(float horizontal, float vertical)
    {
        if (player.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            IsItGroundedOrWhat = true;

            if (Input.GetButtonDown("Jump"))
            {
                if (EnduranceEffectManager.PotionActivated)
                {
                    animator.SetTrigger("Jump");
                    verticalVelocity = jumpForce;
                    verticalVelocity *= 5;
                    Vector3 jumpMotion = new Vector3(horizontal, verticalVelocity, vertical);
                    player.Move(jumpMotion);
                }
                else
                {

                    animator.SetTrigger("Jump");
                    verticalVelocity = jumpForce;
                    verticalVelocity *= 3;
                    Vector3 jumpMotion = new Vector3(horizontal, verticalVelocity, vertical + 1.2f);
                    player.Move(jumpMotion);
                }

            }

            else { verticalVelocity -= gravity * Time.deltaTime; }

        }
        else { IsItGroundedOrWhat = false; }
    }

    private void Rotate(float horizontal)
    {
        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
    }

   

    IEnumerator WaitAndMoveLeftRight(string LR, float horizontalMove)
    {
        if(LR == "Left")
        {
            yield return new WaitForSeconds(0.5f);
            if(horizontalMove != 0)
            {
                player.SimpleMove(-transform.right * LRSpeed);
            }
            
        }
        else if(LR == "Right")
        {
            yield return new WaitForSeconds(0.5f);
            if (horizontalMove != 0)
            {
                player.SimpleMove(transform.right * LRSpeed);
            }
        }
    }

 

}
