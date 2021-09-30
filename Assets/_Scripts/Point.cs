using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Point : MonoBehaviour
{
    public enum PointType
    {
        NONE,
        START,
        CHECK,
        END
    }

    public PointType type = PointType.NONE;

    //Modified version of code taken from https://answers.unity.com/questions/33597/is-it-possible-to-create-a-tag-programmatically.html
    void createTag(string tag)
    {
        var tagAsset = AssetDatabase.LoadAllAssetsAtPath("projectsettings/tagmanager.asset");
        var obj = new SerializedObject(tagAsset[0]);
        var tags = obj.FindProperty("tags");

        for (int a = 0; a < tags.arraySize; ++a)
            if (tags.GetArrayElementAtIndex(a).stringValue == tag) return;

        tags.InsertArrayElementAtIndex(0);
        tags.GetArrayElementAtIndex(0).stringValue = tag;

        obj.ApplyModifiedProperties();
        obj.Update();
    }

    void Start()
    {
        createTag("Point");
        tag = "Point";
    }
}
