using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {
    Rect stateRect;
    //ItemIndicator indicator;
    public Sprite sleepTex;
    public Sprite cryingTex;
    public Sprite pukingTex;
    public Sprite hurtTex;
    public Sprite deadTex;
    public Sprite escapingTex;
    public Sprite infectionTex;
    public Baby leftBaby;
    public Baby rightBaby;

    public int _babyNum;

    private Sprite stateTex;
    private SpriteRenderer rend;
    GameObject heldObj;

    bool Sleeping = true;
    bool _crying = false;
    bool _hurt = false;
    bool _puking = false;
    bool _escaping = false;
    bool _infected = false;
    bool _dead = false;
    bool hasObj = false;
    bool escalateAdded = false;
	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        //indicator = GetComponentInChildren<ItemIndicator>();
        this.transform.localScale = new Vector3(.5f, .5f, 1f);
        stateTex = sleepTex;
        rend.sprite = sleepTex;
	}

    

    void Awake()
    {
        Start();
    }
	
	// Update is called once per frame
	void Update () {
        stateRect = new Rect(Camera.main.WorldToScreenPoint(transform.position).x -30,
            Screen.height - (Camera.main.WorldToScreenPoint(transform.position).y + 30), 
            80, 80);
        if (!hasObj && !Sleeping && !escalateAdded)
        {
            PhaseManager.PhaseOver += Escalate;
            escalateAdded = true;
        }
        else
        {
            if (escalateAdded && Sleeping)
            {
                PhaseManager.PhaseOver -= Escalate;
                escalateAdded = false;
            }
        }
        
	}

    void Pacified()
    {
        Sleeping = true;
        _crying = false;
        hasObj = false;
        _hurt = false;
        _puking = false;
        _escaping = false;
        _infected = false;
        rend.sprite = sleepTex;
        PhaseManager.PhaseOver -= Pacified;
    }

    public void Cry()
    {
        Sleeping = false;
        _crying = true;
        rend.sprite = cryingTex;
    }

    public void Hurt()
    {
        Sleeping = false;
        _escaping = false;
        _hurt = true;
        rend.sprite = hurtTex;
    }

    public void Puking()
    {
        Sleeping = false;
        _puking = true;
        rend.sprite = pukingTex;
    }

    public void Escaping()
    {
        Sleeping = false;
        _crying = false;
        _escaping = true;
        rend.sprite = escapingTex;
    }
    public void Infected()
    {
        Sleeping = false;
        _hurt = false;
        _puking = false;
        _infected = true;
        rend.sprite = infectionTex;
    }
    public void Dead()
    {
        Sleeping = false;
        _dead = true;
        rend.sprite = deadTex;
    }

    public bool IsSleeping()
    {
        return Sleeping;
    }

    public bool IsDead()
    {
        return _dead;
    }

    public bool UseObject(string fixes, bool dirty)
    {
        if (!dirty) {
            if (_crying && fixes == "Crying")
            {
                hasObj = true;
                PhaseManager.PhaseOver += Pacified;
                return true;
            }
            if (_hurt && fixes == "Hurt")
            {
                hasObj = true;
                PhaseManager.PhaseOver += Pacified;
                return true;
            }
            if (_puking && fixes == "Puking")
            {
                hasObj = true;
                PhaseManager.PhaseOver += Pacified;
                return true;
            }
            if (_escaping && fixes == "Escaping")
            {
                hasObj = true;
                PhaseManager.PhaseOver += Pacified;
                return true;
            }
            if (_infected && fixes == "Infection")
            {
                hasObj = true;
                PhaseManager.PhaseOver += Pacified;
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_crying && other.tag == "Pacifier" 
            || _hurt && other.tag == "BunnyBooBoo" 
            || _puking && other.tag == "Towel" 
            || _escaping && other.tag == "Blanket"
            || _infected && other.tag == "FirstAid") {
            other.gameObject.GetComponent<ComfortItem>().EnterPossibleUse();
            other.gameObject.GetComponent<ComfortItem>().Touched(true, this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_crying && other.tag == "Pacifier"
            || _hurt && other.tag == "BunnyBooBoo"
            || _puking && other.tag == "Towel"
            || _escaping && other.tag == "Blanket"
            || _infected && other.tag == "FirstAid")
        {
            other.gameObject.GetComponent<ComfortItem>().ExitPossibleUse();
            other.gameObject.GetComponent<ComfortItem>().Touched(false, null);
        }
    }

    void Escalate()
    {
        Debug.Log("Baby:" + _babyNum + " Escalating");
        //If left crying then neighbors will wake up
        if(_crying) {
            Debug.Log("Baby:" + _babyNum + " crying");
            Escaping();
            if (leftBaby.IsSleeping())
                leftBaby.Cry();
            if(rightBaby.IsSleeping()) 
                rightBaby.Cry();
        }
        else if (_hurt || _puking) {
            Debug.Log("Baby:" + _babyNum + " Hurt or puking");
            //untreated puking or injury leads to infection
            Infected();
        }
        else if (_escaping) {
            Debug.Log("Baby:" + _babyNum + " escaping");
            //escaping hurts the baby
            Hurt();
        }
        else if (_infected) {
            Debug.Log("Baby:" + _babyNum + " infected");
            //untreated infections kill babies
            Dead();
        }
    }
    public int GetState()
    {
        if (hasObj || Sleeping || IsDead())
        {
            return 0;
        }
        else
        {
            if (_crying)
            {
                return 1;
            }
            else if (_hurt)
            {
                return 2;
            }
            else if (_puking)
            {
                return 3;
            }
            else if (_escaping)
            {
                return 4;
            }
            else if (_infected)
            {
                return 5;
            }
            else
            {
                throw new UnityException("Baby has an odd state");
            }
        }
    }
}
