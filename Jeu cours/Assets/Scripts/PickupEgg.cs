using UnityEngine;

public class PickupEgg : MonoBehaviour
{
    [HideInInspector]
    public bool canDo = true;
    public AudioClip sound;
    public float volume;
    public AudioSource source;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            if (canDo)
            {
                canDo = false;
                GameManager.Instance.Pickoeuf(gameObject);
                AudioManager.Instance.PlaySound(sound, volume);
                gameObject.SetActive(false);
            }
        }
    }
}
