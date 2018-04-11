// Useful for Text Meshes that should face the camera.
//
// In some cases there seems to be a Unity bug where the text meshes end up in
// weird positions if it's not positioned at (0,0,0). In that case simply put it
// into an empty GameObject and use that empty GameObject for positioning.

using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    void Awake()
    {
        enabled = false;
    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }
}