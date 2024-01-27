using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{   
    //Moving Direction: 0 None, 1 Right, 2 Up, 3 Down, 4 Left
    public float moveSpeed = 1f;
    public float collisionOffset = 0f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        
        if (movementInput != Vector2.zero) {
           bool success = TryMove(movementInput);

           if (!success) {
                success = TryMove(new Vector2(movementInput.x, 0));
           }

           if (!success) {
                success = TryMove(new Vector2(0, movementInput.y));
           }
        }

        if (movementInput.x < 0) {
            spriteRenderer.flipX = true;
            animator.SetInteger("MovingDirection", 1);
        } else if (movementInput.x > 0) {
            spriteRenderer.flipX = false;
            animator.SetInteger("MovingDirection", 1);
        } else if (movementInput.y > 0) { 
            animator.SetInteger("MovingDirection", 2);
        } else if (movementInput.y < 0) { 
            animator.SetInteger("MovingDirection", 3);
        } 

    }

    private bool TryMove(Vector2 direction) {
        int count = rb.Cast(
                        movementInput,
                        movementFilter,
                        castCollisions,
                        moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0) {
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }

    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
