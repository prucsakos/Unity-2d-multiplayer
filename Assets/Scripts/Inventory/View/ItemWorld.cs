using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

/// <summary>
/// A wrapper for Item.
/// </summary>
public class ItemWorld : NetworkBehaviour
{
    // Static calls.
    public static ItemWorld SpawnItemWorld(Vector3 position ,Item item)
    {
        
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity, GameManager.Instance.SpawnedObjects);

        ItemWorld iw = transform.GetComponent<ItemWorld>();
        iw.SetItem(item);

        return iw;
    }
    
    // 
    public static ItemWorld DropItem(Vector3 dropPos, Vector3 direction, Item item)
    {
        Vector3 dir = new Vector3(direction.x, direction.y).normalized;
        Vector3 pos = dropPos; // + dir * 0.2f;
        ItemWorld iw = SpawnItemWorld(pos , item.clone());
        iw.GetComponent<Rigidbody2D>().AddForce(dir * 1.3f, ForceMode2D.Impulse);
        return iw;
    }
    public NetworkVariable<ItemStructNetcode> ItemStructNetVar = new NetworkVariable<ItemStructNetcode>();
    [SerializeField] private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    public bool IsPickupAble = true;
    public bool isItemSet = false;

    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        textMeshPro = transform.Find("amountText").GetComponent<TextMeshPro>();

        IsPickupAble = false;
        Invoke(nameof(setTriggerTrue), 0.1f);
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(!isItemSet)
        {
            item = new Item(ItemStructNetVar.Value);
            spriteRenderer.sprite = item.GetSprite();
            if (item.amount > 1)
            {
                textMeshPro.SetText(item.amount.ToString());
            }
            else
            {
                textMeshPro.SetText("");
            }
            isItemSet = true;
        }
    }

    private void setTriggerTrue()
    {
        IsPickupAble = true;
    }
    public void FetchItem()
    {

    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if ( item.amount > 1 )
        {
            textMeshPro.SetText(item.amount.ToString());
        } else
        {
            textMeshPro.SetText("");
        }
        isItemSet = true;
    }

    internal Item getItem()
    {
        return item;
    }

    internal void DestroySelf()
    {
        DestroyObjectServerRpc();
    }
    [ServerRpc(RequireOwnership =false)]
    private void DestroyObjectServerRpc()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blocking"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

}
