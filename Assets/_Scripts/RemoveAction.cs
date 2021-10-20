using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAction : IMyAction
{
    public GameObject lnk { get; private set; }
    GameObject prefab;
    Transform trans = new GameObject().transform, parent = new GameObject().transform;

    public RemoveAction(GameObject lnk, Transform trans, GameObject prefab)
    {
        this.lnk = lnk;
        this.prefab = prefab;
        this.trans = trans;
    }

    public void Invoke()
    {
        parent = lnk.transform.parent;
        GameObject.Destroy(lnk);
    }

    public void Undo()
    {
        new CreateAction(prefab, trans, parent).Invoke();
    }
}
