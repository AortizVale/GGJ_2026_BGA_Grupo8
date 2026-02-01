using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class InteractableWindow : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private const float FADE_ANIMATION_DURATION = 0.25f;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ToggleWindow(bool active, UnityAction callback = null)
    {
        if(!active)
            ToggleInteraction(false);
        
        _canvasGroup.DOFade(active ? 1 : 0, FADE_ANIMATION_DURATION).OnComplete(()=>
        {
            if(active)
                ToggleInteraction(true);
            
            callback?.Invoke();
        });
    }

    protected virtual void ToggleInteraction(bool active)
    {
        _canvasGroup.blocksRaycasts = active;
        _canvasGroup.interactable = active;
    }
}
