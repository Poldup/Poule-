using UnityEngine;

public class PickupEgg : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            GameManager.Instance.Pickoeuf(gameObject);
            gameObject.SetActive(false);
        }
    }
}
