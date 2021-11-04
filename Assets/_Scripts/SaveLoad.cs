using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using util;

public class SaveLoad
{
    [StructLayout(LayoutKind.Sequential)]
    public class MyGameObject
    {

        public MyGameObject(GameObject obj)
        {
            pos = new vec3(obj.transform.position);
            rot = new quat(obj.transform.rotation);
            name = obj.name;
        }

        vec3 pos = new vec3(0, 0, 0);
        quat rot = new quat(0, 0, 0, 1);
        [MarshalAs(UnmanagedType.LPStr)] string name = "";
    }

#if UNITY_EDITOR
    const string DLL = "/Plugins/SaveLoadDLL.dll";
#else
    const string DLL = "/Plugins/x86_64/SaveLoadDLL.dll";
#endif

    static IntPtr _pluginHandle = IntPtr.Zero;

    #region Plugin Handling
    public static void initPlugin()
    {
        if (_pluginHandle != IntPtr.Zero) return;

        if ((_pluginHandle = ManualPluginImporter.OpenLibrary(Application.dataPath + DLL)) == IntPtr.Zero) return; //No DLL found

        //imported delegates 
        //    getlastnetworkerror = ManualPluginImporter.GetDelegate<getLastNetworkErrorDelegate>(_pluginHandle, "getLastNetworkError");

        save = ManualPluginImporter.GetDelegate<saveDelegate>(_pluginHandle, "save");
        load = ManualPluginImporter.GetDelegate<loadDelegate>(_pluginHandle, "load");
    }

    public static void closePlugin()
    {
        if (_pluginHandle != IntPtr.Zero)
            ManualPluginImporter.CloseLibrary(_pluginHandle);
    }
    #endregion


    private static saveDelegate save;
    private delegate void saveDelegate([MarshalAs(UnmanagedType.LPStr)] string file, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] MyGameObject[] objects, int size);

    public static void Save(string file, MyGameObject[] objects) => save.Invoke(Application.dataPath + "/" + file, objects, objects.Length);

    private static loadDelegate load;
    private delegate IntPtr loadDelegate([MarshalAs(UnmanagedType.LPStr)] string file, ref int size);

    public static MyGameObject[] Load(string file)
    {
        int size = 0;
        IntPtr data = load.Invoke(file, ref size);

        MyGameObject[] objects = new MyGameObject[size];

        for (int a = 0; a < size; ++a)
            Marshal.PtrToStructure(data + Marshal.SizeOf<MyGameObject>() * a, objects[a]);

        return objects;
    }
}
