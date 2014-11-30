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
    private Texture2D backButton;
	private Texture2D background;

    private Rect rectStrip;
    private Rect rectBMSLogo;
    private Rect rectSignOut;
    private Rect rectUsernameLabel;
    private Rect rectAccountLabel;
    private Rect rectEmailLabel;
    private Rect rectFavoriteLabel;
    private Rect rectNoFavsLabel;
    private Rect rectLayoutLabel;
    private Rect rectBackButton;

    private Vector2 favoriteScrollPos = Vector2.zero;

    private GUIStyle labelStyle;
    private GUIStyle highlightStyle;
    private GUIStyle scrollStyle;

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
        backButton = Resources.Load<Texture2D>("Textures/BackTexture");

		background = new Texture2D (1, 1);
		background.SetPixel (0, 0, Color.gray);
		background.Apply ();

        highlightStyle = new GUIStyle();
        highlightStyle.fontSize = 32;
        highlightStyle.fontStyle = FontStyle.Bold;
        highlightStyle.normal.textColor = Color.black;

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 25;
        labelStyle.normal.textColor = Color.black;

        scrollStyle = new GUIStyle();
        scrollStyle.fontSize = 20;
        scrollStyle.normal.textColor = Color.black;

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
        rectBackButton = new Rect(Screen.width - unit_w * 4, unit_h * 0.5f, unit_w * 1.5f, unit_h);

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
        GUI.DrawTexture(rectBackButton, backButton);
        if (GUI.Button(rectBackButton, "", "Label"))
        {
            Application.LoadLevel(SceneController.Instance.BackStack.Pop());
        }

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

		// Dynamic buffers for spacing
		float xBuffer = unit_w * 0.15f;
		float yBuffer = unit_h * 0.15f;

		// x,y coords for favorites container
		float rectFavoritesXPos = rectFavoriteLabel.x;
		float rectFavoritesYPos = rectFavoriteLabel.y + rectFavoriteLabel.height;

		// x,y coords for favorites inner scroll view
		float rectFavoritesInnerXPos = rectFavoritesXPos + xBuffer * 2;
		float rectFavoritesInnerYPos = rectFavoritesYPos + yBuffer;
		
		float currentXPos = rectFavoritesInnerXPos + xBuffer;
		float currentYPos = rectFavoritesInnerYPos;

		// Buffered height/width for favorite item
		Vector2 favoriteStyle = scrollStyle.CalcSize (new GUIContent ("Efficient Floor Lamp"));
		float favoriteHeight = favoriteStyle.y + yBuffer;
		float favoriteWidth = favoriteStyle.x + (xBuffer * 2);
		
		float imageHeight = favoriteHeight - yBuffer;
		float imageWidth = imageHeight;
		
		float logoHeight = favoriteHeight - yBuffer;
		float logoWidth = logoHeight;

		float buttonHeight = favoriteHeight - yBuffer;
		float buttonWidth = buttonHeight;

		int numFavorites = SceneController.Instance.Favorites.Count;

		Rect rectFavoritesInner = new Rect (rectFavoritesInnerXPos, rectFavoritesInnerYPos, (imageWidth + xBuffer + logoWidth + xBuffer + favoriteWidth + xBuffer + ((buttonWidth + xBuffer) * 2) + (buttonWidth * 2 + xBuffer) + (buttonWidth * 2.2f + xBuffer)), (favoriteHeight + yBuffer) * numFavorites);
		Rect rectFavorites = new Rect (rectFavoritesXPos, rectFavoritesYPos, rectFavoritesInner.width, rectFavoritesInner.height + yBuffer);
        GUI.DrawTexture(new Rect(unit_w * 9, unit_h * 7, unit_w * 6, Mathf.Min(unit_h * 8, rectFavorites.height + 20)), background);

        favoriteScrollPos = GUI.BeginScrollView(new Rect(unit_w * 9, unit_h * 7, unit_w * 6, Mathf.Min(unit_h * 8 + 30, rectFavorites.height + 30)), favoriteScrollPos, rectFavoritesInner);

        foreach (var fav in SceneController.Instance.Favorites)
        {
			Rect rectPurchase;
			Rect rectDelete;
			Rect rectFacebook;
			Rect rectTwitter;
	
			GUI.DrawTexture(new Rect(currentXPos, currentYPos + yBuffer, logoWidth, logoHeight), SceneController.Instance.ItemList[fav].Logo);
			currentXPos = currentXPos + logoWidth + xBuffer;

			GUI.DrawTexture(new Rect(currentXPos, currentYPos + yBuffer, imageWidth, imageHeight), SceneController.Instance.ItemList[fav].EfficiencyImage);
			currentXPos = currentXPos + imageWidth + xBuffer;

			GUI.Label(new Rect(currentXPos, currentYPos + yBuffer, favoriteWidth, favoriteHeight), fav, scrollStyle);
			currentXPos = currentXPos + favoriteWidth;

			rectPurchase = new Rect(currentXPos, currentYPos + yBuffer, buttonWidth * 2.2f, buttonHeight);
            GUI.DrawTexture(rectPurchase, purchaseButton);
            if (GUI.Button(rectPurchase, "", "Label"))
            {
                Application.ExternalEval("window.open('" + SceneController.Instance.ItemList[fav].PurchaseURL + "','_blank')");
            }
			currentXPos = currentXPos + buttonWidth * 2.2f + xBuffer;

			rectDelete = new Rect(currentXPos, currentYPos + yBuffer, buttonWidth * 2, buttonHeight);
            GUI.DrawTexture(rectDelete, deleteButton);
            if (GUI.Button(rectDelete, "", "Label"))
            {
                removeQ.Enqueue(fav);
			}
			currentXPos = currentXPos + buttonWidth * 2 + xBuffer;

			rectFacebook = new Rect(currentXPos, currentYPos + yBuffer, buttonWidth, buttonHeight);
            GUI.DrawTexture(rectFacebook, facebookButton);
            if (GUI.Button(rectFacebook, "", "Label"))
            {
                Application.ExternalEval("window.open('https://www.facebook.com/','_blank')");
			}
			currentXPos = currentXPos + buttonWidth + xBuffer;	

			rectTwitter = new Rect(currentXPos, currentYPos + yBuffer, buttonWidth, buttonHeight);
            GUI.DrawTexture(rectTwitter, twitterButton);
            if (GUI.Button(rectTwitter, "", "Label"))
            {
                Application.ExternalEval("window.open('https://www.twitter.com/','_blank')");
			}

			currentYPos = currentYPos + favoriteHeight + yBuffer;
			currentXPos = rectFavoritesInnerXPos + xBuffer;
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
            SceneController.Instance.BackStack.Push("account");
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
            SceneController.Instance.BackStack.Push("account");
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
            SceneController.Instance.BackStack.Push("account");
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
