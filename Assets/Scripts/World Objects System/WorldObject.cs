using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    [SerializeField] private WorldObjectSO worldObjectSO;

    private IWorldObjectParent parent;

    public WorldObjectSO GetWorldObjectSO()
    {
        return worldObjectSO;
    }

    public void SetWorldObjectParent(IWorldObjectParent worldObjectParent)
    {
        // Set the parent of this world object
        if(this.parent != null)
        {
            this.parent.ClearWorldObject();
        }

        this.parent = worldObjectParent;


        // Set the world object of the parent
        if(worldObjectParent.HasWorldObject())
        {
            Debug.LogError("WorldObjectParent already has a world object");
        }

        worldObjectParent.SetWorldObject(this);

        // Set the position of the world object
        transform.parent = worldObjectParent.GetWorldObjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }

    public void DestroySelf()
    {
        parent.ClearWorldObject();
        Destroy(gameObject);
    }

    public bool TryGetMobileWO(out MobileWO mobileWO)
    {
        if(this is MobileWO)
        {
            mobileWO = this as MobileWO;
            return true;
        }
        else
        {
            mobileWO = null;
            return false;
        }
    }

    public static WorldObject SpawnWorldObject(WorldObjectSO worldObjectSO, IWorldObjectParent worldObjectParent)
    {
        Transform worldObjectTransform = Instantiate(worldObjectSO.prefab);
        WorldObject worldObject = worldObjectTransform.GetComponent<WorldObject>();
        worldObject.SetWorldObjectParent(worldObjectParent);
        return worldObject;
    }
}
