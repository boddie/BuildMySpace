using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{
    private float dist;
    private Vector3 v3Offset;
    private Plane plane;
    private Camera innerCamera;

    private void Start()
    {
        innerCamera = GameObject.Find("Inner Camera").camera;
    }

    private void OnMouseDown()
    {
        plane.SetNormalAndPosition(innerCamera.transform.forward, transform.position);
        Ray ray = innerCamera.ScreenPointToRay(Input.mousePosition);
        float dist;
        plane.Raycast(ray, out dist);
        v3Offset = transform.position - ray.GetPoint(dist);
    }

    private void OnMouseDrag()
    {
        Ray ray = innerCamera.ScreenPointToRay(Input.mousePosition);
        float dist;
        plane.Raycast(ray, out dist);
        Vector3 v3Pos = ray.GetPoint(dist);
        transform.position = v3Pos + v3Offset;
    }
}
