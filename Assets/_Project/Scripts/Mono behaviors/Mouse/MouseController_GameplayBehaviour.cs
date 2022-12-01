using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class MouseController
{
    [Title("Debug - Gameplay behaviour")]
    [ReadOnly]
    [SerializeField]
    private SlotAccessor lastClickedSlot;

    private SlotAccessor draggableSlot;
    private Task dragRoutine;
    private PointerEventData pointerEventData;
    
    private void CheckForDragRelease()
    {
        if (draggableSlot == null)
            return;
        
        RemoveCachedDraggable();

        if (!TryGetSlotAccessorThoughtClick(out var foundClickable))
            return;

        var newClickedSlot = foundClickable.GetComponent<SlotAccessor>();
        lastClickedSlot.ChangeSlots(newClickedSlot);
    }

    private void CheckForDragBegin()
    {
        if (!TryGetSlotAccessorThoughtClick(out var foundClickable))
            return;
        
        if (foundClickable.IsEmpty)
            return;

        lastClickedSlot = foundClickable;
        
        draggableSlot = Instantiate(lastClickedSlot, dragItemFolder, true);
        // ((RectTransform)draggableSlot.transform).anchorMin = new Vector2(0, 1);
        // ((RectTransform)draggableSlot.transform).anchorMax = new Vector2(0, 1);
        draggableSlot.UpdateSlotAlpha(.2f);
        draggableSlot.GetComponent<Image>().raycastTarget = false;

        dragRoutine?.Stop();
        dragRoutine = new Task(DragRoutine());
    }

    private void RemoveCachedDraggable()
    {
        if (draggableSlot == null)
            return;
        
        Destroy(draggableSlot.gameObject);
        draggableSlot = null;
    }

    private bool TryGetSlotAccessorThoughtClick (out SlotAccessor foundSlot)
    {
        var raycastResults = GetClickedObjects();
        
        if (raycastResults.Count == 0)
        {
            foundSlot = null;
            return false;
        }

        foundSlot = raycastResults
            .Select(r => r.gameObject.GetComponent<SlotAccessor>())
            .Where(sl => sl != null)
            .FirstOrDefault(sa => sa);
        
        return foundSlot != null;
    }

    private List<RaycastResult> GetClickedObjects()
    {
        pointerEventData.position = Input.mousePosition;
        
        var raycastResults = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, raycastResults);
        
        return raycastResults;
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