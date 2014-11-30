using UnityEngine;
using System.Collections.Generic;

public class ScreenEditDoubleBasic : MonoBehaviour
{
    #region Class Member Variables

    public GameObject TopView;
    public GameObject SideView;
    public GameObject InnerCamera;

    private const float DIVISOR = 16;

    private float unit_w;
	private float unit_h;
    private float max_w;
	
	private int greenScore;

    private Texture2D stripColor;
    private Texture2D logoBMS;
    private Texture2D signOutButton;
    private Texture2D backButton;
    private Texture2D topViewButton;
	private Texture2D sideViewButton;
	private Texture2D addButton;
	private Texture2D favoriteButton;
	private Texture2D background;

    private Rect rectStrip;
    private Rect rectBMSLogo;
    private Rect rectSignOut;
    private Rect rectLayoutLabel;
	private Rect rectItemLabel;
    private Rect rectBackButton;
    private Rect rectSideView;
	private Rect rectTopView;
	
	private Vector2 scrollPos = Vector2.zero;

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
		addButton = Resources.Load<Texture2D>("Textures/AddTexture");
		favoriteButton = Resources.Load<Texture2D>("Textures/FavoriteTexture");

        highlightStyle = new GUIStyle();
        highlightStyle.fontSize = 32;
        highlightStyle.fontStyle = FontStyle.Bold;
		highlightStyle.normal.textColor = Color.black;
		
		background = new Texture2D (1, 1);
		background.SetPixel (0, 0, Color.gray);
		background.Apply ();

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 19;
		labelStyle.normal.textColor = Color.black;
		
		greenScore = 0;
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

        Vector2 labelSize = highlightStyle.CalcSize(new GUIContent("Double Basic Layout (Green Score: XX)"));
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
            SceneController.Instance.BackStack.Push("doubleBasic");
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

        GUI.Label(rectLayoutLabel, "Double Basic Layout (Green Score: " + greenScore + ")", highlightStyle);
		GUI.Label(rectItemLabel, "Layout Items", highlightStyle);

        DrawItems();
    }

    #endregion

	private void DrawItems()
	{
		Rect rectItems = new Rect(unit_w * 9, unit_h * 4, unit_w * 6, unit_h * 11);
		Rect rectItemsInner = new Rect (0, 0, max_w, SceneController.Instance.ItemList.Count * unit_h);
		GUI.DrawTexture (rectItems, background);
		scrollPos = GUI.BeginScrollView(rectItems, scrollPos, rectItemsInner);
		
		float screenRatio = ((float) Screen.height) / ((float)Screen.width);
		
		float xBuffer = unit_w * 0.1f;
		float yBuffer = unit_h * 0.1f;
		float currentYPos = yBuffer;
		
		foreach (var item in SceneController.Instance.ItemList)
		{
			Vector2 size = labelStyle.CalcSize(new GUIContent(item.Key));
			
			Rect rectLogo = new Rect(xBuffer, currentYPos, unit_w * screenRatio, unit_h);
			GUI.DrawTexture(rectLogo, item.Value.Logo);
			
			Rect rectEfficiencyImage = new Rect(rectLogo.xMax + xBuffer, currentYPos, unit_w * screenRatio, unit_h);
			GUI.DrawTexture(rectEfficiencyImage, item.Value.EfficiencyImage);
			
			Rect rectAdd = new Rect(rectEfficiencyImage.xMax + xBuffer, currentYPos, unit_w, unit_h);
			Rect rectFavorite = new Rect(rectAdd.xMax + xBuffer, currentYPos, unit_w, unit_h);
			
			Rect rectLabel = new Rect(rectFavorite.xMax + xBuffer, currentYPos + (unit_h - size.y)/2.0f, size.x, unit_h);
			GUI.Label(rectLabel, item.Key, labelStyle);

			GUI.DrawTexture(rectAdd, addButton);
			if (GUI.Button(rectAdd, "", "Label"))
			{
				GameObject.Instantiate(Resources.Load<GameObject>(item.Value.Prefab), new Vector3(0, 2, 0), Quaternion.identity);
				if(item.Value.EfficiencyImage == Resources.Load<Texture2D>("Textures/efficient")) {
					greenScore += 5;
				} else greenScore -= 5;
			}
			
			GUI.DrawTexture(rectFavorite, favoriteButton);
			if (GUI.Button(rectFavorite, "", "Label"))
			{
				if (!SceneController.Instance.Favorites.Contains(item.Key))
				{
					SceneController.Instance.Favorites.Add(item.Key);
				}
			}
			
			currentYPos = currentYPos + unit_h + yBuffer;
            float current_w = rectLogo.width + rectEfficiencyImage.width + rectAdd.width + rectFavorite.width + rectLabel.width;
            if (current_w > max_w)
            {
                max_w = current_w;
            }
		}
		GUI.EndScrollView();
	}
}
