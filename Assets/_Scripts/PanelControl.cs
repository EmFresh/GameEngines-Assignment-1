using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PanelControl : MonoBehaviour
{
    public GameObject title, pos, rot;

    GameObject current;

    public void removeObject()
    {
        Destroy(current);
        current = null;
    }

    // Update is called once per frame
    void Update()
    {
        //set defaults
        if (current != GameControl.selectedObj)
        {
            current = GameControl.selectedObj;
            if (!current) return;

            //title change
            var tit = title.GetComponent<TMP_Text>().text;
            title.GetComponent<TMP_Text>().text = tit.Substring(0, tit.IndexOf(' ') + 1) + current.name;

            //transform init
            var tmp = GameControl.selectedObj.transform;
            pos.transform.GetChild(0).GetComponent<TMP_InputField>().text = tmp.position.x.ToString();
            pos.transform.GetChild(1).GetComponent<TMP_InputField>().text = tmp.position.y.ToString();
            pos.transform.GetChild(2).GetComponent<TMP_InputField>().text = tmp.position.z.ToString();

            rot.transform.GetChild(0).GetComponent<TMP_InputField>().text = tmp.rotation.eulerAngles.x.ToString();
            rot.transform.GetChild(1).GetComponent<TMP_InputField>().text = tmp.rotation.eulerAngles.y.ToString();
            rot.transform.GetChild(2).GetComponent<TMP_InputField>().text = tmp.rotation.eulerAngles.z.ToString();
        }

        if (!current) return;
        string val;
        float
        posx = float.Parse((val = pos.transform.GetChild(0).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
        posy = float.Parse((val = pos.transform.GetChild(1).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
        posz = float.Parse((val = pos.transform.GetChild(2).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
        rotx = float.Parse((val = rot.transform.GetChild(0).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
        roty = float.Parse((val = rot.transform.GetChild(1).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
        rotz = float.Parse((val = rot.transform.GetChild(2).GetComponent<TMP_InputField>().text) == "" ? "0" : val);

        current.transform.position = new Vector3(posx, posy, posz);
        current.transform.rotation = Quaternion.Euler(new Vector3(rotx, roty, rotz));
        // print("Position: " + current.transform.position);
        // print("Rotation: " + current.transform.rotation.eulerAngles);
    }
}
