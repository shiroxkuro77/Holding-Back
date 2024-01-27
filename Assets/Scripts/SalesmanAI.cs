using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
    
public class SalesmanAI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionOffset = 0.05f;
    public Vector2 targetDirection;
    private Rigidbody2D rb; 
    SpriteRenderer spriteRenderer;
    public ContactFilter2D movementFilter;
    private Animator animator;
    private float changeDirectionCooldown;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public int visionRange = 10;
    public LayerMask playerLayer;
    public bool isFollowing = false;
    GameObject playerObject;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (isFollowing) {
            FollowPlayer();
            return;
        }

        DetectPlayer(targetDirection);

        if (!isFollowing) {
            bool success = TryMove(targetDirection);
        }
        /*
        if (!success) {
            targetDirection = -targetDirection;
            TryMove(targetDirection);
        }*/
        

        if (targetDirection.x < 0) {
            spriteRenderer.flipX = true;
            animator.SetInteger("MovingDirection", 1);
        } else if (targetDirection.x > 0) {
            spriteRenderer.flipX = false;
            animator.SetInteger("MovingDirection", 1);
        } else if (targetDirection.y > 0) { 
            animator.SetInteger("MovingDirection", 2);
        } else if (targetDirection.y < 0) { 
            animator.SetInteger("MovingDirection", 3);
        } 
    }

    private bool TryMove(Vector2 targetDirection) {
            int count = rb.Cast(
                    targetDirection,
                    movementFilter,
                    castCollisions,
                    moveSpeed * Time.fixedDeltaTime + collisionOffset);
            
            if (count <= 1) {
                rb.MovePosition(rb.position + targetDirection * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
    }

    public void DetectPlayer(Vector2 rayDirection) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, visionRange, playerLayer);

        if (hit.collider != null && hit.collider.CompareTag("Player")) {
            // Player is within the line of sight
            Debug.Log("Player detected! Following");
            playerObject = hit.collider.gameObject;
            FollowPlayer();
        }
    }

    public void FollowPlayer() {
        isFollowing = true;
        Vector2 newDirection = (playerObject.transform.position - transform.position).normalized;
        moveSpeed = 5f;
        TryMove(newDirection);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Enemy is touching player
            Debug.Log("Player Touched! Selling him things!");
            
            GameObject player = collision.gameObject;
            // Deactivate the GameObject
            player.SetActive(false);
        }
    }

}
