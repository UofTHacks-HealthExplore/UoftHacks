using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCamera : MonoBehaviour
{
    public Renderer companion;
    public bool visible;

    void Update()
    {
        if (IsVisible(companion))
        {
            visible = true;
        }
        else
        {
            visible = false;
        }
    }

    bool IsVisible(Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
