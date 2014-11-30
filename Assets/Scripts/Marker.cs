using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to put labeled markers on 3D scene objects
/// </summary>
public class Marker : MonoBehaviour
{
    /// <summary>
    /// The texture for the marker below the displayed text
    /// </summary>
    public Texture2D MarkerTexture;

    /// <summary>
    /// Number of pixels above the 3D labeled object
    /// </summary>
    public float Y_Offset = 10;

    /// <summary>
    /// Height of the texture
    /// </summary>
    public float Texture_Height = 20;

    /// <summary>
    /// Width of the Texture
    /// </summary>
    public float Texture_Width = 20;

    /// <summary>
    /// Camera used for screen point calculations
    /// </summary>
    public Camera InnerCamera;

    /// <summary>
    /// Calculated point to draw the marker and label
    /// </summary>
    private Vector2 screenPoint;

    /// <summary>
    /// Boolean value which is set true if object is in camera view
    /// </summary>
    private bool draw;

    /// <summary>
    /// Calculates the screenpoint of the 3D object and determines
    /// if it should be drawn or not.
    /// </summary>
    void Update()
    {
        screenPoint = InnerCamera.WorldToScreenPoint(transform.position);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        draw = GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    /// <summary>
    /// Draw the label and marker texture for 3D object on the screen
    /// </summary>
    void OnGUI()
    {
        // Only draws if in the camera view
        if (draw)
        {
            // Draws the marker above the object
            GUI.DrawTexture(new Rect(screenPoint.x - Texture_Width / 2, (Screen.height - screenPoint.y) - (Y_Offset + Texture_Height), Texture_Width, Texture_Height), MarkerTexture);
        }
    }
}
