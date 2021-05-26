using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    [Inject]
    List<InventoryData> playerInventoryData;

    [Inject]
    List<InventoryData> objectInventoryData;

    public GameObject playerInventoryObj;
    public GameObject objectInventoryObj;


    private void Awake()
    {

        playerInventoryObj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnSortClicked);
        objectInventoryObj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnSortClicked);

        int pos = 1;
        foreach (InventoryData data in playerInventoryData)
        {
            if(data.itemObj!=null)
                CreateInventoryObject(playerInventoryObj, pos, data.itemObj.itemPrefab, data.amount);
            pos++;
        }

        pos = 1;
        foreach (InventoryData data in objectInventoryData)
        {
            if (data.itemObj != null)
                CreateInventoryObject(objectInventoryObj, pos, data.itemObj.itemPrefab, data.amount);
            pos++;
        }
    }


    public void CreateInventoryObject(GameObject _parentObj, int _position, GameObject _obj, int _amount)
    {

        Transform transformObj = _parentObj.transform.Find(_position.ToString());
        GameObject createdObj = GameObject.Instantiate(_obj, transformObj);
        createdObj.transform.localPosition = Vector2.zero;
        createdObj.GetComponent<Image>().raycastTarget = true;
        createdObj.AddComponent<CanvasGroup>();
        createdObj.AddComponent<DragHandler>();
        createdObj.AddComponent<Canvas>().overrideSorting = true;
        createdObj.GetComponent<Canvas>().sortingOrder = 1;
        createdObj.AddComponent<GraphicRaycaster>();


        GameObject textObj = new GameObject("Amount", typeof(TextMeshProUGUI));
        textObj.transform.SetParent(createdObj.transform);
        textObj.transform.localPosition = new Vector2(75, -75);
        textObj.GetComponent<RectTransform>().sizeDelta = 40 * Vector2.one;
        TextMeshProUGUI createdText = textObj.GetComponent<TextMeshProUGUI>();
        createdText.text = _amount.ToString();
        createdText.alignment = TextAlignmentOptions.Center;
        createdText.raycastTarget = false;
        createdText.enableAutoSizing = true;
    }


    public void UpdateInventoryCount(GameObject obj, ref List<InventoryData> list)
    {
        int pos = 1;
        foreach (InventoryData data in list)
        {
            GameObject imgObj = obj.transform.Find(pos.ToString()).GetChild(0).gameObject;

            if (imgObj.transform.GetChild(0) != null)
                imgObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.amount.ToString();
            pos++;
        }
    }

    public void DestroyInventoryObject(GameObject _obj)
    {
        _obj.SetActive(false);
        Destroy(_obj);
    }

    public void ForceInventoryUpdate()
    {

        foreach(Transform child in playerInventoryObj.transform)
        {
            foreach (Transform insideTrans in child.transform)
            {
                Destroy(insideTrans.gameObject);
            }
        }

        foreach (Transform child in objectInventoryObj.transform)
        {
            foreach (Transform insideTrans in child.transform)
            {
                Destroy(insideTrans.gameObject);
            }
        }

        int pos = 1;
        foreach (InventoryData data in playerInventoryData)
        {
            if (data.itemObj != null)
                CreateInventoryObject(playerInventoryObj, pos, data.itemObj.itemPrefab, data.amount);
            pos++;
        }

        pos = 1;
        foreach (InventoryData data in objectInventoryData)
        {
            if (data.itemObj != null)
                CreateInventoryObject(objectInventoryObj, pos, data.itemObj.itemPrefab, data.amount);
            pos++;
        }

    }

    public void OnSortClicked()
    {

        if (EventSystem.current.currentSelectedGameObject.transform.parent.name.Contains("Player"))
        {
            for (int i = 0; i < playerInventoryData.Count; i++)
            {
                for (int j = 0; j < playerInventoryData.Count; j++)
                {
                    if (j == i)
                        continue;

                    if (playerInventoryData[i].itemObj == playerInventoryData[j].itemObj)
                    {
                        playerInventoryData[i].amount += playerInventoryData[j].amount;
                        DestroyInventoryObject(playerInventoryObj.transform.Find((j + 1).ToString()).GetChild(0).gameObject);
                        playerInventoryData.RemoveAt(j);
                    }
                }
            }

            UpdateInventoryCount(playerInventoryObj, ref playerInventoryData);
        }
        else
        {
            for (int i = 0; i < objectInventoryData.Count; i++)
            {
                for (int j = 0; j < objectInventoryData.Count; j++)
                {
                    if (j == i)
                        continue;

                    if (objectInventoryData[i].itemObj == objectInventoryData[j].itemObj)
                    {
                        objectInventoryData[i].amount += objectInventoryData[j].amount;
                        DestroyInventoryObject(objectInventoryObj.transform.Find((j + 1).ToString()).GetChild(0).gameObject);
                        objectInventoryData.RemoveAt(j);
                    }
                }
            }

            UpdateInventoryCount(objectInventoryObj, ref objectInventoryData);
        }

    }
}
