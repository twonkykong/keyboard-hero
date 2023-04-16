using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MapCreatorKeyAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private RectTransform settingsMenu;

    private RectTransform _rectTransform;
    private float _currentSize = 1;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _currentSize = _rectTransform.localScale.x;
        _rectTransform.DOScale(_currentSize + 0.05f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.DOScale(_currentSize, 0.1f);
        settingsMenu.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (settingsMenu.gameObject.activeInHierarchy) return;

        settingsMenu.gameObject.SetActive(true);
        settingsMenu.anchoredPosition = Vector2.zero;
        Vector2 settingsMenuScale = Vector2.one / _currentSize;
        settingsMenu.localScale = new Vector2(settingsMenuScale.x, settingsMenuScale.y * 0.8f);
        settingsMenu.DOScale(settingsMenuScale, 0.08f);
    }
}
