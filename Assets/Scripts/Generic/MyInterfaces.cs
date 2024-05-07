public interface IDamage
{
    public void TakeDamage(float damage, bool forceKilled = false);
    public void TakeMaxDamage();
}

public interface IPopup
{
    /// <summary>
    /// Looking at the interactable item should display how to interact with the object.
    /// </summary>
    public void DisplayInteractText();

    /// <summary>
    /// To turn off the popup text when we are not looking at it.
    /// </summary>
    public void HideInteractText();
}

public interface IInteractable
{
    /// <summary>
    /// The main function to interact with the object.
    /// </summary>
    public bool Interact();

}

public interface IPickupable
{
    public void Pickup();
}
