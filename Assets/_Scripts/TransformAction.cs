using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTransform
{

    public MyTransform(Transform trans)
    {
        pos = trans.position;
        scale = trans.localScale;
        rot = trans.rotation;
    }
    public MyTransform(MyTransform trans)
    {
        pos = trans.pos;
        scale = trans.scale;
        rot = trans.rot;
    }
    public MyTransform()
    {
        // pos = Vector3.zero;
        // rot = Quaternion.identity;
        // scale = Vector3.one;
    }
    public Vector3 pos = Vector3.zero, scale = Vector3.one;
    public Quaternion rot = Quaternion.identity;
}

public class TransformAction : IMyAction
{
    public GameObject lnk { get; set; }
    public string name { get; set; }

    MyTransform lastTrans, trans;


    public TransformAction(GameObject lnk, MyTransform trans)
    {
        this.lnk = lnk;
        this.name = lnk.name;
        this.trans = trans;
        //rot = trans.rot;
        //pos = trans.pos;
    }


    public void Invoke()
    {
        lastTrans = new MyTransform(lnk.transform);
        //   lastPos = lnk.transform.position;
        //   lastRot = lnk.transform.rotation;

        lnk.transform.position = trans.pos;
        lnk.transform.rotation = trans.rot;

    }
    public void Undo()
    {
        lnk.transform.position = lastTrans.pos;
        lnk.transform.rotation = lastTrans.rot;
    }
}
