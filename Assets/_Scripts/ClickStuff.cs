using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 public class ClickStuff : MonoBehaviour, IPointerClickHandler
    {
        // void onClick(GameObject obj)
        // {
        //     if (GameControl.editMode)
        //         GameControl.selectedObj = obj;
        // }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameControl.editMode)
            {
                GameControl.selectedObj = gameObject;

                print("function entered");
            }
        }
    }