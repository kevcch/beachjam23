using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass
{
    public GameObject objectViewPrefab;
    public ItemType objectType;

    public ItemClass(GameObject _objectViewPrefab, ItemType _objectType) {
        objectViewPrefab = _objectViewPrefab;
        objectType = _objectType;
    }

    public ItemClass(ItemType _objectType) {
        objectType = _objectType;
        switch(_objectType) {
            case ItemType.Cup:
                objectViewPrefab = Resources.Load("CupView") as GameObject;
                break;
            case ItemType.Cone:
                objectViewPrefab = Resources.Load("ConeView") as GameObject;
                break;
            case ItemType.Strawberry:
                objectViewPrefab = Resources.Load("StrawberryView") as GameObject;
                break;
            case ItemType.Chocolate:
                objectViewPrefab = Resources.Load("ChocolateView") as GameObject;
                break;
            case ItemType.Vanilla:
                objectViewPrefab = Resources.Load("VanillaView") as GameObject;
                break;
            default:
                break;
        }
    }

}
public enum ItemType {Cup, Cone, Strawberry, Chocolate, Vanilla}