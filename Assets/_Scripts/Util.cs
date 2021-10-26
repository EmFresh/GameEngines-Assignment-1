using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace util
{
    [StructLayout(LayoutKind.Sequential)]
    public struct vec3
    {
        public vec3(Vector3 obj)
        {
            x = obj.x; y = obj.y; z = obj.z;
        }
        public vec3(float x, float y, float z)
        { this.x = x; this.y = y; this.z = z; }
        public float x, y, z;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct quat
    {
        public quat(Quaternion obj)
        {
            x = obj.x; y = obj.y; z = obj.z; w = obj.w;
        }
        public quat(float x, float y, float z, float w)
        { this.x = x; this.y = y; this.z = z; this.w = w; }
        public float x, y, z, w;
    }
}