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

}
public enum ItemType {Cup, Cone, Strawberry, Chocolate, Vanilla}