using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class PanelControl : MonoBehaviour
{
    public GameObject title, pos, rot, randMax, randMin;

    GameObject current;
    MyTransform lastTransform;
    bool submitted = false;
    public void removeObject()
    {
        var fab = CreateSpawnButton.statItems.Find(ctx => current.GetComponent<ObjectSelect>().prefab.name.Contains(ctx.name));
        Invoker.addAction(new RemoveAction(current, new MyTransform(current.transform), fab));

        lastTransform = new MyTransform(); current = null;
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
            lastTransform = new MyTransform(current.transform);

            pos.transform.GetChild(0).GetComponent<TMP_InputField>().onSubmit.RemoveAllListeners();
            pos.transform.GetChild(1).GetComponent<TMP_InputField>().onSubmit.RemoveAllListeners();
            pos.transform.GetChild(2).GetComponent<TMP_InputField>().onSubmit.RemoveAllListeners();
            rot.transform.GetChild(0).GetComponent<TMP_InputField>().onSubmit.RemoveAllListeners();
            rot.transform.GetChild(1).GetComponent<TMP_InputField>().onSubmit.RemoveAllListeners();
            rot.transform.GetChild(2).GetComponent<TMP_InputField>().onSubmit.RemoveAllListeners();
            pos.transform.GetChild(0).GetComponent<TMP_InputField>().onSubmit.AddListener(ctx => { submitted = true; });
            pos.transform.GetChild(1).GetComponent<TMP_InputField>().onSubmit.AddListener(ctx => { submitted = true; });
            pos.transform.GetChild(2).GetComponent<TMP_InputField>().onSubmit.AddListener(ctx => { submitted = true; });
            rot.transform.GetChild(0).GetComponent<TMP_InputField>().onSubmit.AddListener(ctx => { submitted = true; });
            rot.transform.GetChild(1).GetComponent<TMP_InputField>().onSubmit.AddListener(ctx => { submitted = true; });
            rot.transform.GetChild(2).GetComponent<TMP_InputField>().onSubmit.AddListener(ctx => { submitted = true; });

            pos.transform.GetChild(0).GetComponent<TMP_InputField>().onSelect.RemoveAllListeners();
            pos.transform.GetChild(1).GetComponent<TMP_InputField>().onSelect.RemoveAllListeners();
            pos.transform.GetChild(2).GetComponent<TMP_InputField>().onSelect.RemoveAllListeners();
            rot.transform.GetChild(0).GetComponent<TMP_InputField>().onSelect.RemoveAllListeners();
            rot.transform.GetChild(1).GetComponent<TMP_InputField>().onSelect.RemoveAllListeners();
            rot.transform.GetChild(2).GetComponent<TMP_InputField>().onSelect.RemoveAllListeners();
            pos.transform.GetChild(0).GetComponent<TMP_InputField>().onSelect.AddListener(ctx => EditMovement.controls.EditMode.Disable());
            pos.transform.GetChild(1).GetComponent<TMP_InputField>().onSelect.AddListener(ctx => EditMovement.controls.EditMode.Disable());
            pos.transform.GetChild(2).GetComponent<TMP_InputField>().onSelect.AddListener(ctx => EditMovement.controls.EditMode.Disable());
            rot.transform.GetChild(0).GetComponent<TMP_InputField>().onSelect.AddListener(ctx => EditMovement.controls.EditMode.Disable());
            rot.transform.GetChild(1).GetComponent<TMP_InputField>().onSelect.AddListener(ctx => EditMovement.controls.EditMode.Disable());
            rot.transform.GetChild(2).GetComponent<TMP_InputField>().onSelect.AddListener(ctx => EditMovement.controls.EditMode.Disable());


            pos.transform.GetChild(0).GetComponent<TMP_InputField>().onDeselect.RemoveAllListeners();
            pos.transform.GetChild(1).GetComponent<TMP_InputField>().onDeselect.RemoveAllListeners();
            pos.transform.GetChild(2).GetComponent<TMP_InputField>().onDeselect.RemoveAllListeners();
            rot.transform.GetChild(0).GetComponent<TMP_InputField>().onDeselect.RemoveAllListeners();
            rot.transform.GetChild(1).GetComponent<TMP_InputField>().onDeselect.RemoveAllListeners();
            rot.transform.GetChild(2).GetComponent<TMP_InputField>().onDeselect.RemoveAllListeners();
            pos.transform.GetChild(0).GetComponent<TMP_InputField>().onDeselect.AddListener(ctx => EditMovement.controls.EditMode.Enable());
            pos.transform.GetChild(1).GetComponent<TMP_InputField>().onDeselect.AddListener(ctx => EditMovement.controls.EditMode.Enable());
            pos.transform.GetChild(2).GetComponent<TMP_InputField>().onDeselect.AddListener(ctx => EditMovement.controls.EditMode.Enable());
            rot.transform.GetChild(0).GetComponent<TMP_InputField>().onDeselect.AddListener(ctx => EditMovement.controls.EditMode.Enable());
            rot.transform.GetChild(1).GetComponent<TMP_InputField>().onDeselect.AddListener(ctx => EditMovement.controls.EditMode.Enable());
            rot.transform.GetChild(2).GetComponent<TMP_InputField>().onDeselect.AddListener(ctx => EditMovement.controls.EditMode.Enable());

            if (current.GetComponent<RandScale>())
            {
                randMin.transform.GetChild(0).GetComponent<TMP_InputField>().text = current.GetComponent<RandScale>().min.x.ToString();
                randMin.transform.GetChild(1).GetComponent<TMP_InputField>().text = current.GetComponent<RandScale>().min.y.ToString();
                randMin.transform.GetChild(2).GetComponent<TMP_InputField>().text = current.GetComponent<RandScale>().min.z.ToString();

                randMax.transform.GetChild(0).GetComponent<TMP_InputField>().text = current.GetComponent<RandScale>().max.x.ToString();
                randMax.transform.GetChild(1).GetComponent<TMP_InputField>().text = current.GetComponent<RandScale>().max.y.ToString();
                randMax.transform.GetChild(2).GetComponent<TMP_InputField>().text = current.GetComponent<RandScale>().max.z.ToString();
            }
        }

        if (!current) return;
        string val;
        float
        posx = float.Parse((val = pos.transform.GetChild(0).GetComponent<TMP_InputField>().text) == "" || !submitted ? current.transform.position.x.ToString() : val),
        posy = float.Parse((val = pos.transform.GetChild(1).GetComponent<TMP_InputField>().text) == "" || !submitted ? current.transform.position.y.ToString() : val),
        posz = float.Parse((val = pos.transform.GetChild(2).GetComponent<TMP_InputField>().text) == "" || !submitted ? current.transform.position.z.ToString() : val),
        rotx = float.Parse((val = rot.transform.GetChild(0).GetComponent<TMP_InputField>().text) == "" || !submitted ? current.transform.rotation.x.ToString() : val),
        roty = float.Parse((val = rot.transform.GetChild(1).GetComponent<TMP_InputField>().text) == "" || !submitted ? current.transform.rotation.y.ToString() : val),
        rotz = float.Parse((val = rot.transform.GetChild(2).GetComponent<TMP_InputField>().text) == "" || !submitted ? current.transform.rotation.z.ToString() : val);



        //Set Transformations
        MyTransform trans = new MyTransform();

        trans.pos = new Vector3(posx, posy, posz);
        trans.rot = Quaternion.Euler(rotx, roty, rotz);


        if (lastTransform.pos != current.transform.position ||
        lastTransform.rot != current.transform.rotation)
        {
            pos.transform.GetChild(0).GetComponent<TMP_InputField>().text = current.transform.position.x.ToString();
            pos.transform.GetChild(1).GetComponent<TMP_InputField>().text = current.transform.position.y.ToString();
            pos.transform.GetChild(2).GetComponent<TMP_InputField>().text = current.transform.position.z.ToString();

            rot.transform.GetChild(0).GetComponent<TMP_InputField>().text = current.transform.rotation.eulerAngles.x.ToString();
            rot.transform.GetChild(1).GetComponent<TMP_InputField>().text = current.transform.rotation.eulerAngles.y.ToString();
            rot.transform.GetChild(2).GetComponent<TMP_InputField>().text = current.transform.rotation.eulerAngles.z.ToString();
        }

        if (submitted)
            if (trans.pos != current.transform.position ||
                trans.rot != current.transform.rotation)
            {
                Invoker.addAction(new TransformAction(current, trans));
                lastTransform = trans;
                submitted = false;
            }
        //Destroy(trans);

        if (current.GetComponent<RandScale>())
        {
            float
            randMinx = float.Parse((val = randMin.transform.GetChild(0).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
            randMiny = float.Parse((val = randMin.transform.GetChild(1).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
            randMinz = float.Parse((val = randMin.transform.GetChild(2).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
            randMaxx = float.Parse((val = randMax.transform.GetChild(0).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
            randMaxy = float.Parse((val = randMax.transform.GetChild(1).GetComponent<TMP_InputField>().text) == "" ? "0" : val),
            randMaxz = float.Parse((val = randMax.transform.GetChild(2).GetComponent<TMP_InputField>().text) == "" ? "0" : val);

            current.GetComponent<RandScale>().max = new Scaler.vec3(randMaxx, randMaxy, randMaxz);
            current.GetComponent<RandScale>().min = new Scaler.vec3(randMinx, randMiny, randMinz);
        }
        // print("Position: " + current.transform.position);
        // print("Rotation: " + current.transform.rotation.eulerAngles);
    }

    class Trans : Transform
    {
        public Trans(Vector3 pos, Quaternion rot)
        {
            this.position = pos;
            this.rotation = rot;
        }
    }
}
