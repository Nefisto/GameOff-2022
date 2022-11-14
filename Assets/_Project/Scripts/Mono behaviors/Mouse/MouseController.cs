using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableContext
{
    
}

public interface IClickable
{
    public void OnPress (ClickableContext context);
    public void OnRelease (ClickableContext context);
}

public class MouseController : MonoBehaviour
{
    [Title("References")]
    public GraphicRaycaster raycaster;

    [Title("Debug")]
    [ReadOnly]
    [SerializeField]
    private SlotAccessor lastClickedSlot;

    private SlotAccessor draggableSlot;

    private Task dragRoutine;
    
    private PointerEventData pointerEventData;
    
    private void Start()
    {
        pointerEventData = new PointerEventData(EventSystem.current);
        
        GameInputAccessor.Gameplay.Click.started += _ => OnPressMouse();
        GameInputAccessor.Gameplay.Click.canceled += _ => OnReleaseMouse();
    }

    private void OnReleaseMouse()
    {
        if (draggableSlot == null)
            return;
        
        Destroy(draggableSlot.gameObject);
        draggableSlot = null;
        
        pointerEventData.position = Input.mousePosition;
        var raycastResults = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, raycastResults);

        if (raycastResults.Count == 0)
            return;

        var foundClickable = raycastResults
            .Select(r => r.gameObject)
            .FirstOrDefault(go => go.TryGetComponent<SlotAccessor>(out _));

        if (foundClickable == null)
            return;

        var newClickedSlot = foundClickable.GetComponent<SlotAccessor>();
        lastClickedSlot.ChangeSlots(newClickedSlot);
    }

    private void OnPressMouse()
    {
        pointerEventData.position = Input.mousePosition;
        var raycastResults = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, raycastResults);

        if (raycastResults.Count == 0)
            return;

        var foundClickable = raycastResults
            .Select(r => r.gameObject)
            .FirstOrDefault(go => go.TryGetComponent<SlotAccessor>(out _));

        if (foundClickable == null)
            return;

        foundClickable.TryGetComponent<SlotAccessor>(out var tempSlotAccessor);
        if (tempSlotAccessor.IsEmpty)
            return;

        lastClickedSlot = tempSlotAccessor;
        
        draggableSlot = Instantiate(lastClickedSlot, transform, true);
        draggableSlot.UpdateSlotAlpha(.2f);
        draggableSlot.GetComponent<Image>().raycastTarget = false;

        dragRoutine?.Stop();
        dragRoutine = new Task(DragRoutine());
    }

    private IEnumerator DragRoutine()
    {
        while (true)
        {
            if (draggableSlot == null)
                yield break;
            
            ((RectTransform)draggableSlot.transform).anchoredPosition = Input.mousePosition;
            
            yield return new WaitForSeconds(.01f);
        }
    }
}