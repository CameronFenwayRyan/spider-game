using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControls : MonoBehaviour
{
    public LayerMask wallLayer;
    public float normalGravity = 3f;
    public float launchForce = 15f;
    public float maxDragDistance = 2f;

    private Rigidbody2D rb;
    private CircleCollider2D col;

    private Vector2 dragStart;
    private bool isDragging = false;
    public LineRenderer dragLineRenderer;
    private bool initializeLine = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        rb.gravityScale = normalGravity;
    }

    void Update()
    {
        bool isTouchingWall = IsTouchingWall();
        bool isHolding = Input.GetMouseButton(0);

        // Start drag if touching wall and holding click
        if (isTouchingWall && isHolding && !isDragging)
        {
            isDragging = true;
        }

        // Cling while dragging and overlapping wall
        if (isDragging && isTouchingWall)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            if (initializeLine && rb.velocity.magnitude < 0.05f)
            {
                Debug.Log("Spider is sleeping");
                dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragLineRenderer.SetPosition(0, dragStart);
                initializeLine = false;
            }
            dragLineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        // Release: throw the spider
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = dragEnd - dragStart; // same direction as swipe

            dragVector = Vector2.ClampMagnitude(dragVector, maxDragDistance);

            rb.gravityScale = normalGravity;
            rb.velocity = Vector2.zero;
            rb.AddForce(dragVector * launchForce, ForceMode2D.Impulse);

            isDragging = false;
            initializeLine = true;
        }

        // Restore gravity if not clinging or dragging
        if (!isHolding && !isDragging)
        {
            rb.gravityScale = normalGravity;
        }
    }

    bool IsTouchingWall()
    {
        return Physics2D.IsTouchingLayers(col, wallLayer);
    }
}
