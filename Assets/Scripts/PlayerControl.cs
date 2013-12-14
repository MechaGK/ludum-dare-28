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
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 pos = transform.position;
        Debug.DrawLine(pos, pos + (Vector2.right * (spriteSize.x / 2) * Mathf.Sign(h)));
        if (!Physics2D.Linecast(pos, pos + (Vector2.right * ((spriteSize.x / 2) + 0.02f) * Mathf.Sign(h)), 1 << LayerMask.NameToLayer("Solid")))
        {
            transform.Translate(Vector2.right * h * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            print("Jump!");
            jump = true;
            rigidbody2D.AddForce(Vector2.up * 25);
        }

        if (!Physics2D.Linecast(pos, pos - Vector2.up * ((spriteSize.y / 2) + 0.1f), 1 << LayerMask.NameToLayer("Solid")))
        {
            grounded = false;
            print("not grounded!");
        }
        else
        {
            grounded = true;
        }


        float timeModifier = Mathf.Abs(h) * (1f / 50f);
        timeModifier += 1 / 150f;
        timeModifier += grounded ? 0 : 1f / 25f;

        TimeControl.timeModifier = timeModifier;
    }
}
