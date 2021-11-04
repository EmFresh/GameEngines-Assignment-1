using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAction : IMyAction
{
    public GameObject lnk { get; set; }
    public string name { get; set; }

    GameObject item;
    MyTransform trans;
    Transform parent;

    public CreateAction(GameObject inst, MyTransform trans, Transform parent)
    {
        this.item = inst;
        this.trans = new MyTransform(trans);
        //this.trans.position = trans.position;
        //this.trans.rotation = trans.rotation;
        this.parent = parent;
        //lnk = null;
    }

    public void Invoke()
    {
        lnk = GameObject.Instantiate<GameObject>(item, trans.pos, trans.rot, parent);
        name = lnk.name;

        if (!lnk.GetComponent<ObjectSelect>())
            lnk.AddComponent<ObjectSelect>().prefab = item;


        if (!lnk.GetComponent<RandScale>())
            lnk.AddComponent<RandScale>();
    }

    public void Undo()
    {
        new RemoveAction(lnk, trans, item).Invoke();
    }
}
