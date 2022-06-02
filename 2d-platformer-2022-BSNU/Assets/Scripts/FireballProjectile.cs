using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private bool hit;
    private float flytime;
    private float direction;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        flytime += Time.deltaTime;
        if (flytime > 5) Vanish();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("fireballHit");
        hit = true;
        boxCollider.enabled = false;
    }

    public void SetDirection(float direction)
    {
        this.direction = direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        flytime = 0;

        float xScale = transform.localScale.x;
        if (Mathf.Sign(xScale) != direction)
            xScale = -xScale;
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }

    private void Vanish()
    {
        gameObject.SetActive(false);
    }
}
