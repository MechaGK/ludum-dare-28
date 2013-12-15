using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    Vector2 spriteSize;
    int direct = 1;
    public float speed = 4;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        spriteSize = new Vector2(sprite.bounds.max.x * 2, sprite.bounds.max.y * 2);
    }

    void Update()
    {
        Vector2 pos = transform.position;

        if (Physics2D.Linecast(pos, pos + (Vector2.right * (spriteSize.x) * direct), (1 << LayerMask.NameToLayer("Floor")) | (1 << LayerMask.NameToLayer("Walls"))))
        {
            direct = -direct;
        }

        transform.Translate(Vector2.right * speed * TimeControl.DeltaTime * direct);
    }


}
