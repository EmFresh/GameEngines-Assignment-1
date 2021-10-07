using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Scaler;
public class RandScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        initPlugin();
    }
    public vec3 max = new vec3(1, 1, 1), min = new vec3(1, 1, 1);
    Vector3 current;

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale == current)
        {
            var tmp = Scaler.scaleobject.Invoke(max, min);
            current = new Vector3(tmp.x, tmp.y, tmp.z);
        }

        transform.localScale += (current - transform.localScale).normalized * Time.deltaTime;

        if ((current - transform.localScale).magnitude < .1)
            transform.localScale = current;
    }
}
