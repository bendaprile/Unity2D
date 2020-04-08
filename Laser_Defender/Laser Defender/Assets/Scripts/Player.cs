using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Used to add padding to limiting the sprite to the play space
    float spriteXSize;
    float spriteYSize;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        var sprite = GetComponent<SpriteRenderer>();

        // Grab the X and Y size of the sprite from center to right/top
        spriteXSize = sprite.bounds.extents.x;
        spriteYSize = sprite.bounds.extents.y;


        // Create the minimum and maximum x values that we can travel to
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        // Create the minimum and maximum y values that we can travel to
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        // Grab the change in X for this frame along the Horizontal axis
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Limits the new y position of our current plus the change in y to be between yMin and yMax
        var newXPos = Mathf.Clamp((transform.position.x + deltaX), xMin + spriteXSize, xMax - spriteXSize);
        var newYPos = Mathf.Clamp((transform.position.y + deltaY), yMin + spriteYSize, yMax - spriteYSize);


        // Update x position to our newly found x position
        transform.position = new Vector2(newXPos, newYPos);
    }
}
