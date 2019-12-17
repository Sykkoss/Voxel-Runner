using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum Tabs
{
    Skins,
    PowerUps,
    Equip
}

public class ShopManager : MonoBehaviour
{
    public List<GameObject> skins;
    public List<GameObject> powerUps;
    public List<GameObject> equip;
    public Tabs focusOn;

    private Vector3 skinPosition;
    private Vector3 skinScale;

    public GameObject defaultItemContainer;
    private GameObject placeholder;

    // Start is called before the first frame update
    void Start()
    {
        skins = new List<GameObject>(GetAtPath<GameObject>("Prefabs/Player/"));
        powerUps = new List<GameObject>(GetAtPath<GameObject>("Prefabs/Power Ups/"));
        skinPosition = new Vector3(0, -150);
        skinScale = new Vector3(150, 150, -150);
        placeholder = GameObject.Find("Placeholder");
        PlayerPrefs.SetInt(skins[1].name, 1);
        FetchEquip();
        SetFocus(focusOn);
    }

    private void FetchEquip()
    {
        for (int i = 0; i < skins.Count; i++)
        {
            var tmp = PlayerPrefs.GetInt(skins[i].name, -1);
            if (tmp > -1)
            {
                equip.Add(skins[i]);
                skins.RemoveAt(i);
                i--;
            }
        }
    }

    internal void ResetFocus()
    {
        FetchEquip();
        SetFocus(focusOn);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeFocus(int tab)
    {
        if (focusOn == (Tabs)tab)
            return;
        focusOn = (Tabs)tab;
        SetFocus(focusOn);
    }

    void SetFocus(Tabs tab)
    {
        ClearShopChildren();
        switch (tab)
        {
            case Tabs.Skins:
                SetPlaceholderText("Nothing to buy");
                DisplayTab(skins, skinPosition);
                break;
            case Tabs.PowerUps:
                SetPlaceholderText("Nothing to buy");
                DisplayTab(powerUps, new Vector3(0, 0));
                break;
            case Tabs.Equip:
                if (equip.Count == 0)
                {
                    SetPlaceholderText("No Skin");
                    EnablePlaceholder(true);
                }
                DisplayTab(equip, skinPosition, true);
                break;
            default:
                EnablePlaceholder(true);
                break;
        }
    }

    private void SetPlaceholderText(string v)
    {
        if (placeholder.activeInHierarchy)
            placeholder.GetComponentInChildren<Text>().text = v;
    }

    private void DisplayTab(List<GameObject> tab, Vector3 newPosition, bool isEquip = false)
    {
        EnablePlaceholder(tab.Count == 0);
        transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (tab.Count / 2 + tab.Count % 2 + 1) * 300);
        for (int i = 0; i < tab.Count; i++)
        {
            var tmpContainer = Instantiate(defaultItemContainer, transform);
            if (isEquip)
            {
                var tmpPanel = tmpContainer.transform.Find("Panel");
                tmpPanel.transform.Find("BuyButton").gameObject.SetActive(false);
                tmpPanel.transform.Find("PriceText").gameObject.SetActive(false);
            }
            tmpContainer.transform.localPosition = new Vector3(150 * (i % 2 == 0 ? -1 : 1), -300 * (i / 2));
            var tmpObj = Instantiate(tab[i], tmpContainer.transform);
            tmpObj.transform.localPosition = newPosition;
            tmpObj.transform.localScale = skinScale;
        }
    }

    private void EnablePlaceholder(bool v)
    {
        if (v)
            placeholder.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1080);
        placeholder.SetActive(v);
    }

    private void ClearShopChildren()
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
    }

    

    private static T[] GetAtPath<T>(string path)
    {
        ArrayList al = new ArrayList();
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);
        foreach (string fileName in fileEntries)
        {
            int index = fileName.LastIndexOf("/");
            string localPath = "Assets/" + path;

            if (index > 0)
                localPath += fileName.Substring(index);

            object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));

            if (t != null)
                al.Add(t);
        }
        T[] result = new T[al.Count];
        for (int i = 0; i < al.Count; i++)
            result[i] = (T)al[i];

        return result;
    }
}
