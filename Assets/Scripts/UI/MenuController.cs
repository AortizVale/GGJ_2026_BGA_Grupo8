using DG.Tweening;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private const float FADE_ANIMATION_DURATION = 0.25f;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ToggleMenuContainer(bool active)
    {
        _canvasGroup.DOFade(active ? 1 : 0, FADE_ANIMATION_DURATION).OnComplete(()=>
        {

        });
    }
}
