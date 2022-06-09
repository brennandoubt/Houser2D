using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Need camera to scale to aspect ratio
 * 
 * 
 */
public class CameraController2D : MonoBehaviour
{
    public Transform target; // the player
    public float speed;
    public float globalMaxX;
    public float globalMaxY;
    public float globalMinX;
    public float globalMinY;

    public SpriteRenderer background;
    void Start()
    {
        background = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 start = transform.position;
        Vector3 goal = target.position + new Vector3(0.0f, 0.0f, -10);
        float t = Time.deltaTime * speed;
        Vector3 newPosition = Vector3.Lerp(start, goal, t);

        // camera positioning based on its size and aspect ratio
        float maxX = globalMaxX - Camera.main.orthographicSize * Camera.main.aspect;
        float maxY = globalMaxY - Camera.main.orthographicSize;
        float minX = globalMinX + Camera.main.orthographicSize * Camera.main.aspect;
        float minY = globalMinY + Camera.main.orthographicSize;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // finally setting camera with new position
        transform.position = newPosition;
    }

    // using Gizmos to visualize boundaries
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY, 0.0f), new Vector3(globalMaxX, globalMinY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMaxY, 0.0f), new Vector3(globalMaxX, globalMaxY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY, 0.0f), new Vector3(globalMinX, globalMaxY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMaxX, globalMinY, 0.0f), new Vector3(globalMaxX, globalMaxY, 0.0f));
    }
}
