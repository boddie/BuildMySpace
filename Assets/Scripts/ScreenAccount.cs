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

        // Single basic
        GUI.DrawTexture(new Rect(unit_w, unit_h * 4, unit_w * 2, unit_h * 3), singleBasic);
        Vector2 labelSize = labelStyle.CalcSize(new GUIContent("Single Basic"));
        GUI.Label(new Rect(unit_w * 3.5f, unit_h * 4, labelSize.x, labelSize.y), "Single Basic", labelStyle);
        Rect editRect = new Rect(unit_w * 3.5f, unit_h * 5.5f, unit_w * 1f, unit_h);
        GUI.DrawTexture(editRect, editButton);
        if (GUI.Button(editRect, "", "Label"))
        {
            Application.LoadLevel("singleBasic");
        }
        Rect facebookRect = new Rect(unit_w * 5, unit_h * 5.5f, unit_w * 0.5f, unit_h);
        GUI.DrawTexture(facebookRect, facebookButton);
        if (GUI.Button(facebookRect, "", "Label"))
        {
            Application.ExternalEval("window.open('https://www.facebook.com/','_blank')");
        }
        Rect twitterRect = new Rect(unit_w * 5.6f, unit_h * 5.5f, unit_w * 0.5f, unit_h);
        GUI.DrawTexture(twitterRect, twitterButton);
        if (GUI.Button(twitterRect, "", "Label"))
        {
            Application.ExternalEval("window.open('https://www.twitter.com/','_blank')");
        }

        // Dual basic
        GUI.DrawTexture(new Rect(unit_w, unit_h * 8, unit_w * 2, unit_h * 3), doubleBasic);
        labelSize = labelStyle.CalcSize(new GUIContent("Double Basic"));
        GUI.Label(new Rect(unit_w * 3.5f, unit_h * 8, labelSize.x, labelSize.y), "Double Basic", labelStyle);
        editRect = new Rect(unit_w * 3.5f, unit_h * 9.5f, unit_w * 1f, unit_h);
        GUI.DrawTexture(editRect, editButton);
        if (GUI.Button(editRect, "", "Label"))
        {
            Application.LoadLevel("doubleBasic");
        }
        facebookRect = new Rect(unit_w * 5, unit_h * 9.5f, unit_w * 0.5f, unit_h);
        GUI.DrawTexture(facebookRect, facebookButton);
        if (GUI.Button(facebookRect, "", "Label"))
        {
            Application.ExternalEval("window.open('https://www.facebook.com/','_blank')");
        }
        twitterRect = new Rect(unit_w * 5.6f, unit_h * 9.5f, unit_w * 0.5f, unit_h);
        GUI.DrawTexture(twitterRect, twitterButton);
        if (GUI.Button(twitterRect, "", "Label"))
        {
            Application.ExternalEval("window.open('https://www.twitter.com/','_blank')");
        }

        // Single bath
        GUI.DrawTexture(new Rect(unit_w, unit_h * 12, unit_w * 2, unit_h * 3), singleBath);
        labelSize = labelStyle.CalcSize(new GUIContent("Single Bath"));
        GUI.Label(new Rect(unit_w * 3.5f, unit_h * 12, labelSize.x, labelSize.y), "Single Bath", labelStyle);
        editRect = new Rect(unit_w * 3.5f, unit_h * 13.5f, unit_w * 1f, unit_h);
        GUI.DrawTexture(editRect, editButton);
        if (GUI.Button(editRect, "", "Label"))
        {
            Application.LoadLevel("singleBath");
        }
        facebookRect = new Rect(unit_w * 5, unit_h * 13.5f, unit_w * 0.5f, unit_h);
        GUI.DrawTexture(facebookRect, facebookButton);
        if (GUI.Button(facebookRect, "", "Label"))
        {
            Application.ExternalEval("window.open('https://www.facebook.com/','_blank')");
        }
        twitterRect = new Rect(unit_w * 5.6f, unit_h * 13.5f, unit_w * 0.5f, unit_h);
        GUI.DrawTexture(twitterRect, twitterButton);
        if (GUI.Button(twitterRect, "", "Label"))
        {
            Application.ExternalEval("window.open('https://www.twitter.com/','_blank')");
        }
    }
}
