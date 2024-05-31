using UnityEngine;
using System.Collections.Generic;
using System;

public class BaseStorage<T> where T : IStorable
{
    public List<T> storage;
    protected int storageSize;

    public BaseStorage(int storageSize)
    {
        this.storage = new List<T>();
        storage.Capacity = storageSize;
        this.storageSize = storageSize;
    }

    /// <summary>
    /// Adds new item to inventory. If it is stackable object and if there exist a empty space at stack, increase amount of stack.
    /// If there is no any empty stack, adds new item to inventory if there is a capacity.
    /// </summary>
    /// <param name="newItem">Item that will add.</param>
    /// <returns>Whether adding is succefull or not.</returns>
    public virtual bool AddItem(T newItem)
    {
        
        // If stacakable, find stack and add it to it.
        if (newItem is IStackable stackItem)
        {
            List<T> allStacks = storage.FindAll(item => item.ItemId == newItem.ItemId);
            //Finds stack and add it to it if there is a space.
            
            foreach (T item in allStacks)
            {
                IStackable stack = (IStackable)item;
                if (stack.Amount < stack.StackSize)
                {
                    stack.Amount++;
                    return true;
                }

            }
           
            // There is no any stack.
            storage.Add(newItem);
            stackItem.Amount++;
            
            return true;
        }
        

        // If not stackable or there is no empty space in stack, add it to empty space.
        if (storage.Count <= storage.Capacity)
        {
            storage.Add(newItem);

            return true;
        }

        

        return false; // There is no empty space.
    }





    public virtual int RemoveItem(string ItemID,int amount)
    {

        // Find the object in inventory.
        // -- If it is stackable and there is a amount, decrease it.
        // -- If there is no amount or not stackable, directly remove it from storage.


        int willDeleteCount = amount;

        for(int i = 0;i < storage.Count;i++)
        {

            T item = storage[i];


            if (item.ItemId.Equals(ItemID))
            {
                if (item is IStackable stackable)
                {

                    if (stackable.Amount > 0)
                    {
                        
                        stackable.Amount -= 1;
                        willDeleteCount--;

                        
                        if (stackable.Amount == 0)
                        {
                    
                            storage.RemoveAt(i);
                        

                        }

                        continue;
                    }
                   

                }

                
                storage.RemoveAt(i);
                willDeleteCount--;
                continue;
            }
        }

        return amount - willDeleteCount;
    }



    public virtual int Count()
    {
        return storage.Count;
    }

    public virtual int Capacity()
    {
        return storage.Capacity;
    }


}
