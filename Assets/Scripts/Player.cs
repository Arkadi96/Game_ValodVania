using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //configuration parameters
    [Range(0, 5)] [SerializeField] private float playerSpeed=2.5f;
    [Range(0, 20)] [SerializeField] private float jumpSpeed = 10.0f;
    [Range(0, 5)] [SerializeField] private float climbingSpeed = 2.0f;    
    private float gravityScale = 1.0f;

    //chached references
    private Rigidbody2D rigidbody2D;
    private CapsuleCollider2D bodyCollider2D;
    private BoxCollider2D feetColider2D;
    private Animator animator;
    private bool isAlive = true;
    
    //string values
    private string HORIZONTAL_AXIS = "Horizontal";
    private string VERTICAL_AXIS = "Vertical";
    private string RUNNING_ANIMATION = "IsRunning";
    private string WALKING_ANIMATION = "IsWalking";
    private string CLIMBING_ANIMATION = "IsClimbing";
    private string DYING_ANIMATION = "IsDying";
    private string DEAD_PLAYER_LAYER = "Dead Player";
    private string WATER_LAYER = "Water";
    private string OBSTACLE_LAYER = "Obstacle";

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetColider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!isAlive)
        {
            return;
        }

        MovePlayer();
        Jump();
        Climb();
        FlipSprite();
        Die();
    }

    private void Die()
    {
        if (!bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            return;
        }
        Debug.Log("Died from Enemy");
        DyingAnimation();
    }

    private void DyingAnimation()
    {
        isAlive = false;
        animator.SetTrigger(DYING_ANIMATION);
        StartCoroutine(SetStaticRidgidBody());       
    }

    IEnumerator SetStaticRidgidBody()
    {
        yield return new WaitForSeconds(1f);
        rigidbody2D.bodyType = RigidbodyType2D.Static;        
        gameObject.layer = LayerMask.NameToLayer(DEAD_PLAYER_LAYER);
    }

    private void MovePlayer()
    {
        HandleHorizontalMovement();
        MovingAnimation();
    }

    private void Jump()
    {
        if (!feetColider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 newVelocity = new Vector2(0f, jumpSpeed);
            rigidbody2D.velocity += newVelocity;
        }
    }

    private void Climb()
    {
        if (!feetColider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rigidbody2D.gravityScale = gravityScale;
            animator.SetBool(CLIMBING_ANIMATION, false);
            return;
        }

        HandleVerticalMovement();
        ClimbingAnimation();
    }

    private void FlipSprite()
    {
        bool hasMovementSpeed = (Mathf.Abs(rigidbody2D.velocity.x) > 0);

        if (hasMovementSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }
    }

    private void HandleHorizontalMovement()
    {
        float translation = Input.GetAxis(HORIZONTAL_AXIS) * playerSpeed;
        Vector2 playerVelocity = new Vector2(translation, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;
    }

    private void HandleVerticalMovement()
    {
        float translation = Input.GetAxis(VERTICAL_AXIS) * climbingSpeed;
        Vector2 newVelocity = new Vector2(rigidbody2D.velocity.x, translation);
        rigidbody2D.velocity = newVelocity;
        rigidbody2D.gravityScale = 0f;
    }

    private void ClimbingAnimation()
    {
        bool hasClimbingSpeed = (Mathf.Abs(rigidbody2D.velocity.y) > 0);

        if (hasClimbingSpeed)
        {
            animator.SetBool(CLIMBING_ANIMATION, true);
        }
        else
        {
            animator.SetBool(CLIMBING_ANIMATION, false);
        }
    }    

    private void MovingAnimation()
    {
        bool haSpeedOnAxisX = (Mathf.Abs(rigidbody2D.velocity.x) > 0);    

        if (Input.GetKey(KeyCode.LeftShift)&&haSpeedOnAxisX)
        {
            animator.SetBool(RUNNING_ANIMATION, true);
            animator.SetBool(WALKING_ANIMATION, false);
        }
        else if (haSpeedOnAxisX)
        {
            animator.SetBool(RUNNING_ANIMATION,false);
            animator.SetBool(WALKING_ANIMATION,true);
        }
        else if (!haSpeedOnAxisX)
        {
            animator.SetBool(RUNNING_ANIMATION, false);
            animator.SetBool(WALKING_ANIMATION, false);                       
        }      
        else
        {
            return;
        }
    }        

    public void SetNewSpeed(float value)
    {
        if (value >= 0 && value <= 5)
        {
            playerSpeed = value;
        }
        else
        {
            Debug.LogError($"has set a new speed value {value}, that is out of a range");
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.layer == LayerMask.NameToLayer(WATER_LAYER))
        {            
            Debug.Log("Died from water");
            DyingAnimation();
        }

        if (otherGameObject.layer == LayerMask.NameToLayer(OBSTACLE_LAYER))
        {
            Debug.Log("Died from Obstacle");
            DyingAnimation();
        }
    }
}
