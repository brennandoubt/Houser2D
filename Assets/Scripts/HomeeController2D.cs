using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * https://generalistprogrammer.com/game-design-development/unity-drag-and-drop-tutorial/
 * 
 * ^ Most code retrieved from above website for drag-and-drop
 * implementation for game objects.
 * 
 * 
 */
public class HomeeController2D : MonoBehaviour
{
    bool canMove;
    bool dragging;
    private Collider2D coll;
    //private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;
        //audioClip = GetComponent<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (coll == Physics2D.OverlapPoint(mousePos))
            {
                canMove = true;
            } else
            {
                canMove = false;
            }

            if (canMove)
            {
                dragging = true;
            }
        }

        if (dragging)
        {
            this.transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }
}
