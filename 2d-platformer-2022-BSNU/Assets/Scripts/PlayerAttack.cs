using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float fireballCastDelay;
    [SerializeField] private Transform castPoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private PlayerMovement playerMovement;

    private float castDelayTimer = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && playerMovement.attackAvaible() && castDelayTimer > fireballCastDelay)
            FireballCast();
        
        castDelayTimer += Time.deltaTime;
    }

    // find nit used at the moment fireball
    private int FindFreeProjectile()
    {
        for (int i = 0; i < fireballs.Length; i++)
            if (!fireballs[i].activeInHierarchy)
                return i;
        return 0;
    }

    private void FireballCast()
    {
        SoundLogic.instance.PlaySound(fireballSound);
        anim.SetTrigger("fireballCast");
        

        // pool fireballs
        fireballs[FindFreeProjectile()].transform.position = castPoint.position;
        fireballs[FindFreeProjectile()].GetComponent<FireballProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        castDelayTimer = 0;

        
    }

}
