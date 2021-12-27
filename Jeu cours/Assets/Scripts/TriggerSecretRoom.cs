using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerSecretRoom : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Poule"))
        {
            tilemapRenderer.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Poule"))
        {
            tilemapRenderer.enabled = true;
        }
    }
}
