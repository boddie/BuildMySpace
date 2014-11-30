using UnityEngine;
using System.Collections.Generic;

public class ScreenAccount : MonoBehaviour
{
    #region Class Member Variables

    private const float DIVISOR = 16;

    private float unit_w;
    private float unit_h;

    private Texture2D stripColor;
    private Texture2D logoBMS;
    private Texture2D signOutButton;
    private Texture2D purchaseButton;
    private Texture2D facebookButton;
    private Texture2D twitterButton;
    private Texture2D deleteButton;
    private Texture2D singleBasic;
    private Texture2D doubleBasic;
    private Texture2D singleBath;
    private Texture2D editButton;

    private Rect rectStrip;
    private Rect rectBMSLogo;
    private Rect rectSignOut;
    private Rect rectUsernameLabel;
    private Rect rectAccountLabel;
    private Rect rectEmailLabel;
    private Rect rectFavoriteLabel;
    private Rect rectNoFavsLabel;
    private Rect rectLayoutLabel;

    private Vector2 favoriteScrollPos = Vector2.zero;

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
        purchaseButton = Resources.Load<Texture2D>("Textures/PurchaseTexture");
        facebookButton = Resources.Load<Texture2D>("Textures/facebook");
        twitterButton = Resources.Load<Texture2D>("Textures/twitter");
        deleteButton = Resources.Load<Texture2D>("Textures/DeleteTexture");
        singleBasic = Resources.Load<Texture2D>("Textures/RoomSingleBasicTexture");
        doubleBasic = Resources.Load<Texture2D>("Textures/RoomDualBasicTexture");
        singleBath = Resources.Load<Texture2D>("Textures/RoomSingleBathTexture");
        editButton = Resources.Load<Texture2D>("Textures/EditTexture");

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

        labelSize = labelStyle.CalcSize(new GUIContent("No favorited items"));
        rectNoFavsLabel = new Rect(unit_w * 9, unit_h * 7, labelSize.x, labelSize.y);

        labelSize = highlightStyle.CalcSize(new GUIContent("Layouts"));
        rectLayoutLabel = new Rect(unit_w, unit_h * 2.5f, labelSize.x, labelSize.y);
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

        if (SceneController.Instance.Favorites.Count == 0)
        {
            GUI.Label(rectNoFavsLabel, "No favorited items", labelStyle);
        }
        else
        {
            DrawFavorites();
        }

        DrawLayouts();
    }

    #endregion

    private void DrawFavorites()
    {
        Queue<string> removeQ = new Queue<string>();

        favoriteScrollPos = GUI.BeginScrollView(new Rect(0, 0, 0, 0), favoriteScrollPos, new Rect(0, 0, 0, 0));
        foreach (var fav in SceneController.Instance.Favorites)
        {
            Rect rectPurchase = new Rect(0, 0, 0, 0);
            Rect rectDelete = new Rect(0, 0, 0, 0);
            Rect rectFacebook = new Rect(0, 0, 0, 0);
            Rect rectTwitter = new Rect(0, 0, 0, 0);

            Vector2 size = labelStyle.CalcSize(new GUIContent(fav));
            GUI.Label(new Rect(0, 0, size.x, size.y), fav, labelStyle);
            GUI.DrawTexture(new Rect(0, 0, 0, 0), SceneController.Instance.ItemList[fav].Logo);
            GUI.DrawTexture(new Rect(0, 0, 0, 0), SceneController.Instance.ItemList[fav].EfficiencyImage);

            GUI.DrawTexture(rectPurchase, purchaseButton);
            if (GUI.Button(rectPurchase, "", "Label"))
            {
                Application.ExternalEval("window.open('" + SceneController.Instance.ItemList[fav].PurchaseURL + "','_blank')");
            }

            GUI.DrawTexture(rectDelete, deleteButton);
            if (GUI.Button(rectDelete, "", "Label"))
            {
                removeQ.Enqueue(fav);
            }

            GUI.DrawTexture(rectFacebook, facebookButton);
            if (GUI.Button(rectFacebook, "", "Label"))
            {
                Application.ExternalEval("window.open('https://www.facebook.com/','_blank')");
            }

            GUI.DrawTexture(rectTwitter, twitterButton);
            if (GUI.Button(rectTwitter, "", "Label"))
            {
                Application.ExternalEval("window.open('https://www.twitter.com/','_blank')");
            }
        }
        GUI.EndScrollView();

        while (removeQ.Count > 0)
        {
            SceneController.Instance.Favorites.Remove(removeQ.Dequeue());
        }
    }

    private void DrawLayouts()
    {
        GUI.Label(rectLayoutLabel, "Layouts", highlightStyle);

        GUI.DrawTexture(new Rect(0, 0, 0, 0), singleBasic);
        Vector2 labelSize = labelStyle.CalcSize(new GUIContent("Single Basic"));
        GUI.Label(new Rect(0, 0, labelSize.x, labelSize.y), "Single Basic", labelStyle);
        Rect editRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(editRect, editButton);
        if (GUI.Button(editRect, "", "Label"))
        {

        }
        Rect facebookRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(facebookRect, facebookButton);
        if (GUI.Button(facebookRect, "", "Label"))
        {

        }
        Rect twitterRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(twitterRect, twitterButton);
        if (GUI.Button(twitterRect, "", "Label"))
        {

        }

        GUI.DrawTexture(new Rect(0, 0, 0, 0), singleBasic);
        labelSize = labelStyle.CalcSize(new GUIContent("Single Basic"));
        GUI.Label(new Rect(0, 0, labelSize.x, labelSize.y), "Single Basic", labelStyle);
        editRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(editRect, editButton);
        if (GUI.Button(editRect, "", "Label"))
        {

        }
        facebookRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(facebookRect, facebookButton);
        if (GUI.Button(facebookRect, "", "Label"))
        {

        }
        twitterRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(twitterRect, twitterButton);
        if (GUI.Button(twitterRect, "", "Label"))
        {

        }

        GUI.DrawTexture(new Rect(0, 0, 0, 0), singleBasic);
        labelSize = labelStyle.CalcSize(new GUIContent("Single Basic"));
        GUI.Label(new Rect(0, 0, labelSize.x, labelSize.y), "Single Basic", labelStyle);
        editRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(editRect, editButton);
        if (GUI.Button(editRect, "", "Label"))
        {

        }
        facebookRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(facebookRect, facebookButton);
        if (GUI.Button(facebookRect, "", "Label"))
        {

        }
        twitterRect = new Rect(0, 0, 0, 0);
        GUI.DrawTexture(twitterRect, twitterButton);
        if (GUI.Button(twitterRect, "", "Label"))
        {

        }
    }
}
