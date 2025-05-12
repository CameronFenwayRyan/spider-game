using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public GameObject segmentPrefab;
    public float moveInterval = 0.3f;
    private Vector2Int direction = Vector2Int.right;
    private List<Transform> segments = new List<Transform>();
    private float moveTimer;
    private bool isGameOver = false;

    void Start()
    {
        segments.Add(this.transform);
    }

    void Update()
    {
        // Prevent the snake from moving in the opposite direction
    if (Input.GetKeyDown(KeyCode.UpArrow)) 
        direction = Vector2Int.up;
    if (Input.GetKeyDown(KeyCode.DownArrow)) 
        direction = Vector2Int.down;
    if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        direction = Vector2Int.left;
    if (Input.GetKeyDown(KeyCode.RightArrow)) 
        direction = Vector2Int.right;

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            Move();
            moveTimer = 0f;
        }
    }

    void Move()
    {
        // Move the body segments of the snake
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Move the snake's head in the current direction
        transform.position = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y),
            0f
        );
    }

    void OnSnakeCollision()
    {
        Debug.Log("Game Over! Snake collided with itself.");
        gameObject.SetActive(false); // Deactivate the snake
    }
}
