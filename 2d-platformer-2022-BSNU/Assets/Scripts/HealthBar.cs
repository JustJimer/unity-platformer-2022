using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.HealthPoints / 10;
    }
    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.HealthPoints / 10;
    }
}