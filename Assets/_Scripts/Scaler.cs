using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using util;

public class Scaler : MonoBehaviour
{



#if UNITY_EDITOR
    const string DLL = "/Plugins/EnginesTestDLL.dll";
#else
    const string DLL = "/Plugins/x86_64/EnginesTestDLL.dll";
#endif

    static IntPtr _pluginHandle = IntPtr.Zero;

    #region Plugin Handling
    public static void initPlugin()
    {
        if (_pluginHandle != IntPtr.Zero) return;

        if ((_pluginHandle = ManualPluginImporter.OpenLibrary(Application.dataPath + DLL)) == IntPtr.Zero) return; //No DLL found

        //imported delegates 
        //    getlastnetworkerror = ManualPluginImporter.GetDelegate<getLastNetworkErrorDelegate>(_pluginHandle, "getLastNetworkError");

        scaleObjectUniform = ManualPluginImporter.GetDelegate<scaleObjectUniformDelegate>(_pluginHandle, "scaleObjectUniform");
        scaleobject = ManualPluginImporter.GetDelegate<scaleObjectDelegate>(_pluginHandle, "scaleObject");
    }

    public static void closePlugin()
    {
        if (_pluginHandle != IntPtr.Zero)
            ManualPluginImporter.CloseLibrary(_pluginHandle);
    }
    #endregion

    private void OnApplicationQuit()
    {
        closePlugin();
    }
    // ///<summary>
    // ///gets the last error that happened
    // ///</summary>
    // private static getLastNetworkErrorDelegate getlastnetworkerror;
    // private delegate IntPtr getLastNetworkErrorDelegate(int idk);

    public static scaleObjectUniformDelegate scaleObjectUniform;
    public delegate void scaleObjectUniformDelegate(ref vec3 obj, float uni);

    public static scaleObjectDelegate scaleobject;
    public delegate vec3 scaleObjectDelegate(vec3 min, vec3 max);

}
