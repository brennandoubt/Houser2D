using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : Controller2D
{
    public float speed = 5;
    public float jumpforce = 5;
    public bool invulnerable;
    public int lives;

    private SpriteRenderer sr;

    public float animationFPS;
    public Sprite[] idleAnimation;
    public Sprite[] walkAnimation;
    public Sprite[] jumpAnimation;
    private float frameTimer = 0;
    private int frameIndex = 0;

    public override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // grounded check (using inherited function from Controller2D)
        UpdateGrounding();

        // move left or right check
        Vector2 vel = rb2d.velocity;
        vel.x = Input.GetAxis("Horizontal") * speed;

        // jump check
        bool inputJump = Input.GetKeyDown(KeyCode.Space);
        if (inputJump && grounded)
        {
            vel.y = jumpforce;

            AnimateJump();
        }

        if (grounded)
        {
            // update player's facing direction
            if (rb2d.velocity.x < -0.01f)
            {
                sr.flipX = true;
                AnimateWalk();
            }
            else if (rb2d.velocity.x > 0.01f)
            {
                sr.flipX = false;
                AnimateWalk();
            }
            else
            {
                AnimateIdle();
            }
        }

        // finally update player's rigid body velocity
        rb2d.velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController controller = collision.gameObject.GetComponent<EnemyController>();
        if (controller != null)
        {
            // calculating direction of the collision
            Vector3 impactDirection = collision.gameObject.transform.position - transform.position;
            Hurt(impactDirection);
            Debug.Log("Hurt(impactDirection) here");
        }
    }

    // Coroutine - stopping and continuing code at a given line
    // Invulnerability Coroutine executes function until it reaches the end, then is destroyed
    // This coroutine is utilized here within the Hurt() function
    protected override void Hurt(Vector3 impactDirection)
    {
        // checking impact direction (from sides, top, or bottom)
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y))
        {
            Debug.Log("TakeDamage() here");
            StartCoroutine(Invulnerability(1));
        }
        else
        {
            if (impactDirection.y > 0.0f)
            {
                Debug.Log("TakeDamage() here");
                StartCoroutine(Invulnerability(1));
            }
            Vector2 vel = rb2d.velocity;
            vel.y = jumpforce;
            rb2d.velocity = vel;
        }
    }

    IEnumerator Invulnerability(float time)
    {
        lives--; // decrease lives by 1
        invulnerable = true;
        for (int i = 0; i < time / 0.2f; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        invulnerable = false;
    }


    void AnimateIdle()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;
            frameIndex %= idleAnimation.Length;
            sr.sprite = idleAnimation[frameIndex];
            frameIndex++;
        }
    }

    void AnimateWalk()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;
            frameIndex %= idleAnimation.Length;
            sr.sprite = walkAnimation[frameIndex];
            frameIndex++;
        }
    }

    void AnimateJump()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;
            frameIndex %= idleAnimation.Length;
            sr.sprite = jumpAnimation[frameIndex];
            frameIndex++;
        }
    }
}
