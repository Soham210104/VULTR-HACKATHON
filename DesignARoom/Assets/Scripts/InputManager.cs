using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject arObj; // AR object to instantiate
    [SerializeField] private Camera arCam; // AR camera
    [SerializeField] private ARRaycastManager raycastManager; // AR Raycast Manager
    List<ARRaycastHit> hits = new List<ARRaycastHit>(); // List to store raycast hits

    // Update is called once per frame
    void Update()
    {
        // Check if there is at least one touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            // Check if the touch phase is 'Began', indicating the start of a touch
            if (touch.phase == TouchPhase.Began)
            {
                // Create a ray from the AR camera to the touch position
                Ray ray = arCam.ScreenPointToRay(touch.position);

                // Perform a raycast using the AR Raycast Manager
                if (raycastManager.Raycast(ray, hits))
                {
                    // Get the pose (position and rotation) of the first raycast hit
                    Pose hitPose = hits[0].pose;

                    // Instantiate the AR object at the hit position and rotation
                    Instantiate(arObj, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}