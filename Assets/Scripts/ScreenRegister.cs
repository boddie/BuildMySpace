using UnityEngine;
using System.Collections;

public class ScreenRegister : MonoBehaviour
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
    private Rect rectRegisterButton;
    private Rect rectHomeImage;
    private Rect rectBMSLogo;
    private Rect rectUsernameLabel;
    private Rect rectUsernameField;
    private Rect rectPassLabel;
    private Rect rectPassField;
    private Rect rectRePassLabel;
    private Rect rectRePassField;
    private Rect rectRegisterLabel;
    private Rect rectEmailLabel;
    private Rect rectEmailField;
    private Rect rectErrorLabel;

    private GUIStyle labelStyle;
    private GUIStyle registerStyle;
    private GUISkin fieldSkin;

    private string username = string.Empty;
    private string password = string.Empty;
    private string repassword = string.Empty;
    private string email = string.Empty;
    private string error = string.Empty;

    #endregion

    #region Unity Loop Methods

    private void Start()
    {
        stripColor = new Texture2D(1, 1);
        stripColor.SetPixel(0, 0, Color.blue);
        stripColor.Apply();

        registerButton = Resources.Load<Texture2D>("Textures/RegisterTexture");
        homeImage = Resources.Load<Texture2D>("Textures/RoomSingleBasicTexture");
        logoBMS = Resources.Load<Texture2D>("Textures/LogoBMS");

        fieldSkin = Resources.Load<GUISkin>("Skins/TextFieldSkin");

        registerStyle = new GUIStyle();
        registerStyle.fontSize = 32;
        registerStyle.fontStyle = FontStyle.Bold;
        registerStyle.normal.textColor = Color.black;

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 25;
        labelStyle.normal.textColor = Color.black;

        error = string.Empty;
    }

    private void Update()
    {
        unit_w = Screen.width / DIVISOR;
        unit_h = Screen.height / DIVISOR;
        rectStrip = new Rect(0, 0, Screen.width, unit_h * 2);
        rectHomeImage = new Rect(unit_w, unit_h * 3, unit_w * 7, unit_h * 12);
        rectBMSLogo = new Rect(unit_w, unit_h * 0.15f, unit_w * 5, unit_h * 1.7f);

        Vector2 labelSize = registerStyle.CalcSize(new GUIContent("Registration"));
        rectRegisterLabel = new Rect(unit_w * 9, unit_h * 2.5f, labelSize.x, labelSize.y);

        labelSize = labelStyle.CalcSize(new GUIContent("Username"));
        rectUsernameLabel = new Rect(unit_w * 9, unit_h * 4, labelSize.x, labelSize.y);
        rectUsernameField = new Rect(unit_w * 9, unit_h * 5, unit_w * 4, unit_h);

        labelSize = labelStyle.CalcSize(new GUIContent("Password"));
        rectPassLabel = new Rect(unit_w * 9, unit_h * 6 + 3, labelSize.x, labelSize.y);
        rectPassField = new Rect(unit_w * 9, unit_h * 7, unit_w * 4, unit_h);

        labelSize = labelStyle.CalcSize(new GUIContent("Re-Enter Password"));
        rectRePassLabel = new Rect(unit_w * 9, unit_h * 8 + 3, labelSize.x, labelSize.y);
        rectRePassField = new Rect(unit_w * 9, unit_h * 9, unit_w * 4, unit_h);

        labelSize = labelStyle.CalcSize(new GUIContent("Email"));
        rectEmailLabel = new Rect(unit_w * 9, unit_h * 10 + 3, labelSize.x, labelSize.y);
        rectEmailField = new Rect(unit_w * 9, unit_h * 11, unit_w * 4, unit_h);

        rectRegisterButton = new Rect(unit_w * 11.5f, unit_h * 12.5f, unit_w * 1.5f, unit_h);

        labelSize = labelStyle.CalcSize(new GUIContent(error));
        rectErrorLabel = new Rect(unit_w * 9, unit_h * 12.5f, labelSize.x, labelSize.y);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(rectStrip, stripColor);
        GUI.DrawTexture(rectHomeImage, homeImage);
        GUI.DrawTexture(rectBMSLogo, logoBMS);

        GUI.Label(rectRegisterLabel, "Registration", registerStyle);
        GUI.Label(rectUsernameLabel, "Username", labelStyle);
        GUI.Label(rectPassLabel, "Password", labelStyle);
        GUI.Label(rectRePassLabel, "Re-Enter Password", labelStyle);
        GUI.Label(rectEmailLabel, "Email", labelStyle);

        GUISkin curSkin = GUI.skin;
        GUI.skin = fieldSkin;
        username = GUI.TextField(rectUsernameField, username, FIELD_MAX);
        password = GUI.PasswordField(rectPassField, password, '*', FIELD_MAX);
        repassword = GUI.PasswordField(rectRePassField, repassword, '*', FIELD_MAX);
        email = GUI.TextField(rectEmailField, email, FIELD_MAX);
        GUI.skin = curSkin;

        GUI.DrawTexture(rectRegisterButton, registerButton);
        if (GUI.Button(rectRegisterButton, "", "Label"))
        {
            SceneController.Instance.Username = username;
            SceneController.Instance.Email = email;
            SceneController.Instance.Password = password;
            if (username != string.Empty &&
                email != string.Empty &&
                password != string.Empty &&
                password == repassword)
            {
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("email", email);
                PlayerPrefs.SetString("password", password);
                Application.LoadLevel("signon");
            }
            else
            {
                error = "Invalid Input";
            }
        }

        GUI.Label(rectErrorLabel, error, labelStyle);
    }

    #endregion
}
