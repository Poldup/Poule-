using UnityEngine;

public class weakspot : MonoBehaviour
{
    public float jumpForce;
    public GameObject objetToDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            if(!collision.GetComponent<PlayerControler>().isInvincible())
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
            }
            if(objetToDestroy != null)
            {
                Destroy(objetToDestroy);
            }
        }
    }

}
