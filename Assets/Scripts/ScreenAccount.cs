using UnityEngine;
using System.Collections;

public class ScreenAccount : MonoBehaviour
{
    #region Class Member Variables

    private const float DIVISOR = 16;

    private float unit_w;
    private float unit_h;

    private Texture2D stripColor;
    private Texture2D logoBMS;
    private Texture2D signOutButton;

    private Rect rectStrip;
    private Rect rectBMSLogo;
    private Rect rectSignOut;
    private Rect rectUsernameLabel;
    private Rect rectAccountLabel;
    private Rect rectEmailLabel;
    private Rect rectFavoriteLabel;

    private GUIStyle labelStyle;
    private GUIStyle highlightStyle;

    private string username;
    private string email;

    #endregion

    #region Unity Loop Methods

    private void Start()
    {
        stripColor = new Texture2D(1, 1);
        stripColor.SetPixel(0, 0, Color.blue);
        stripColor.Apply();

        logoBMS = Resources.Load<Texture2D>("Textures/LogoBMS");
        signOutButton = Resources.Load<Texture2D>("Textures/SignOutTexture");

        highlightStyle = new GUIStyle();
        highlightStyle.fontSize = 32;
        highlightStyle.fontStyle = FontStyle.Bold;
        highlightStyle.normal.textColor = Color.black;

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 25;
        labelStyle.normal.textColor = Color.black;

        username = SceneController.Instance.Username;
        email = SceneController.Instance.Email;
    }

    private void Update()
    {
        unit_w = Screen.width / DIVISOR;
        unit_h = Screen.height / DIVISOR;
        rectStrip = new Rect(0, 0, Screen.width, unit_h * 2);
        rectBMSLogo = new Rect(unit_w, unit_h * 0.15f, unit_w * 5, unit_h * 1.7f);
        rectSignOut = new Rect(Screen.width - unit_w * 2, unit_h * 0.5f, unit_w * 1.5f, unit_h);

        Vector2 labelSize = highlightStyle.CalcSize(new GUIContent("Account"));
        rectAccountLabel = new Rect(unit_w * 9, unit_h * 2.5f, labelSize.x, labelSize.y);

        labelSize = labelStyle.CalcSize(new GUIContent("Username: " + username));
        rectUsernameLabel = new Rect(unit_w * 9, unit_h * 4, labelSize.x, labelSize.y);

        labelSize = labelStyle.CalcSize(new GUIContent("Email: " + email));
        rectEmailLabel = new Rect(unit_w * 9, unit_h * 5, labelSize.x, labelSize.y);

        labelSize = highlightStyle.CalcSize(new GUIContent("Favorited Items"));
        rectFavoriteLabel = new Rect(unit_w * 9, unit_h * 6, labelSize.x, labelSize.y);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(rectStrip, stripColor);
        GUI.DrawTexture(rectBMSLogo, logoBMS);
        GUI.DrawTexture(rectSignOut, signOutButton);

        if (GUI.Button(rectSignOut, "", "Label"))
        {
            Application.LoadLevel("signon");
        }

        GUI.Label(rectAccountLabel, "Account", highlightStyle);
        GUI.Label(rectUsernameLabel, "Username: " + username, labelStyle);
        GUI.Label(rectEmailLabel, "Email: " + email, labelStyle);
        GUI.Label(rectFavoriteLabel, "Favorited Items", highlightStyle);
    }

    #endregion
}
