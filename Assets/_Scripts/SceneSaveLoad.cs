using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SaveLoad;
public class SceneSaveLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()

    {
        initPlugin();
    }

    public void saveLevel()
    {
        var objs = transform.GetComponentsInChildren<ObjectSelect>();
        List<MyGameObject> myobj = new List<MyGameObject>();

        foreach (var obj in objs)
            myobj.Add(new MyGameObject(obj.gameObject));

        Save("MyFile.sav", myobj.ToArray());
        print("save Complete");
    }

    public void loadLevel() { }

    private void OnApplicationQuit()
    {
        closePlugin();
    }
}
