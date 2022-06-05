using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // health system
    [Header("Health")]
    [SerializeField] private float respawnHealth;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip respawnSound;

    public float HealthPoints { get; private set; }

    private Animator anim;
    private bool isDead;

    // invulnerability system
    [Header("Invunerability")]
    [SerializeField] private float invunerabilityDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;
   // private float temp = body.gravityScale;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        HealthPoints = respawnHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        HealthPoints = Mathf.Clamp(HealthPoints - damage, 0, respawnHealth);

        if (HealthPoints > 0)
        {
            anim.SetTrigger("damage");
            SoundLogic.instance.PlaySound(damageSound);
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!isDead)
            {
                anim.SetBool("isGrounded", true);
                anim.SetTrigger("death");
                SoundLogic.instance.PlaySound(deathSound);
                GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
            }
        }
    }

    public void AddHealth(float value)
    {
        HealthPoints = Mathf.Clamp(HealthPoints + value, 0, respawnHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.75f);
            yield return new WaitForSeconds(invunerabilityDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(invunerabilityDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    public void Respawn()
    {
        AddHealth(respawnHealth);
        anim.ResetTrigger("death");
        GetComponent<PlayerMovement>().enabled = true;
        
        //body.gravityScale = 0;
        //body.gravityScale = temp;
        GetComponent<PlayerMovement>().transform.position = 
            new Vector2(transform.position.x, -1.7f);
        isDead = false;
        anim.Play("idle");
        SoundLogic.instance.PlaySound(respawnSound);
        StartCoroutine(Invunerability());
    }
}
