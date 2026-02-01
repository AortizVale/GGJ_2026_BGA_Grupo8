using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [SerializeField]
    protected CanvasGroup _interactableWindow;

    protected MenuController _menuController;
    protected Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _menuController = GetComponentInParent<MenuController>();
    }

    protected virtual void ActivateButton()
    {
        if (_interactableWindow)
        {
            //_interactableWindow.DOFade(1, );
            if (_menuController)
                _menuController.ToggleMenuContainer(false);
        }
    }

}
