using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Runtime.InteropServices;
using util;

using static Unity.Mathematics.math;
using static Scaler;
public class RandScale : MonoBehaviour
{

    // [DllImport("EnginesTestDLL")]
    // public static extern vec3 scaleObject(vec3 min, vec3 max);
    // Start is called before the first frame update
    void Awake()
    {
        lastScale = transform.localScale;
        initPlugin();
    }

    public vec3 max = new vec3(1, 1, 1), min = new vec3(1, 1, 1);
    float speed = 25;
    Vector3 current = Vector3.one, lastScale, lastCurrent;
    float count = 0;
    // Update is called once per frame
    void Update()
    {
        if (transform.localScale == (Vector3)(lastScale * new float3(current)))
        {
            var tmp = Scaler.scaleobject.Invoke(min, max);

            lastCurrent = (current == Vector3.negativeInfinity ? Vector3.one : current);
            current = new Vector3(tmp.x, tmp.y, tmp.z);
            //lastScale = transform.localScale;
            count = 0;

            //  print(current);
        }

        //Mathf.Lerp();


        transform.localScale = lastScale * math.lerp(lastCurrent, current, count += Time.deltaTime * speed * .1f);

        if (count >= 1)
            transform.localScale = lastScale * new float3(current);
        //   print("count: " + count);

    }

    private void OnApplicationQuit()
    {
        closePlugin();
    }
}
