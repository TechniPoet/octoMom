using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
    public GameObject Pacifier;
    public GameObject FirstAid;
    public GameObject Blanket;
    public GameObject BunnyBoo;
    public GameObject Towel;
    public int pAmt;
    public int faAmt;
    public int bAmt;
    public int bbAmt;
    public int tAmt;
    List<GameObject> items;

	// Use this for initialization
	void Awake () {
        items = new List<GameObject>();
        
        for (int i = 0; i < pAmt; i++ )
        {
            GameObject obj = ((GameObject)Instantiate(Pacifier));
            obj.SetActive(true);
			obj.transform.parent = this.gameObject.transform;
            items.Add(obj);
        }
        for (int i = 0; i < faAmt; i++)
        {
            GameObject obj = ((GameObject)Instantiate(FirstAid));
            obj.SetActive(true);
			obj.transform.parent = this.gameObject.transform;
            items.Add(obj);
        }
        for (int i = 0; i < bAmt; i++)
        {
            GameObject obj = ((GameObject)Instantiate(Blanket));
            obj.SetActive(true);
			obj.transform.parent = this.gameObject.transform;
            items.Add(obj);
        }
        for (int i = 0; i < bbAmt; i++)
        {
            GameObject obj = ((GameObject)Instantiate(BunnyBoo));
            obj.SetActive(true);
			obj.transform.parent = this.gameObject.transform;
            items.Add(obj);
        }
        for (int i = 0; i < tAmt; i++)
        {
            GameObject obj = ((GameObject)Instantiate(Towel));
            obj.SetActive(true);
			obj.transform.parent = this.gameObject.transform;
            items.Add(obj);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void LoseItem()
    {
        bool lost = false;
        while (!lost && !AllGone()) {
            int chosen = Random.Range(0, items.Count);
            if(items[chosen].activeSelf) {
                items[chosen].SetActive(false);
                lost = true;
            }
        }
    }

    public Dictionary<string, int> GetItems() {
        Dictionary<string, int> ret = new Dictionary<string, int>();
        ret.Add("Pacifier", pAmt);
        ret.Add("BunnyBooBoo", bbAmt);
        ret.Add("First Aid", faAmt);
        ret.Add("Blanket", bAmt);
        ret.Add("Towel", tAmt);
        return ret;
    }

    public void AddItems(Dictionary<string, int> bought) {
        if (bought["Pacifier"] > 0)
        {
            for (int i = 0; i < bought["Pacifier"]; i++)
            {
                GameObject obj = ((GameObject)Instantiate(Pacifier));
                obj.SetActive(true);
                items.Add(obj);
                pAmt++;
            }
        }
        if (bought["BunnyBooBoo"] > 0)
        {
            for (int i = 0; i < bought["BunnyBooBoo"]; i++)
            {
                GameObject obj = ((GameObject)Instantiate(BunnyBoo));
                obj.SetActive(true);
                items.Add(obj);
                bbAmt++;
            }
        }
        if (bought["First Aid"] > 0)
        {
            for (int i = 0; i < bought["First Aid"]; i++)
            {
                GameObject obj = ((GameObject)Instantiate(FirstAid));
                obj.SetActive(true);
                items.Add(obj);
                faAmt++;
            }
        }
        if (bought["Blanket"] > 0)
        {
            for (int i = 0; i < bought["Blanket"]; i++)
            {
                GameObject obj = ((GameObject)Instantiate(Blanket));
                obj.SetActive(true);
                items.Add(obj);
                bAmt++;
            }
        }
        if (bought["Towel"] > 0)
        {
            for (int i = 0; i < bought["Towel"]; i++)
            {
                GameObject obj = ((GameObject)Instantiate(Towel));
                obj.SetActive(true);
                items.Add(obj);
                tAmt++;
            }
        }
    }

    bool AllGone()
    {
        foreach(GameObject item in items) {
            if(item.activeSelf) {
                return false;
            }
        }
        return true;
    }
}
