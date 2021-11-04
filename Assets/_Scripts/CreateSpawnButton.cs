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
    public static List<GameObject> statItems
    { get; private set; }
    [SerializeField] GameObject button;

    [SerializeField] Transform parent;



    void onclickstuff(GameObject item)
    {

        CreateAction create = new CreateAction(item, new MyTransform(), parent);
        Invoker.addAction(create);
    }



    // Start is called before the first frame update
    void Start()
    {

        statItems = items;
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
