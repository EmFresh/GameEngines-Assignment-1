using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAction : IMyAction
{
    public GameObject lnk { get; private set; }
    Vector3 lastPos, pos;
    Quaternion lastRot, rot;

    public TransformAction(GameObject lnk, Transform trans)
    {
        this.lnk = lnk;
        rot = trans.rotation;
        pos = trans.position;
    }
    public void Invoke()
    {
        lastPos = lnk.transform.position;
        lastRot = lnk.transform.rotation;

        lnk.transform.position = pos;
        lnk.transform.rotation = rot;

    }
    public void Undo()
    {
        lnk.transform.position = lastPos;
        lnk.transform.rotation = lastRot;
    }
}
