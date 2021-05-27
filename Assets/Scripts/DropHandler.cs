using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class DropHandler : MonoBehaviour, IDropHandler
{
    [Inject]
    List<InventoryData> playerInventoryData;

    [Inject]
    List<InventoryData> objectInventoryData;

    [Inject]
    InventoryManager inventoryManager;

    int prevPosition;

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag == null)
            return;

        prevPosition = int.Parse(eventData.pointerDrag.gameObject.transform.parent.name);

        if (prevPosition == int.Parse(name))
        {
            eventData.pointerDrag.gameObject.transform.localPosition = Vector2.zero;
            return;
        }


        /*if (transform.childCount !=0)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            return;
        }*/

        Transform parentTrans = eventData.pointerDrag.transform.parent.transform.parent;

        if (parentTrans != transform.parent) // Moved From One Group To Another
        {
            if (parentTrans.name.Contains("Player")) // Move from player to object
                adjustList(prevPosition - 1, int.Parse(transform.name) - 1, ref playerInventoryData, ref objectInventoryData);
            else // Move from object to player
                adjustList(prevPosition - 1, int.Parse(transform.name) - 1, ref objectInventoryData, ref playerInventoryData);

        }
        else // Moved in Same Group
        {
            if (parentTrans.name.Contains("Player"))
                adjustList(prevPosition-1, int.Parse(transform.name)-1, ref playerInventoryData);
            else
                adjustList(prevPosition - 1, int.Parse(transform.name) - 1, ref objectInventoryData);

        }

        eventData.pointerDrag.transform.SetParent(gameObject.transform);
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        inventoryManager.ForceInventoryUpdate();
    }

    void adjustList(int prevPosition, int currPosition, ref List<InventoryData> _prevList, ref List<InventoryData> _currentList)
    {
        Debug.Log("prevPosition = " + prevPosition + " currPosition = " + currPosition);


        if (_currentList[currPosition].itemObj != null && _prevList[prevPosition].itemObj != null &&
        _currentList[currPosition].itemObj.itemType == _prevList[prevPosition].itemObj.itemType)
        {
            _currentList[currPosition].itemObj = _prevList[prevPosition].itemObj;
            _currentList[currPosition].amount += _prevList[prevPosition].amount;

            _prevList[prevPosition].itemObj = null;
            _prevList[prevPosition].amount = 0;
            return;
        }


       InventoryData tempData = new InventoryData();

        tempData.itemObj = _currentList[currPosition].itemObj;
        tempData.amount = _currentList[currPosition].amount;

        _currentList[currPosition].itemObj = _prevList[prevPosition].itemObj;
        _currentList[currPosition].amount = _prevList[prevPosition].amount;

        _prevList[prevPosition].itemObj = tempData.itemObj;
        _prevList[prevPosition].amount = tempData.amount;

       // _prevList[prevPosition].itemObj = null;
       // _prevList[prevPosition].amount = 0;
    }

    void adjustList(int prevPosition, int currPosition, ref List<InventoryData> _list)
    {
        Debug.Log("prevPosition = " + prevPosition + " currPosition = " + currPosition);


        if (_list[currPosition].itemObj != null && _list[prevPosition].itemObj != null &&
            _list[currPosition].itemObj.itemType == _list[prevPosition].itemObj.itemType)
        {
            _list[currPosition].itemObj = _list[prevPosition].itemObj;
            _list[currPosition].amount += _list[prevPosition].amount;

            _list[prevPosition].itemObj = null;
            _list[prevPosition].amount = 0;

            return;
        }


        InventoryData tempData = new InventoryData();

        tempData.itemObj = _list[currPosition].itemObj;
        tempData.amount = _list[currPosition].amount;

        _list[currPosition].itemObj = _list[prevPosition].itemObj;
        _list[currPosition].amount = _list[prevPosition].amount;

        _list[prevPosition].itemObj = tempData.itemObj;
        _list[prevPosition].amount = tempData.amount;

    }
}
