using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    [SerializeField] private AudioClip winSound;
    private Transform latestCheckpoint;
    private HealthSystem playerHealth;
    private Animator anim;

    private void Start()
    {
        playerHealth = GetComponent<HealthSystem>();
        anim = GetComponent<Animator>();
    }

    public void Respawn()
    {
        playerHealth.Respawn(); //Restore player health and reset animation
        transform.position = latestCheckpoint.position; //Move player to checkpoint location

        //Move the camera to the checkpoint's room
      //  Camera.main.GetComponent<CameraMoving>().MoveToNewRoom(latestCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            latestCheckpoint = collision.transform;
            SoundLogic.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
        else if (collision.gameObject.tag == "Trophy")
        {
            SoundLogic.instance.PlaySound(winSound);
            GetComponent<PlayerMovement>().enabled = false;
            collision.GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("happy");
            collision.GetComponent<Animator>().SetTrigger("win");
        }
    }
}