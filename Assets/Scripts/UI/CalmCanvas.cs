using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CalmCanvas : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Win")]
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        OpenHUD();
    }

    // ---------- HUD ----------

    public void UpdateTime(int time)
    {
        timeText.text = $"Calm: {time}";
    }

    public void OpenHUD()
    {
        hudPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    // ---------- GAME OVER ----------

    public void OpenGameOver()
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    // ---------- Win ----------

    public void OpenWin()
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void ReturnToForest()
    {
        SceneManager.LoadScene(1);
    }
}
