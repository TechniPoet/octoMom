using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
    public Sprite sun;
    public Sprite moon;
    SpriteRenderer rend;
    int counter = 0;
	// Use this for initialization
	void Start () {
        PhaseManager.PhaseOver += Swap;
        rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Swap()
    {
        if (counter % 2 == 0)
        {
            rend.sprite = sun;
        }
        else
        {
            rend.sprite = moon;
        }
        counter++;
    }
}
