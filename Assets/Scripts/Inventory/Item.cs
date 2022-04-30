using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

    public enum ItemType 
    {
        Pistol,
        AR,
        RocketLauncher,
        Head,
        Body,
        HealthPotion,
        Coin,
        Xp
    }
    public enum ItemTier
    {
        Common,
        Good,
        Rare,
        Legendary
    }

public class Item 
{

    public ItemType itemType;
    public ItemTier itemTier;
    public int amount;

    public Item()
    {

    }
    public Item(ItemType itype, ItemTier itier, int am)
    {
        this.itemType = itype;
        this.itemTier = itier;
        this.amount = am;
    }
    public Item(ItemStructNetcode isn)
    {
        this.itemType = isn.itemType;
        this.itemTier = isn.itemTier;
        this.amount = isn.amount;
    }
    public Sprite GetSprite()
    {
        switch(itemType)
        {
            case ItemType.Pistol:    return ItemAssets.Instance.pistolSprite;
            case ItemType.AR:    return ItemAssets.Instance.arSprite;
            case ItemType.RocketLauncher:    return ItemAssets.Instance.rocketLauncherSprite;
            case ItemType.Head:    return ItemAssets.Instance.headSprite;
            case ItemType.Body:   return ItemAssets.Instance.bodySprite;
            case ItemType.HealthPotion:    return ItemAssets.Instance.healthPotionSprite;
            case ItemType.Coin:   return ItemAssets.Instance.coinSprite;
            case ItemType.Xp: return ItemAssets.Instance.xpSprite;
            default: return ItemAssets.Instance.notFound;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.Xp:
                return true;
            case ItemType.Pistol:
            case ItemType.AR:
            case ItemType.RocketLauncher:
            case ItemType.Body:
            case ItemType.Head:
                return false;
        }
    }

    public Item clone()
    {
        return new Item() { amount = this.amount, itemType = this.itemType };
    }

}
