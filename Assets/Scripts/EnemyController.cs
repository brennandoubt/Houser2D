using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller2D
{
    public float speed = 5;
    private int direction = 1;

    private SpriteRenderer sr;
    public float animationFPS;
    public Sprite[] snakeAnimation;
    private float frameTimer = 0;
    private int frameIndex = 0;

    void Animate()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;
            frameIndex %= snakeAnimation.Length;
            sr.sprite = snakeAnimation[frameIndex];
            frameIndex++;
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // grounded check
        UpdateGrounding();

        // direction check
        UpdateDirection();

        // animate
        Animate();

        // update player's facing direction
        if (rb2d.velocity.x < -0.01f)
        {
            sr.flipX = true;
        }
        else if (rb2d.velocity.x > 0.01f)
        {
            sr.flipX = false;
        }

        // finally update enemy's velocity
        Vector2 vel = rb2d.velocity;
        vel.x = direction * speed;
        rb2d.velocity = vel;
    }

    void UpdateDirection()
    {
        // two additional edge rays to check for cliffs
        Vector3 leftpos = transform.position + Vector3.left * 0.5f;
        Vector3 rightpos = transform.position + Vector3.right * 0.5f;
        RaycastHit2D leftendray = Physics2D.Raycast(leftpos, Vector3.down, groundRayLength, groundLayers);
        RaycastHit2D rightendray = Physics2D.Raycast(rightpos, Vector3.down, groundRayLength, groundLayers);
        Debug.DrawLine(leftpos, leftpos + Vector3.down * groundRayLength, Color.blue);
        Debug.DrawLine(rightpos, rightpos + Vector3.down * groundRayLength, Color.blue);

        // update direction when at an edge
        if (rightendray.collider == null)
        {
            direction = -1;
        }
        else if (leftendray.collider == null)
        {
            direction = 1;
        }
    }

    protected override void Hurt(Vector3 impactDirection)
    {
        // checking impact direction (from sides, top, or bottom)
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y))
        {
            Debug.Log("TakeDamage() here");
        }
        else
        {
            if (impactDirection.y > 0.0f)
            {
                Debug.Log("TakeDamage() here");
            }
            /*Vector2 vel = rb2d.velocity;
            vel.y = jumpforce;
            rb2d.velocity = vel;*/
        }
    }
}
