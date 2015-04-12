using UnityEngine;
using System.Collections;

public class PhaseManager : MonoBehaviour {
    public float phaseLength;
    public int workLoad;
    public ItemManager itemManager;
    public BabyManager babeManager;
	public int totalWork = 0;
    public int workDone;
    float phaseStart;
    public int phaseNum = 0;
    Rect timeRect;
    Rect workRect;
    Rect infoRect;
    Color old;
    float flashTime = -1;
    public delegate void PhaseManage();
    public static event PhaseManage PhaseOver;
    public Texture sun;
    public Texture moon;

	// Use this for initialization
	void Start () {
        phaseStart = Time.time;
        phaseLength = phaseLength / 2;
        workDone = 0;
        PhaseOver += WorkWeekOver;
        old = Camera.main.backgroundColor;
	}
	
	// Update is called once per frame
	void Update () {
        
	    if (Time.time - phaseStart >= phaseLength) {
            PhaseOver();
            phaseNum++;
            phaseStart = Time.time;
            if (phaseNum == 1)
            {
                phaseLength = phaseLength * 2;
            }
            else if (phaseNum % 12 == 0 && phaseNum > 5)
            {
                phaseLength -= 5;
            }
        }
        if (Time.time - flashTime >= .5) {
            Camera.main.backgroundColor = old;
        }

	}

    void WorkWeekOver()
    {
        
        if (phaseNum > 0 && phaseNum % 2 == 0)
        {
            workDone -= workLoad;
            if (workDone < 0)
            {
                //remove an item
                itemManager.LoseItem();
                Camera.main.backgroundColor = Color.red;
            }
            else
            {
                Camera.main.backgroundColor = Color.green;
            }
            flashTime = Time.time;
        }

        if (phaseNum > 0 && phaseNum % 4 == 0)
        {
            babeManager.AddAngryBaby();
        }
    }
    void OnGUI()
    {
        
        float workX = 10;
        float workY = Screen.height - (Screen.height * .2f);
        float workWidth = Screen.width * .1f;
        float workHeight = Screen.height * .1f;
        timeRect = new Rect(10, 10, Screen.width * .3f, Screen.height * .1f);
        workRect = new Rect(workX, workY, workWidth, workHeight);
        infoRect = new Rect(workX, workY - Screen.height * .05f, workWidth * 2, Screen.height * .05f);
        GUI.Label(timeRect, "Phase: " + phaseNum + " Time: " + Mathf.FloorToInt(phaseLength - (Time.time - phaseStart)));
        GUI.Label(infoRect, "Quota: " + workLoad + " Made: " + workDone);
        if (GUI.Button(workRect, "WORK")) {
            workDone++;
			totalWork++;
        }
    }

    public int GetCash()
    {
        return workDone;
    }

    public void SetCash(int leftover)
    {
        workDone = leftover;
    }
}
