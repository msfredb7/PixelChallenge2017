using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Shop : MonoBehaviour
{
    public float maxHeight = 410;
    public VerticalLayoutGroup list;
    public GameObject[] itemPrefab;
    public Transform spawnPos;
    public Image bg;

    private float heightPerItem;
    private List<ItemAVendre> items;
    RectTransform rect;

    void Awake()
    {
        gameObject.SetActive(false);
        rect = GetComponent<RectTransform>();
    }

    public void Init(List<ItemAVendre> itemsAVendre)
    {
        this.items = itemsAVendre;
        float sizeY = (maxHeight / 5) * items.Count;
        Vector2 targetSize = new Vector2(rect.sizeDelta.x, sizeY);
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, 0);
        rect.DOSizeDelta(targetSize, 0.5f).SetEase(Ease.OutQuad).OnComplete(Open);
        list.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    void Open()
    {

        for (int i = 0; i < itemPrefab.Length; i++)
        {
            if (i >= items.Count)
            {
                itemPrefab[i].SetActive(false);
                continue;
            }
            if (items[i].quantity > 0)
                itemPrefab[i].SetActive(true);

            itemPrefab[i].transform.Find("Text").GetComponent<Text>().text = items[i].item.name + " restant: " + items[i].quantity;
            itemPrefab[i].transform.Find("Cost").GetComponent<Text>().text = "" + items[i].cost + "$";
            itemPrefab[i].transform.Find("Image").GetComponent<Image>().sprite = items[i].item.GetComponentInChildren<SpriteRenderer>().sprite;

        }
        list.gameObject.SetActive(true);
        UpdateBuyButtons();
    }

    public void OnItemSelect(int index)
    {
        if(spawnPos == null)
        {
            Debug.LogError("no spawn point reference");
            return;
        }
        items[index].quantity--;
        itemPrefab[index].transform.Find("Text").GetComponent<Text>().text = items[index].item.name + " restant: " + items[index].quantity;
        GameManager.instance.car.ChangeCash(-items[index].cost);
        UpdateBuyButtons();
        Instantiate(items[index].item, spawnPos.position + new Vector3(Random.Range(-7,7), 0, Random.Range(-5,5)), spawnPos.rotation);
    }

    void UpdateBuyButtons()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].cost > GameManager.instance.car.cash || items[i].quantity <= 0)
                itemPrefab[i].GetComponentInChildren<Button>().interactable = false;
            else
                itemPrefab[i].GetComponentInChildren<Button>().interactable = true;

        }
    }

    public void Close()
    {
        items = null;
        gameObject.SetActive(false);
    }
}
