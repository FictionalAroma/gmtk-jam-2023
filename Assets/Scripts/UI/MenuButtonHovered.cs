using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonHovered : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image unselected;
    [SerializeField] private Image selected;

    private void Start()
    {
        unselected.enabled = true;
        selected.enabled = false;
    }

    // When the mouse enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        EnableSelectionImage(true);
    }

    // When the mouse exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        EnableSelectionImage(false);
    }

    private void EnableSelectionImage(bool enable)
    {
        unselected.enabled = !enable;
        selected.enabled = enable;
    }
}