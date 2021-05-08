using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float speed = 1f;
    bool jump = false;
    bool grounded;
    Vector2 spriteSize;
    public GameObject levelEnd;
    Vector3 spawn;
    public GameObject deathTrail;
    bool controlable = true;
    bool respawing = false;
    public float timeSinceSpawn = 0;
    float jumpTimer = 0;
    float h;
    int ticks;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        Sprite sprite = _spriteRenderer.sprite;
        spriteSize = new Vector2(sprite.bounds.max.x, sprite.bounds.max.y);
        spawn = StagePropeties.Current.spawn.position;

        Camera.main.transform.Find("Watch").GetComponent<Watch>().player = this;
    }

    void Update()
    {
        if (controlable)
        {
            Vector2 pos = transform.position;

            if (Physics2D.OverlapArea(pos + Vector2.right * (spriteSize.x - 0.02f),
                pos - Vector2.up * ((spriteSize.y) + 0.02f) - Vector2.right * (spriteSize.x - 0.02f),
                (1 << LayerMask.NameToLayer("Platforms")) | (1 << LayerMask.NameToLayer("Floors"))))
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

            GetComponent<Rigidbody2D>().AddForce(Vector2.zero);

            if (transform.position.y < StagePropeties.Current.stageDimensions.y)
            {
                StartCoroutine(Die());
            }

            timeSinceSpawn += TimeControl.DeltaTime;
            print(timeSinceSpawn);

            if (timeSinceSpawn >= 1f)
            {
                StartCoroutine(Die());
            }
        }
    }

    void FixedUpdate()
    {
        if (controlable)
        {
            h = Input.GetAxis("Horizontal");
            Vector2 pos = transform.position;

            if (!Physics2D.OverlapArea(pos + Vector2.up * (spriteSize.y - 0.001f),
                pos + (Vector2.right * ((spriteSize.x + 0.02f) * Mathf.Sign(h))) - Vector2.up * (spriteSize.y - 0.001f),
                (1 << LayerMask.NameToLayer("Platforms")) | (1 << LayerMask.NameToLayer("Wall"))))
            {
                _rigidbody2D.velocity = new Vector3(h * speed * Time.deltaTime, _rigidbody2D.velocity.y, 0);
            }

            if (Input.GetButton("Jump"))
            {
                if (grounded)
                {
                    jump = true;
                    _rigidbody2D.AddForce(Vector2.up * 24);
                    jumpTimer = 0;
                    ticks = 0;
                }
                else if (jump && ticks < 6)
                {
                    _rigidbody2D.AddForce(Vector2.up * 3.5f);
                    ticks += 1;
                }
            }
            else
            {
                jump = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Goal"))
        {
            CameraFollow.Main.player = col.transform;
            TimeControl.timeModifier = 1;
            Instantiate(levelEnd, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator Die()
    {
        if (!respawing)
        {
            respawing = true;

            _spriteRenderer.enabled = false;
            TimeControl.timeModifier = 1;
            float timer = 0;
            controlable = false;
            GameObject effect = Instantiate(deathTrail, transform.position, Quaternion.identity) as GameObject;
            CameraFollow.Main.player = effect.transform;

            while (timer <= 1f)
            {
                effect.transform.position = Vector3.Lerp(transform.position, spawn, timer);
                timer += Time.deltaTime / 2;
                timeSinceSpawn += TimeControl.DeltaTime;
                yield return new WaitForEndOfFrame();
            }

            transform.position = spawn;
            effect.GetComponent<ParticleSystem>().Stop();
            CameraFollow.Main.player = transform;
            Destroy(effect, 5);
            controlable = true;
            timeSinceSpawn = 0f;
            TimeControl.timeModifier = 0f;
            _spriteRenderer.enabled = true;

            respawing = false;
        }
    }
}
