using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform handleRect;
    [SerializeField] private float handleRange = 50;
    private RectTransform baseRect;

    public Vector2 Direction;

    private int? activePointerID;

    private void Awake()
    {
        baseRect = this.GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        // exit if 2nd pointer is detected, so only 1st pointer can affect joystick
        if (activePointerID != eventData.pointerId) return;

        // exit if bad input
        Vector2 local;
        bool inside = RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, eventData.position, eventData.pressEventCamera, out local);
        if (!inside) return;

        // clamp handle movement to handleRange radius, then set normalized Direction
        Vector2 clamped = Vector2.ClampMagnitude(local, handleRange);
        handleRect.anchoredPosition = clamped;
        Direction = clamped / handleRange;
        //Debug.Log(Direction);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // set current mouse pointer ID to prevent 2nd pointer causing issues
        if (activePointerID != null) return;
        activePointerID = eventData.pointerId;
        //Debug.Log("down");

        // call OnDrag so joystick starts responding immediately even if player doesn't move pointer
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // make activePointerID available only if current active pointer is released
        if (activePointerID != eventData.pointerId) return;
        activePointerID = null;

        handleRect.anchoredPosition = Vector2.zero;
        Direction = Vector2.zero;
        //Debug.Log("up");
    }
}
