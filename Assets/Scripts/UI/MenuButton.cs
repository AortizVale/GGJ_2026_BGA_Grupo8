using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [SerializeField]
    protected InteractableWindow _interactableWindow;

    private MenuController _menuController;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _menuController = GetComponentInParent<MenuController>();
    }

    public virtual void OpenInteractableWindow()
    {
        if (!_interactableWindow) return;
        _menuController.SwitchToWindow(_interactableWindow);
    }

    public void SwitchToScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
