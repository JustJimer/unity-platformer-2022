using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float respawnHealth;

    public float HealthPoints { get; private set; }

    private Animator anim;
    private bool isDead;

    void Start()
    {
        HealthPoints = respawnHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        HealthPoints = Mathf.Clamp(HealthPoints - damage, 0, respawnHealth);

        if (HealthPoints > 0)
        {
            anim.SetTrigger("damage");
            //iframes
        }
        else
        {
            if (!isDead)
            {
                anim.SetTrigger("death");
                GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
            }
        }
    }
    public void AddHealth(float value)
    {
        HealthPoints = Mathf.Clamp(HealthPoints + value, 0, respawnHealth);
    }


}
