using System.Collections;
using System.Collections.Generic;

//using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CreateSpawnButton : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();
    [SerializeField] GameObject button;
    [SerializeField] Transform parent;

    void onclickstuff(GameObject item)
    {

        GameObject obj = Instantiate<GameObject>(item, Vector3.zero, Quaternion.identity, parent);
        //obj.tag = "Editable";

        if (!obj.GetComponent<ObjectSelect>())
            obj.AddComponent<ObjectSelect>();
        if (!obj.GetComponent<RandScale>())
            obj.AddComponent<RandScale>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //  createTag("Editable");

        int a = 0;
        float spacing = 2;
        var trans = button.GetComponent<RectTransform>();
        foreach (var item in items)
        {
            //create button
            var tmpbut = Instantiate(button, Vector3.zero, Quaternion.identity, transform);
            tmpbut.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText(item.name);
            tmpbut.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, a++ * -(trans.offsetMax.y - trans.offsetMin.y + spacing) + spacing);

            //add button behavior
            tmpbut.GetComponent<Button>().onClick.AddListener(delegate { onclickstuff(item); });

            //resize scroll area 
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, -(tmpbut.GetComponent<RectTransform>().offsetMin.y - spacing));
        }
    }

}
