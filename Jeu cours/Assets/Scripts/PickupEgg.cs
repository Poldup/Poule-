using UnityEngine;

public class PickupEgg : MonoBehaviour
{
    private bool cantDo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule") && !cantDo)
        {
            cantDo = true;
            Destroy(gameObject);
            GameManager.Instance.AddEggs(1);
        }
    }
}
