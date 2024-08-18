using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldObjectParent
{
    public Transform GetWorldObjectFollowTransform();

    public void SetWorldObject(WorldObject kitchenObject);

    public WorldObject GetWorldObject();

    public void ClearWorldObject();

    public bool HasWorldObject();
}
