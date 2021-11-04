using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAction : IMyAction
{
    public GameObject lnk { get; set; }
    public string name { get; set; }

    string lastName = null;
    GameObject prefab;
    MyTransform trans;
    Transform parent;

    public RemoveAction(GameObject lnk, MyTransform trans, GameObject prefab)
    {
        this.lnk = lnk;
        this.name = lnk.name;
        this.prefab = prefab;
        this.trans = new MyTransform(trans);
    }

    public void Invoke()
    {
        parent = lnk.transform.parent;
        lastName = lnk.name;
        GameObject.Destroy(lnk);
    }

    public void Undo()
    {
        var tmp = new CreateAction(prefab, trans, parent);
        tmp.Invoke();
        var list = Invoker.master.FindAll(ctx => ctx.name.Equals(lastName));
        foreach (var obj in list)
        {
            obj.lnk = tmp.lnk;
            obj.name = tmp.name;

            Debug.Log("found object");
        }

    }
}
