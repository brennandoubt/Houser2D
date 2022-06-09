using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller2D : MonoBehaviour
{
    protected Rigidbody2D rb2d;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // set up for grounding checks
    public bool grounded = false;
    public LayerMask groundLayers;
    public float groundRayLength = 0.2f;
    public float groundRaySpread = 0.45f;
    protected void UpdateGrounding()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector3.down, groundRayLength, groundLayers);
        // drawing red lines to visualize raycasts
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundRayLength, Color.red);

        // adding additional rays on left and right of player
        RaycastHit2D leftray = Physics2D.Raycast(transform.position + Vector3.left * groundRaySpread, Vector3.down, groundRayLength, groundLayers);
        RaycastHit2D rightray = Physics2D.Raycast(transform.position + Vector3.right * groundRaySpread, Vector3.down, groundRayLength, groundLayers);

        Vector3 leftorigin = transform.position + Vector3.left * groundRaySpread;
        Vector3 rightorigin = transform.position + Vector3.right * groundRaySpread;
        Debug.DrawLine(leftorigin, leftorigin + Vector3.down * groundRayLength, Color.red);
        Debug.DrawLine(rightorigin, rightorigin + Vector3.down * groundRayLength, Color.red);

        // use rays to check if player is grounded
        if (leftray.collider != null || rayhit.collider != null || rightray.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    protected abstract void Hurt(Vector3 impactDirection);
}
