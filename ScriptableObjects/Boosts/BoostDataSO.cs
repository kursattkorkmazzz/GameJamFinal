using UnityEngine;

public abstract class BoostDataSO :ScriptableObject, IStorable, IStackable, IConsumable
{
    public abstract string ItemId{ get; set; }
    public abstract Sprite IconUI { get; set; }
    public abstract int Amount { get; set; }
    private int stackSize = int.MaxValue;
    public int StackSize { get { return stackSize; } set { stackSize = int.MaxValue; } }


    public abstract void Use(GameObject target);
}
