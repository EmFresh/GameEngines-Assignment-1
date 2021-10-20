using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectSelect : MonoBehaviour, IPointerClickHandler
{
    public GameObject prefab;

    public void OnPointerClick(PointerEventData eventData) =>
          GameControl.selectedObj = GameControl.editMode ? gameObject : GameControl.selectedObj;


}