using UnityEngine;
using System.Collections;

public class Laundry : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ComfortItem>().IsDirty())
        {
            other.gameObject.GetComponent<ComfortItem>().EnterPossibleUse();
            other.gameObject.GetComponent<ComfortItem>().Touched(true, this.gameObject);
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        try
        {
            other.gameObject.GetComponent<ComfortItem>().ExitPossibleUse();
            other.gameObject.GetComponent<ComfortItem>().Touched(false, null);
        }
        catch (MissingComponentException) {}
    }
}
