using UnityEngine;
using System.Collections.Generic;

public class ScreenEditSingleBasic : MonoBehaviour
{
    #region Class Member Variables

    public GameObject TopView;
    public GameObject SideView;
    public GameObject InnerCamera;

    private const float DIVISOR = 16;

    private float unit_w;
    private float unit_h;

    private Texture2D stripColor;
    private Texture2D logoBMS;
    private Texture2D signOutButton;
    private Texture2D backButton;
    private Texture2D topViewButton;
    private Texture2D sideViewButton;

    private Rect rectStrip;
    private Rect rectBMSLogo;
    private Rect rectSignOut;
    private Rect rectLayoutLabel;
    private Rect rectItemLabel;
    private Rect rectBackButton;
    private Rect rectSideView;
    private Rect rectTopView;

    private Vector2 favoriteScrollPos = Vector2.zero;

    private GUIStyle labelStyle;
    private GUIStyle highlightStyle;

    #endregion

    #region Unity Loop Methods

    private void Start()
    {
        stripColor = new Texture2D(1, 1);
        stripColor.SetPixel(0, 0, Color.blue);
        stripColor.Apply();

        logoBMS = Resources.Load<Texture2D>("Textures/LogoBMS");
        signOutButton = Resources.Load<Texture2D>("Textures/SignOutTexture");
        backButton = Resources.Load<Texture2D>("Textures/BackTexture");
        topViewButton = Resources.Load<Texture2D>("Textures/TopViewTexture");
        sideViewButton = Resources.Load<Texture2D>("Textures/SideViewTexture");

        highlightStyle = new GUIStyle();
        highlightStyle.fontSize = 32;
        highlightStyle.fontStyle = FontStyle.Bold;
        highlightStyle.normal.textColor = Color.black;

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 25;
        labelStyle.normal.textColor = Color.black;
    }

    private void Update()
    {
        unit_w = Screen.width / DIVISOR;
        unit_h = Screen.height / DIVISOR;
        rectStrip = new Rect(0, 0, Screen.width, unit_h * 2);
        rectBMSLogo = new Rect(unit_w, unit_h * 0.15f, unit_w * 5, unit_h * 1.7f);
        rectSignOut = new Rect(Screen.width - unit_w * 2, unit_h * 0.5f, unit_w * 1.5f, unit_h);
        rectBackButton = new Rect(Screen.width - unit_w * 4, unit_h * 0.5f, unit_w * 1.5f, unit_h);
        rectTopView = new Rect(unit_w, Screen.height - unit_h * 1.5f, unit_w * 1.5f, unit_h);
        rectSideView = new Rect(unit_w * 3, Screen.height - unit_h * 1.5f, unit_w * 1.5f, unit_h);

        Vector2 labelSize = highlightStyle.CalcSize(new GUIContent("Single Basic Layout"));
        rectLayoutLabel = new Rect(unit_w, unit_h * 2.5f, labelSize.x, labelSize.y);

        labelSize = highlightStyle.CalcSize(new GUIContent("Layout Items"));
        rectItemLabel = new Rect(unit_w * 9, unit_h * 2.5f, labelSize.x, labelSize.y);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(rectStrip, stripColor);
        GUI.DrawTexture(rectBMSLogo, logoBMS);
        GUI.DrawTexture(rectSignOut, signOutButton);
        GUI.DrawTexture(rectBackButton, backButton);
        if (GUI.Button(rectBackButton, "", "Label"))
        {
            Application.LoadLevel(SceneController.Instance.BackStack.Pop());
        }

        if (GUI.Button(rectSignOut, "", "Label"))
        {
            SceneController.Instance.BackStack.Push("singleBasic");
            Application.LoadLevel("signon");
        }

        GUI.DrawTexture(rectTopView, topViewButton);
        if (GUI.Button(rectTopView, "", "Label"))
        {
            InnerCamera.transform.position = TopView.transform.position;
            InnerCamera.transform.rotation = TopView.transform.rotation;
        }

        GUI.DrawTexture(rectSideView, sideViewButton);
        if (GUI.Button(rectSideView, "", "Label"))
        {
            InnerCamera.transform.position = SideView.transform.position;
            InnerCamera.transform.rotation = SideView.transform.rotation;
        }

        GUI.Label(rectLayoutLabel, "Single Basic Layout", highlightStyle);
        GUI.Label(rectItemLabel, "Layout Items", highlightStyle);

        DrawItems();
    }

    #endregion

    private void DrawItems()
    {
        favoriteScrollPos = GUI.BeginScrollView(new Rect(0, 0, 0, 0), favoriteScrollPos, new Rect(0, 0, 0, 0));
        foreach (var item in SceneController.Instance.ItemList)
        {
            Vector2 size = labelStyle.CalcSize(new GUIContent(item.Key));
            GUI.Label(new Rect(0, 0, size.x, size.y), item.Key, labelStyle);
            GUI.DrawTexture(new Rect(0, 0, 0, 0), item.Value.Logo);
            GUI.DrawTexture(new Rect(0, 0, 0, 0), item.Value.EfficiencyImage);
        }
        GUI.EndScrollView();
    }
}
