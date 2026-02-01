using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HideBehindPlayer : MonoBehaviour
{
    [Header("Sorting Layers")]
    public string backgroundLayer = "Background";
    public string hidePlayerLayer = "HidePlayer";

    [Header("Order in Layer")]
    public int backgroundOrder = 1;
    public int hidePlayerOrder = 0;

    private SpriteRenderer sr;
    private Transform player;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (player == null)
        {
            if (GameManager.PlayerCharacter != null)
                player = GameManager.PlayerCharacter.transform;
            else
                return;
        }

        // Player delante (más abajo en Y)
        if (player.position.y < transform.position.y)
        {
            sr.sortingLayerName = backgroundLayer;
            sr.sortingOrder = backgroundOrder;
        }
        // Player detrás (más arriba en Y)
        else
        {
            sr.sortingLayerName = hidePlayerLayer;
            sr.sortingOrder = hidePlayerOrder;
        }
    }

    public void ForceBackground()
    {
        sr.sortingLayerName = backgroundLayer;
        sr.sortingOrder = backgroundOrder;

        // Detenemos la lógica automática
        enabled = false;
    }

}
