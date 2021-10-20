using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAction : IMyAction
{
    public GameObject lnk { get; private set; }
    GameObject item;
    Transform trans = new GameObject().transform, parent = new GameObject().transform;

    public CreateAction(GameObject inst, Transform trans, Transform parent)
    {
        this.item = inst;
        this.trans = trans;
        //this.trans.position = trans.position;
        //this.trans.rotation = trans.rotation;
        this.parent = parent;
    }

    public void Invoke()
    {
        lnk = GameObject.Instantiate<GameObject>(item, trans.position, trans.rotation, parent);

        if (!lnk.GetComponent<ObjectSelect>())
            lnk.AddComponent<ObjectSelect>();
        // if (!lnk.GetComponent<RandScale>())
        //     lnk.AddComponent<RandScale>();
    }

    public void Undo()
    {
        new RemoveAction(lnk, trans, item).Invoke();
    }
}
