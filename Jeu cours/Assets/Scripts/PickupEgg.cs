using UnityEngine;

public class PickupEgg : MonoBehaviour
{
    [HideInInspector]
    public bool canDo = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            if (canDo)
            {
                GameManager.Instance.Pickoeuf(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
