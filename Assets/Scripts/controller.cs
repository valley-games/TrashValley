using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed;
    public float jumpPower;
    private Animator anim;
    private BoxCollider2D boxCollider;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    private float wallJumpCooldown;
    private float initialGravityScale;
    private float horizontalInput;

    private void Awake(){
        //Grab references
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        initialGravityScale = body.gravityScale;
    }

    // Update is called once per frame
    void Update(){
        horizontalInput = Input.GetAxis("Horizontal");
        
        //Flip
        if (horizontalInput < 0) transform.localScale = Vector3.one;
        else if(horizontalInput > 0) transform.localScale = new Vector3(-1, 1, 1);
        
        anim.SetBool("move", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        
        //Wall jump logic
        if (wallJumpCooldown > 0.2f){
            body.velocity= new Vector2(horizontalInput*speed, body.velocity.y);   
            if (onWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            
            else body.gravityScale = initialGravityScale;

            if(Input.GetKey(KeyCode.Space)) Jump();
        }
        else    wallJumpCooldown += Time.deltaTime;
    }

    private void Jump(){
        if (isGrounded()){
            body.velocity= new Vector2(body.velocity.x, jumpPower);   
                anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded()){
            if (horizontalInput == 0){
                body.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z); 
            }
            else body.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 3, 6);
            
            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack(){
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}

