using UnityEngine;
using System.Collections;

public class BabyManager : MonoBehaviour {
    Baby[] babies;
    int troubleBabies = 1;

	// Use this for initialization
	void Start () {
        babies = GetComponentsInChildren<Baby>();
        PhaseManager.PhaseOver += BabyEvents;
	}
	
	// Update is called once per frame
	void Update () {
	    if(AllDead()) {
            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
	}

    void BabyEvents() {
        int babiesWoken = 0;
        int attempts = 0;
        if (!AllAwake()) {
            while (babiesWoken < troubleBabies && attempts < 80)
            {
                attempts++;
                int selectedBaby = Random.Range(0, babies.Length);
                if (babies[selectedBaby].IsSleeping())
                {
                    
                    int babyCondition = Random.Range(0, 10);
                    if (babyCondition <= 2) {
                        babies[selectedBaby].Cry();
                    }
                    else if (babyCondition == 4)
                    {
                        babies[selectedBaby].Puking();
                    }
                    else if (babyCondition <= 7)
                    {
                        babies[selectedBaby].Escaping();
                    }
                    babiesWoken++;
                }
            }
        }
        
    }

    bool AllAwake()
    {
        foreach(Baby bab in babies) {
            if (bab.IsSleeping())
                return false;
        }
        return true;
    }

    public void AddAngryBaby()
    {
        if (troubleBabies < babies.Length) {
            troubleBabies++;
        }
        
    }
    bool AllDead()
    {
        foreach (Baby bab in babies)
        {
            if (!bab.IsDead())
                return false;
        }
        return true;
    }
}
