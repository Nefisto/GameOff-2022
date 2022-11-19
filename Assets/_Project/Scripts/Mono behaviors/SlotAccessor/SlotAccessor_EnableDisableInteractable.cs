public partial class SlotAccessor
{
    public void EnableInteraction()
    {
        clickableAreaTarget.raycastTarget = true;
        disabledOverlapImage.enabled = false;
    }

    public void DisableInteraction()
    {
        clickableAreaTarget.raycastTarget = false;
        disabledOverlapImage.enabled = true;
    }
}