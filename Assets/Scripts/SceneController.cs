using UnityEngine;
using System.Collections.Generic;

public class SceneController : MonoBehaviour 
{
    public static SceneController Instance;

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Item> ItemList;
    public List<Item> Favorites;

    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;
        DontDestroyOnLoad(this);

        Username = string.Empty;
        Email = string.Empty;

        ItemList = new List<Item>();
        Favorites = new List<Item>();
    }
}
