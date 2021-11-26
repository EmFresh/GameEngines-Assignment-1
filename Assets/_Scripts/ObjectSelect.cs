using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectSelect : MonoBehaviour, IPointerClickHandler
{
    public GameObject prefab;
    private void Awake()
    {
        if (!prefab)
            prefab = gameObject;
    }
    public void OnPointerClick(PointerEventData eventData) =>
          GameControl.selectedObj = GameControl.editMode ? gameObject : GameControl.selectedObj;


}