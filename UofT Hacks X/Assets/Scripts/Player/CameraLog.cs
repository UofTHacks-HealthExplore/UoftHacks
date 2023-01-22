using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLog : MonoBehaviour
{
    public List<GameObject> visibleGameObjects = new List<GameObject>();
    public GameObject visibleObject;
    [SerializeField] private float maxRange = 10f;

    public void OnInteract(InputValue value){
        visibleObject.GetComponent<CamInteract>().OnInteract();
    }

    void Update()
    {
        FindObjects();
    }

    void FindObjects()
    {
        GameObject[] detectables = GameObject.FindGameObjectsWithTag("Detectable");
        //List<GameObject> visibleGameObjects = new List<GameObject>();
        visibleGameObjects.Clear();

        foreach (GameObject detetectable in detectables)
        {
            if (IsVisible(detetectable.GetComponent<Renderer>()))
            {
                visibleGameObjects.Add(detetectable);
            }
        }

        GameObject closestObject = null;
        if (visibleGameObjects.Count > 0){
            closestObject = visibleGameObjects[0];
            foreach (GameObject visible in visibleGameObjects){
                if (Vector3.Distance(visible.transform.position, transform.position) < Vector3.Distance(closestObject.transform.position, transform.position) && Vector3.Distance(visible.transform.position, transform.position) < maxRange){
                    closestObject = visible;
                }
            }
        }
        if (closestObject != null){
            if (Vector3.Distance(closestObject.transform.position, transform.position) < maxRange){
                visibleObject = closestObject;
            }
        }
    }

    bool IsVisible(Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
