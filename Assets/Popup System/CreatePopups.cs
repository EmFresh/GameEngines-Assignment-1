using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatePopups : MonoBehaviour
{
    public enum OriginLocal
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT,
    };

    public OriginLocal origin = OriginLocal.TOP;
    public GameObject popupPrefab;
    public float delay = 1.5f, fadeoutTime = 0.5f;
    private GameObject currPopup, inst;
    private List<GameObject> popups = new List<GameObject>();
    private static IList<string> popupMsgs = new List<string>();

    void Start()
    {
        //   changeOrigin(origin, gameObject);
        //changePivot(origin, gameObject);
    }

    public static void SendPopup(object msg, bool dbg = true)
    {
        popupMsgs.Add(msg.ToString());

#if UNITY_EDITOR            
        if (dbg)
            Debug.Log(msg);
#endif

    }

    void changeAnchor(OriginLocal orig, GameObject obj)
    {
        print(orig);
        switch (origin)
        {
            case OriginLocal.TOP:
                obj.GetComponent<RectTransform>().anchorMin =
                obj.GetComponent<RectTransform>().anchorMax =
                new Vector2(.5f, 1);
                break;
            case OriginLocal.BOTTOM:
                obj.GetComponent<RectTransform>().anchorMax =
                obj.GetComponent<RectTransform>().anchorMin =
                new Vector2(.5f, 0);
                break;
            case OriginLocal.LEFT:
                obj.GetComponent<RectTransform>().anchorMax =
                obj.GetComponent<RectTransform>().anchorMin =
                new Vector2(0, .5f);
                break;
            case OriginLocal.RIGHT:
                obj.GetComponent<RectTransform>().anchorMax =
                obj.GetComponent<RectTransform>().anchorMin =
                new Vector2(1, .5f);
                break;
            default:

                obj.GetComponent<RectTransform>().anchorMax =
                obj.GetComponent<RectTransform>().anchorMin =
                new Vector2(.5f, 1);
                break;
        }
    }
    void changePivot(OriginLocal orig, GameObject obj)
    {
        switch (origin)
        {
            case OriginLocal.TOP:
                obj.GetComponent<RectTransform>().pivot =
                new Vector2(.5f, 1);
                break;
            case OriginLocal.BOTTOM:
                obj.GetComponent<RectTransform>().pivot =
                new Vector2(.5f, 0);
                break;
            case OriginLocal.LEFT:
                obj.GetComponent<RectTransform>().pivot =
                new Vector2(0, .5f);
                break;
            case OriginLocal.RIGHT:
                obj.GetComponent<RectTransform>().pivot =
                new Vector2(1, .5f);
                break;
            default:

                obj.GetComponent<RectTransform>().pivot =
                new Vector2(.5f, 1);
                break;
        }

    }


    // Update is called once per frame
    void Update()
    {
        while (popupMsgs.Count > 0)
        {
            inst = Instantiate(popupPrefab, inst == null ? transform : currPopup.transform, false);
            inst.GetComponent<FadeInOut>().delay = delay;
            inst.GetComponent<FadeInOut>().fadeoutTime = fadeoutTime;

            inst.GetComponentInChildren<TextMeshProUGUI>().text = popupMsgs[0];

            //  if (inst.transform.parent != transform)
            changeAnchor((OriginLocal)((int)origin + 2 % 4), inst);
            //else
            //    changeAnchor((origin), inst);
            changePivot(origin, inst);

            // inst.GetComponent<RectTransform>().anchorMax =
            // inst.GetComponent<RectTransform>().anchorMin =
            // new Vector2(.5f, 0);

            inst.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            inst.GetComponent<RectTransform>().localScale = Vector3.one;
            currPopup = inst;
            popups.Add(inst);
            popupMsgs.RemoveAt(0);
        }

        for (int index = 0; index < popups.Count; ++index)
        {
            if (popups[index] == null)
            {
                popups.RemoveAt(index--);
                continue;
            }

            if (popups[index].GetComponent<CanvasGroup>().alpha == 0)
            {
                if (popups[index].transform.childCount > 1)
                {
                    var child = popups[index].transform.GetChild(1);
                    popups[index].transform.GetChild(1).SetParent(popups[index].transform.parent, false);
                    if (child.parent == transform)
                        changeAnchor(origin, child.gameObject);
                    //child.GetComponent<RectTransform>().anchorMax =
                    //child.GetComponent<RectTransform>().anchorMin =
                    //new Vector2(.5f, 1);

                    child.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                Destroy(popups[index]);
            }
            else
            {
                popups[index].GetComponent<RectTransform>().
                SetSizeWithCurrentAnchors((int)origin % 2 == 0 ?
                RectTransform.Axis.Vertical :
                RectTransform.Axis.Horizontal,
                Mathf.Lerp(0, (int)origin % 2 == 0 ?
                popupPrefab.GetComponent<RectTransform>().sizeDelta.y :
                popupPrefab.GetComponent<RectTransform>().sizeDelta.x,
                popups[index].GetComponent<CanvasGroup>().alpha));
            }
        }
    }

}