using System;
using DG.Tweening;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private InteractableWindow _menuContainer;

    private InteractableWindow _currentActiveWindow;

    private void Start()
    {
        _currentActiveWindow = _menuContainer;
    }

    public void SwitchToWindow(InteractableWindow window)
    {
        if (window == _currentActiveWindow || window == null) return;
        
        _currentActiveWindow.ToggleWindow(false, () =>
        {
            window.ToggleWindow(true);
            _currentActiveWindow = window;  
        });
    }
}