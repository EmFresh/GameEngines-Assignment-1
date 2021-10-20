using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

using static Unity.Mathematics.math;
using static Scaler;
public class RandScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        initPlugin();
    }
    public vec3 max = new vec3(1, 1, 1), min = new vec3(1, 1, 1);
    Vector3 current = Vector3.one, lastScale, lastCurrent;
    float count = 0;
    // Update is called once per frame
    void Update()
    {
        if (current == null || transform.localScale == (Vector3)(lastScale * new float3(current)))
        {
            var tmp = Scaler.scaleobject.Invoke(max, min);

            lastScale = transform.localScale;
            lastCurrent = current;
            current = new Vector3(tmp.x, tmp.y, tmp.z);
            count = 0;

            //            print(current);
        }

        //Mathf.Lerp();


        transform.localScale = lastScale * math.lerp(lastCurrent, current, count += .001f);

        if (count >= 1)
        {
            transform.localScale = lastScale * new float3(current);

        }
    }

    private void OnApplicationQuit()
    {
        closePlugin();
    }
}
