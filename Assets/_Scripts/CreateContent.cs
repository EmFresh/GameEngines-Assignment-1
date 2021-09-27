using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CreateContent : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();
    [SerializeField] GameObject button;

    void onclickstuff(GameObject item)
    {

        Instantiate(item, Vector3.zero, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        int a = 0;
        var trans = button.GetComponent<RectTransform>();
        foreach (var item in items)
        {
            //create button
            var tmpbut = Instantiate(button, Vector3.zero, Quaternion.identity, transform);
            tmpbut.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, a++ * -(trans.offsetMax.y - trans.offsetMin.y + 2));

            //add button behavior
            tmpbut.GetComponent<Button>().onClick.AddListener(delegate { onclickstuff(item); });
            tmpbut.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText(item.name);

            //resize scroll area 
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, -(tmpbut.GetComponent<RectTransform>().offsetMin.y - 2));
        }
    }

}
