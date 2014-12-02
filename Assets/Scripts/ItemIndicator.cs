using UnityEngine;
using System.Collections;

public class ItemIndicator : MonoBehaviour {
    public Sprite pacifier;
    public Sprite towel;
    public Sprite blanket;
    public Sprite boo;
    public Sprite fa;

    SpriteRenderer rend;
    Baby baby;
	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        baby = GetComponentInParent<Baby>();
	}

    void Awake()
    {
        Start();
    }
	
	// Update is called once per frame
	void Update () {
        switch (baby.GetState())
        {
            case 1 :
                rend.enabled = true;
                rend.sprite = pacifier;
                break;
            case 2 :
                rend.enabled = true;
                rend.sprite = boo;
                break;
            case 3 :
                rend.enabled = true;
                rend.sprite = towel;
                break;
            case 4 :
                rend.enabled = true;
                rend.sprite = blanket;
                break;
            case 5 :
                rend.enabled = true;
                rend.sprite = fa;
                break;
            default:
                rend.enabled = false;
                break;

        }
	}

}
