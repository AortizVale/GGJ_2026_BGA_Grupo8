using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCalmTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            GameManager.CalmCanvas.OpenWin();
            GameManager.PlayerCharacter.OnWin();
            GameManager.AuraManager.Win();
        }
    }
}
