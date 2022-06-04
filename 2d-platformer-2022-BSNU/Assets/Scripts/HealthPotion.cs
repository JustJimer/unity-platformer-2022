using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip healingSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<HealthSystem>().AddHealth(healthValue);
            SoundLogic.instance.PlaySound(healingSound);
            gameObject.SetActive(false);
        }
    }
}