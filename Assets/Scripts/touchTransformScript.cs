using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class touchTransformScript : MonoBehaviour
{
    private float dist,
                  initialDistance,
                  currentDistance;
    private bool dragging,
                 scale_rotate;
    private Vector3 v3,
                    initialScale, 
                    initialDirection, 
                    currentDirection, 
                    fingerTouch1_Offset, 
                    finger1_pos, 
                    finger2_pos;
    private GameObject go;
    private Touch fingerTouch1, 
                  fingerTouch2;
    private LayerMask defaultLayerMask;

    private void Start()
    {
        dragging = false;
        scale_rotate = false;
        defaultLayerMask = LayerMask.GetMask("Default");
    }

    void Update()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0) && 
        !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(1))
            touchInput();
    }

    private void touchInput()
    {
        //when touch is more than 1 finger
        if (Input.touchCount > 0)
        {
            handlefirstTouch();
            scale_rotate = false;
            //when touch is more than 2 finger
            if (Input.touchCount > 1)
            {
                dragging = false;
                scale_rotate = true;
                handlesecondTouch();
            }
        }
    }
    

    private void handlefirstTouch()
    {
        //Defining conditions about fingerTouch1
        fingerTouch1 = Input.GetTouch(0);
        //finger1_pos means always get fingerTouch1 position
        finger1_pos = fingerTouch1.position;

        //When screen detect there are one finger already touch on it
        if (fingerTouch1.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(finger1_pos);
            if (Physics.Raycast(ray, out hit, 100, defaultLayerMask))
            {
                go = hit.transform.gameObject;
                dist = hit.transform.position.z - Camera.main.transform.position.z;
                v3 = new Vector3(finger1_pos.x, finger1_pos.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                fingerTouch1_Offset = go.transform.position - v3;
                dragging = true;
            }
        }
        if (go != null)
        {
            if (dragging && fingerTouch1.phase == TouchPhase.Moved)
            {
                v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                GOMoved(v3, fingerTouch1_Offset);
            }
            if (dragging && (fingerTouch1.phase == TouchPhase.Ended || fingerTouch1.phase == TouchPhase.Canceled))
            {
                dragging = false;
            }
        }
    }

    private void handlesecondTouch()
    {
        if (go != null)
        {
            //Defining conditions about fingerTouch2
            fingerTouch2 = Input.GetTouch(1);
            //finger2_pos means always get fingerTouch2 position
            finger2_pos = fingerTouch2.position;

            if (fingerTouch2.phase == TouchPhase.Began)
            {
                // Save the direction between first and second touch
                currentDirection = finger2_pos - finger1_pos;
                // Initialize the 'last' direction for comparisons
                initialDirection = currentDirection;

                // get the distance between the touches, saving the initial distance for comparison
                currentDistance = (initialDirection).sqrMagnitude;
                initialDistance = currentDistance;

                // save the object's starting scale
                initialScale = go.transform.localScale;
            }
            else if (fingerTouch2.phase == TouchPhase.Moved || fingerTouch1.phase == TouchPhase.Moved)
            {
                //
                // if either finger 1 touch or finger 2 touch moved, update the rotation and scale
                //

                // get the current direction between the touches
                currentDirection = finger2_pos - finger1_pos;

                // find the angle difference between the last direction and the current
                float angle = Vector3.Angle(currentDirection, initialDirection);

                // Vector3.Angle only outputs positives, so check if it should be a negative angle
                Vector3 cross = Vector3.Cross(currentDirection, initialDirection);
                if (cross.z > 0)
                {
                    angle = -angle;
                }
                // update rotation
                GORotation(angle);

                // save this direction for next frame's comparison
                initialDirection = currentDirection;

                // get the current distance between touches
                currentDistance = (currentDirection).sqrMagnitude;

                // get what % of the intial distance this new distance is
                float difference = currentDistance / initialDistance;

                // scale by that percentage
                GOScale(difference);
            }

            // if the second touch ended
            if (fingerTouch2.phase == TouchPhase.Ended || fingerTouch1.phase == TouchPhase.Canceled)
            {
                // update the first touch offset so dragging will start again from wherever the first touch is now
                fingerTouch1_Offset = go.transform.position - finger1_pos;
            }
        }
    }

    private void GOMoved(Vector3 v3, Vector3 offset)
    {
        go.transform.position = v3 + offset;
    }

    private void GORotation(float angle)
    {
        go.transform.Rotate(Vector3.up, angle);
    }

    private void GOScale(float difference)
    {
        Vector3 newScale = initialScale * difference;
        go.transform.localScale = newScale;
    }
}
