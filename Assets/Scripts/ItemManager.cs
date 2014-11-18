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
            items.Add(obj);
            
        }
        for (int i = 0; i < faAmt; i++)
        {
            GameObject obj = ((GameObject)Instantiate(FirstAid));
            obj.SetActive(true);
            items.Add(obj);
        }
        for (int i = 0; i < bAmt; i++)
        {
            GameObject obj = ((GameObject)Instantiate(Blanket));
            obj.SetActive(true);
            items.Add(obj);
        }
        for (int i = 0; i < bbAmt; i++)
        {
            GameObject obj = ((GameObject)Instantiate(BunnyBoo));
            obj.SetActive(true);
            items.Add(obj);
        }
        for (int i = 0; i < tAmt; i++)
        {
            GameObject obj = ((GameObject)Instantiate(Towel));
            obj.SetActive(true);
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
