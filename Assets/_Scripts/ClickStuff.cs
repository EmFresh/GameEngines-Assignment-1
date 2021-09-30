using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectSelect : MonoBehaviour, IPointerClickHandler
{
   
    public void OnPointerClick(PointerEventData eventData) =>
          GameControl.selectedObj = GameControl.editMode ? gameObject : GameControl.selectedObj;
    //  print("function entered");


}