using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchScript : MonoBehaviour {
    void Update()
    {
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch Began");
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch and Moving");
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Lifted Finger");
            }
        }
    }
    
}
