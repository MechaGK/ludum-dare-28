using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float speed = 1f;
    bool jump = false;
    bool grounded;
    Vector2 spriteSize;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        spriteSize = new Vector2(sprite.bounds.max.x * 2, sprite.bounds.max.y * 2);
        Camera.main.GetComponent<CameraFollow>().player = transform;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 pos = transform.position;
        Debug.DrawLine(pos, pos + (Vector2.right * (spriteSize.x / 2) * Mathf.Sign(h)));
        if (!Physics2D.Linecast(pos, pos + (Vector2.right * ((spriteSize.x / 2) + 0.02f) * Mathf.Sign(h)), 1 << LayerMask.NameToLayer("Solid")))
        {
            rigidbody2D.velocity = new Vector3(h * speed * Time.deltaTime, rigidbody2D.velocity.y, 0);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
            rigidbody2D.AddForce(Vector2.up * 30);
        }

        if (Physics2D.Linecast(pos, pos - Vector2.up * ((spriteSize.y / 2) + 0.1f), 1 << LayerMask.NameToLayer("Solid")))
        {
            grounded = true;
        }
        else if (Physics2D.Linecast(pos, pos - Vector2.up * ((spriteSize.y / 2) + 0.1f), 1 << LayerMask.NameToLayer("MovingSolid")))
        {
            grounded = true;
            
        }
        else
        {
            grounded = false;
        }


        float timeModifier = Mathf.Abs(h) * (1f / 45f);
        timeModifier += 1 / 150f;
        timeModifier += grounded ? 0 : 1f / 20f;

        TimeControl.timeModifier = timeModifier;

        rigidbody2D.AddForce(Vector2.zero);
    }
}
