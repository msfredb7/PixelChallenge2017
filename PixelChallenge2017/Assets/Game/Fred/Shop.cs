using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public float maxHeight = 410;
    public VerticalLayoutGroup list;
    public GameObject[] itemPrefab;
    public Transform spawnPos;
    public Image bg;

    private float heightPerItem;
    private List<Item> items;
    private List<int> counts;
    private List<int> costs;
    RectTransform rect;

    void Awake()
    {
        gameObject.SetActive(false);
        rect = GetComponent<RectTransform>();
    }

    public void Init(List<Item> items, List<int> counts, List<int> costs)
    {
        this.items = items;
        this.costs = costs;
        this.counts = counts;

        float sizeY = (maxHeight / 5) * items.Count;

        rect.sizeDelta = new Vector2(rect.sizeDelta.x, sizeY);

        for (int i = 0; i < itemPrefab.Length; i++)
        {
            if(i >= items.Count)
            {
                itemPrefab[i].SetActive(false);
                continue;
            }
            if (counts[i] > 0)
                itemPrefab[i].SetActive(true);

            itemPrefab[i].transform.Find("Text").GetComponent<Text>().text = items[i].name + " restant: " + counts[i];
            itemPrefab[i].transform.Find("Cost").GetComponent<Text>().text = "" + costs[i] + "$";
            itemPrefab[i].transform.Find("Image").GetComponent<Image>().sprite = items[i].GetComponentInChildren<SpriteRenderer>().sprite;

        }
        UpdateBuyButtons();
        gameObject.SetActive(true);
    }

    public void OnItemSelect(int index)
    {
        if(spawnPos == null)
        {
            Debug.LogError("no spawn point reference");
            return;
        }
        counts[index]--;
        itemPrefab[index].transform.Find("Text").GetComponent<Text>().text = items[index].name + " restant: " + counts[index];
        GameManager.instance.car.ChangeCash(-costs[index]);
        UpdateBuyButtons();
        Instantiate(items[index], spawnPos.position + new Vector3(Random.Range(-7,7), 0, Random.Range(-5,5)), spawnPos.rotation);
    }

    void UpdateBuyButtons()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (costs[i] > GameManager.instance.car.cash || counts[i] <= 0)
                itemPrefab[i].GetComponentInChildren<Button>().interactable = false;
            else
                itemPrefab[i].GetComponentInChildren<Button>().interactable = true;

        }
    }

    public void Close()
    {
        counts = null;
        items = null;
        costs = null;
        gameObject.SetActive(false);
    }
}
