using UnityEngine;
using System.Collections;

public class ComfortItem : MonoBehaviour {

    protected string fixes;
    protected bool draggable;
    protected bool hovering;
    protected bool dirty;
    protected bool cleaning;
    GameObject touching;
    float normA;

    
    protected void Awake()
    {
        draggable = true;
        hovering = false;
        dirty = false;
        normA = GetComponent<SpriteRenderer>().color.a;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonUp(0) && hovering && null != touching) {
            if (touching.tag == "Baby" && touching.GetComponent<Baby>().UseObject(fixes, dirty)) {
                draggable = false;
                hovering = false;
                PhaseManager.PhaseOver += MakeDirty;
            }
            else if (touching.tag == "Laundry" && dirty)
            {
                draggable = false;
                hovering = false;
                PhaseManager.PhaseOver += MakeClean;
            }
        }

        if (touching == null) {
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = normA;
            GetComponent<SpriteRenderer>().color = col;
            //Debug.Log("not touching anything");
        }

        
	}

    public void EnterPossibleUse()
    {
        Color col = GetComponent<SpriteRenderer>().color;
        col.a -= .5f;
        GetComponent<SpriteRenderer>().color = col;
        hovering = true;
        Debug.Log("enter");
    }

    public void ExitPossibleUse()
    {
        Color col = GetComponent<SpriteRenderer>().color;
        col.a += .5f;
        GetComponent<SpriteRenderer>().color = col;
        hovering = false;
        Debug.Log("exit");
    }

    void OnMouseDrag()
    {
        if (draggable)
        {
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector3(pos_move.x, pos_move.y, transform.position.z);
        }
    }

    void MakeDirty()
    {
        dirty = true;
        touching = null;
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.b -= .5f;
        GetComponent<SpriteRenderer>().color = temp;
        draggable = true;
        PhaseManager.PhaseOver -= MakeDirty;
        //ExitPossibleUse();
    }

    void MakeClean()
    {
        dirty = false;
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.b += .5f;
        GetComponent<SpriteRenderer>().color = temp;
        draggable = true;
        PhaseManager.PhaseOver -= MakeClean;
        //ExitPossibleUse();
    }

    public void Touched(bool touch, GameObject obj)
    {
        hovering = touch;
        touching = obj;
    }

    public bool IsDirty()
    {
        return dirty;
    }
}
