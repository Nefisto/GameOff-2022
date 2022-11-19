using System;
using System.Collections;
using DG.Tweening;
using NTools;
using Sirenix.OdinInspector;
using UnityEngine;

public partial class Inventory
{
    private enum InventoryCollapseState
    {
        Collapsed,
        Uncollapsed,
        OnProcess
    }
    
    [Title("Debug collapse values")]
    [ReadOnly]
    [SerializeField]
    private InventoryCollapseState currentCollapseState = InventoryCollapseState.Collapsed;

    [SerializeField]
    private float collapsedXPosition;
    [SerializeField]
    private float uncollapsedXPosition;

    public void ToggleInventoryCollapse()
    {
        switch (currentCollapseState)
        {
            case InventoryCollapseState.Collapsed:
                UncollapseInventory();
                break;

            case InventoryCollapseState.Uncollapsed:
                CollapseInventory();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void SetupCollapseValues()
    {
        collapsedXPosition = rectTransform.anchoredPosition.x;
        uncollapsedXPosition = collapsedXPosition + rectTransform.sizeDelta.x;
    }
    
    private void CollapseInventory()
    {
        currentCollapseState = InventoryCollapseState.OnProcess;
        items.ForEach(sa => sa.DisableInteraction());

        rectTransform
            .DOAnchorPosX(collapsedXPosition, 1f)
            .OnComplete(() => currentCollapseState = InventoryCollapseState.Collapsed);
    }

    private void UncollapseInventory()
    {
        currentCollapseState = InventoryCollapseState.OnProcess;
        items.ForEach(sa => sa.DisableInteraction());

        rectTransform
            .DOAnchorPosX(uncollapsedXPosition, 1f)
            .OnComplete(() =>
            {
                currentCollapseState = InventoryCollapseState.Uncollapsed;
                items.ForEach(sa => sa.EnableInteraction());
            });
    }
}