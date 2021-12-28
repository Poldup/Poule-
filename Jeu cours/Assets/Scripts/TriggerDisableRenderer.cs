using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerDisableRenderer : MonoBehaviour
{
    public Renderer rendererToDisabled;
    public bool activateOnEnter;

    void Start()
    {
        rendererToDisabled.enabled = !activateOnEnter;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Poule"))
        {
            rendererToDisabled.enabled = activateOnEnter;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Poule"))
        {
            rendererToDisabled.enabled = !activateOnEnter;
        }
    }
}
