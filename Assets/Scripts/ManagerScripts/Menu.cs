using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
    bool lastPause = false;
    bool paused = false;
    public Texture back;
    Rect winRect;
    Rect quitRect;
    Rect cashRect;
    int cash;
    public ItemManager items;
    public PhaseManager phaseManager;
    Dictionary<string, int> itemDict;
    Dictionary<string, int> boughtDict;
    Color standardCol;
	// Use this for initialization
	void Start () {
        SetRects();
	}

    void Awake()
    {
        Start();
    }
	// Update is called once per frame
	void Update () {

        InputCheck();
        SetRects();
        lastPause = paused;
	}

    void OnGUI()
    {
        if (paused) {
            standardCol = GUI.color;
            GUI.Window(0, winRect, ShowStore, "Game Paused");
        }
    }

    void SetRects()
    {
        
        winRect = new Rect(0, 0, Screen.width, Screen.height);
        cashRect = new Rect(Screen.width/2, Screen.height * .02f, 100f, 20f);
    }
    void ShowStore(int winID)
    {
        //sets background to grey so player can't see game
        GUI.DrawTexture(winRect, back);
        GUI.Label(cashRect, "Cash: "+ cash);

        float buf = 10;
        float itemX = Screen.width * .35f;
        float itemHeight = Screen.height * .08f;
        float itemY = Screen.height * .2f;
        float numWidth = 20f;
        float labelWidth = 130f;
        float buttonWidth = 100f;
        int counter = 0;
        foreach (string key in itemDict.Keys)
        {
            int price = GetPrice(key);
            Rect labelRect = new Rect(itemX,
                itemY + counter * (itemHeight + buf),
                labelWidth, itemHeight);
            Rect numRect = new Rect(itemX + labelWidth,
                itemY + counter * (itemHeight + buf),
                numWidth, itemHeight);
            Rect up = new Rect(itemX + 2 * labelWidth,
                itemY + counter * (itemHeight + buf),
                buttonWidth, itemHeight);
            Rect down = new Rect(itemX + buttonWidth + 2 * labelWidth + buf,
                itemY + counter * (itemHeight + buf),
                buttonWidth, itemHeight);

            GUI.Label(labelRect, "Price: $"+price+" "+key);

            if (boughtDict[key] > 0)
            {
                GUI.color = Color.green;
                GUI.Label(numRect, (itemDict[key] + boughtDict[key]).ToString());
                GUI.color = standardCol;
            }
            else
            {
                GUI.Label(numRect, (itemDict[key] + boughtDict[key]).ToString());
            }

            if (GUI.Button(up, "Add to cart") && cash - price >= 0)
            {
                boughtDict[key] = boughtDict[key] + 1;
                cash -= price;
            }
            if (GUI.Button(down, "remove") && boughtDict[key] !=0) {
                boughtDict[key] = boughtDict[key] - 1;
                cash += price;
            }
            counter++;
        }
        Rect purchase = new Rect(Screen.width/2 - buttonWidth/2,
            itemY + counter * (itemHeight + buf),
            buttonWidth, itemHeight);
        if (GUI.Button(purchase, "Purchase!"))
        {
            phaseManager.SetCash(cash);
            items.AddItems(boughtDict);
            NewStoreSesh();
        }

    }

    void InputCheck()
    { 
        paused = Input.GetKeyUp(KeyCode.Escape) ? !paused : paused;
        if (lastPause != paused)
        {
            if (!paused)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0;
                NewStoreSesh();
            }
        }
    }

    void NewStoreSesh()
    {
        itemDict = items.GetItems();
        boughtDict = new Dictionary<string, int>();
        foreach(string key in itemDict.Keys) {
            boughtDict.Add(key, 0);
        }
        cash = phaseManager.GetCash();
    }

    int GetPrice(string item)
    {
        if (item == "Pacifier")
        {
            return 20;
        }
        if (item == "BunnyBooBoo")
        {
            return 30;
        }
        if (item == "First Aid")
        {
            return 50;
        }
        if (item == "Blanket")
        {
            return 20;
        }
        if (item == "Towel")
        {
            return 20;
        }
        else
        {
            throw new UnityException("item doesn't have price");
        }
    }
}
