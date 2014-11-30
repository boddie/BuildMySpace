using UnityEngine;
using System.Collections;

public class ScreenHome : MonoBehaviour
{
    #region Class Member Variables

    private const float DIVISOR = 16;
    private const int FIELD_MAX = 30;

    private float unit_w;
    private float unit_h;

    private Texture2D stripColor;
    private Texture2D loginButton;
    private Texture2D registerButton;
    private Texture2D homeImage;
    private Texture2D logoBMS;

    private Rect rectStrip;
    private Rect rectLoginButton;
    private Rect rectRegisterButton;
    private Rect rectHomeImage;
    private Rect rectBMSLogo;
    private Rect rectUsernameLabel;
    private Rect rectUsernameField;
    private Rect rectPassLabel;
    private Rect rectPassField;
    private Rect rectNewUserLabel;

    private GUIStyle labelStyle;
    private GUISkin fieldSkin;

    private string username = string.Empty;
    private string password = string.Empty;

    #endregion

    #region Unity Loop Methods

    private void Start () 
    {
        stripColor = new Texture2D(1, 1);
        stripColor.SetPixel(0, 0, Color.blue);
        stripColor.Apply();

        loginButton = Resources.Load<Texture2D>("Textures/LoginTexture");
        registerButton = Resources.Load<Texture2D>("Textures/RegisterTexture");
        homeImage = Resources.Load<Texture2D>("Textures/RoomSingleBasicTexture");
        logoBMS = Resources.Load<Texture2D>("Textures/LogoBMS");

        fieldSkin = Resources.Load<GUISkin>("Skins/TextFieldSkin");

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 25;
        labelStyle.normal.textColor = Color.black;

        username = SceneController.Instance.Username;
	}
	
	private void Update ()
    {
        unit_w = Screen.width / DIVISOR;
        unit_h = Screen.height / DIVISOR;
        rectStrip = new Rect(0, 0, Screen.width, unit_h * 2);
        rectHomeImage = new Rect(unit_w, unit_h * 3, unit_w * 7, unit_h * 12);
        rectBMSLogo = new Rect(unit_w, unit_h * 0.15f, unit_w * 5, unit_h * 1.7f);

        Vector2 labelSize = labelStyle.CalcSize(new GUIContent("Username"));
        rectUsernameLabel = new Rect(unit_w * 9, unit_h * 3, labelSize.x, labelSize.y);
        rectUsernameField = new Rect(unit_w * 9, unit_h * 4, unit_w * 4, unit_h);

        labelSize = labelStyle.CalcSize(new GUIContent("Password"));
        rectPassLabel = new Rect(unit_w * 9, unit_h * 5 + 3, labelSize.x, labelSize.y);
        rectPassField = new Rect(unit_w * 9, unit_h * 6, unit_w * 4, unit_h);

        rectLoginButton = new Rect(unit_w * 11.5f, unit_h * 7.5f, unit_w * 1.5f, unit_h);

        labelSize = labelStyle.CalcSize(new GUIContent("Are you a new user?"));
        rectNewUserLabel = new Rect(unit_w * 9, unit_h * 9, labelSize.x, labelSize.y);

        rectRegisterButton = new Rect(unit_w * 11.5f, unit_h * 10f, unit_w * 1.5f, unit_h);
	}

    private void OnGUI ()
    {
        GUI.DrawTexture(rectStrip, stripColor);
        GUI.DrawTexture(rectHomeImage, homeImage);
        GUI.DrawTexture(rectBMSLogo, logoBMS);

        GUI.Label(rectUsernameLabel, "Username", labelStyle);
        GUI.Label(rectPassLabel, "Password", labelStyle);

        GUISkin curSkin = GUI.skin;
        GUI.skin = fieldSkin;
        username = GUI.TextField(rectUsernameField, username, FIELD_MAX);
        password = GUI.PasswordField(rectPassField, password, '*', FIELD_MAX);
        GUI.skin = curSkin;

        GUI.DrawTexture(rectLoginButton, loginButton);
        if(GUI.Button(rectLoginButton, "", "Label"))
        {
            if (username == SceneController.Instance.Username &&
                password == SceneController.Instance.Password &&
                username != string.Empty &&
                password != string.Empty)
            {
                Application.LoadLevel("account");
            }
            else
            {
                // Todo error
            }
        }

        GUI.Label(rectNewUserLabel, "Are you a new user?", labelStyle);

        GUI.DrawTexture(rectRegisterButton, registerButton);
        if (GUI.Button(rectRegisterButton, "", "Label"))
        {
            Application.LoadLevel("register");
        }
    }

    #endregion
}
