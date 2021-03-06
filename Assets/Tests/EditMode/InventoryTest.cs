using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTester
{
    // A Test behaves as an ordinary method
    [Test]
    public void Inventory_Init()
    {
        Inventory inv = new Inventory(UseItem);
        Assert.IsNotNull(inv);
    }
    [Test]
    public void Inventory_Add_Remove_Unique()
    {
        Inventory inv = new Inventory(UseItem);
        Item it = new Item();
        int count = inv.getItemList().Count;
        // Add Unique Item
        Assert.IsTrue(inv.AddItem(it));
        Assert.AreEqual(inv.getItemList().Count, count + 1);

        // Remove Unique Item
        inv.RemoveItem(it);
        Assert.AreEqual(inv.getItemList().Count, count);

    }
    [Test]
    public void Inventory_Add_Remove_Stackable()
    {
        Inventory inv = new Inventory(UseItem);
        Item it = new Item(ItemType.Coin, ItemTier.Common, 1);
        int count = inv.getItemList().Count;
        // Add Unique Item
        Assert.IsTrue(inv.AddItem(it));
        Assert.AreEqual(inv.getItemList().Count, count + 1);

        Assert.IsTrue(inv.AddItem(it));
        Assert.AreEqual(inv.getItemList().Count, count + 1);

        // Remove Unique Item
        inv.RemoveItem(it);
        inv.RemoveItem(it);
        Assert.AreEqual(inv.getItemList().Count, count);

    }
    [Test]
    public void Inventory_Equip_Unequip()
    {
        Inventory inv = new Inventory(UseItem);
        Item it = new Item(ItemType.Body, ItemTier.Legendary, 1);
        inv.AddItem(it);

        // Empty Slot
        Assert.IsNull(inv.getArmor());

        inv.EquipItem(it);

        // Not Empty
        Assert.IsNotNull(inv.getArmor());

        // Shield Greater Than 0
        Assert.IsTrue(inv.GetArmor() > 0);

    }
    [Test]
    public void Inventory_XP()
    {
        Inventory inv = new Inventory(UseItem);
        int XP_NUM = 3;
        Item xp = new Item(ItemType.Xp, ItemTier.Common, XP_NUM);
        inv.AddItem(xp);
        Assert.IsTrue(inv.GetXp() == XP_NUM);

        inv.AddItem(xp);
        Assert.IsTrue(inv.GetXp() == XP_NUM*2);
    }

    private void UseItem(Item item)
    {
        return;
    }
}
