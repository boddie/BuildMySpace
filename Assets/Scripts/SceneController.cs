using UnityEngine;
using System.Collections.Generic;

public class SceneController : MonoBehaviour 
{
    public static SceneController Instance;

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int SingleBasicScore { get; set; }
    public int SingleBathScore { get; set; }
    public int DoubleBasicScore { get; set; }
    public Dictionary<string, Item> ItemList;
    public List<string> Favorites;

    public Stack<string> BackStack;

    private void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;
        DontDestroyOnLoad(this);

        if (PlayerPrefs.HasKey("username"))
        {
            Username = PlayerPrefs.GetString("username");
            Email = PlayerPrefs.GetString("email");
            Password = PlayerPrefs.GetString("password");
        }
        else
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

        BackStack = new Stack<string>();
        ItemList = new Dictionary<string, Item>();
        Favorites = new List<string>();
        AddItems();

        SingleBasicScore = 0;
        SingleBathScore = 0;
        DoubleBasicScore = 0;
    }

    private void AddItems()
    {
        ItemList.Add("Efficient Desk Lamp", new Item(
            Resources.Load<Texture2D>("Textures/DeskLampTexture"),
            Resources.Load<Texture2D>("Textures/efficient"),
            "Prefabs/EfficientDeskLamp",
            "http://www.amazon.com/Trademark-Home-72-SL137B-Solar-Powered/dp/B008BS7EAW/ref=sr_1_7?ie=UTF8&qid=1417319509&sr=8-7&keywords=power+saver+desk+lamp"));
        ItemList.Add("Inefficient Desk Lamp", new Item(
            Resources.Load<Texture2D>("Textures/DeskLampTexture"),
            Resources.Load<Texture2D>("Textures/inefficient"),
            "Prefabs/InefficientDeskLamp",
            "http://www.amazon.com/LIGHTING-BO-2298TB-150-watt-Armour-Metal/dp/B008BHFKMC/ref=sr_1_9?ie=UTF8&qid=1417319617&sr=8-9&keywords=150+watt+desk+lamp"));
        ItemList.Add("Efficient Floor Lamp", new Item(
            Resources.Load<Texture2D>("Textures/FloorLampTexture"),
            Resources.Load<Texture2D>("Textures/efficient"),
            "Prefabs/EfficientFloorLamp",
            "http://www.amazon.com/Holtkoetter-Bernie-Series-Low-Voltage-Single/dp/B001U3OO6C/ref=sr_1_32?ie=UTF8&qid=1417319750&sr=8-32&keywords=energy+saver+floor+lamp"));
        ItemList.Add("Inefficient Floor Lamp", new Item(
            Resources.Load<Texture2D>("Textures/FloorLampTexture"),
            Resources.Load<Texture2D>("Textures/inefficient"),
            "Prefabs/InefficientFloorLamp",
            "http://www.amazon.com/Normande-Lighting-Incandescent-Torchiere-Brushed/dp/B005MYXKG0/ref=sr_1_1?ie=UTF8&qid=1417319800&sr=8-1&keywords=150w+floor+lamp"));
        ItemList.Add("Efficient Laptop", new Item(
            Resources.Load<Texture2D>("Textures/LaptopTexture"),
            Resources.Load<Texture2D>("Textures/efficient"),
            "Prefabs/EfficientLaptop",
            "http://www.amazon.com/HP-14-q070nr-14-Inch-Chromebook-T-Mobile/dp/B00FGOTBQO/ref=sr_1_6?s=pc&ie=UTF8&qid=1417319921&sr=1-6&keywords=low+energy+laptop"));
        ItemList.Add("Inefficient Laptop", new Item(
            Resources.Load<Texture2D>("Textures/LaptopTexture"),
            Resources.Load<Texture2D>("Textures/inefficient"),
            "Prefabs/InefficientLaptop",
            "http://www.amazon.com/Alienware-Overclocked-4900MQ-1600MHz-Windows/dp/B00GI2W476/ref=sr_1_1?s=electronics&ie=UTF8&qid=1417320146&sr=1-1&keywords=sli+alienware"));
        ItemList.Add("Efficient Printer", new Item(
            Resources.Load<Texture2D>("Textures/PrinterTexture"),
            Resources.Load<Texture2D>("Textures/efficient"),
            "Prefabs/EfficientPrinter",
            "http://www.amazon.com/HP-OfficeJet-Wireless-A7F65A-B1H/dp/B00J8NBVYE/ref=sr_1_1?s=electronics&ie=UTF8&qid=1417320216&sr=1-1&keywords=energy+star+printer"));
        ItemList.Add("Inefficient Printer", new Item(
            Resources.Load<Texture2D>("Textures/PrinterTexture"),
            Resources.Load<Texture2D>("Textures/inefficient"),
            "Prefabs/InefficientPrinter",
            "http://www.amazon.com/HP-LaserJet-M276nw-Color-Printer/dp/B008ABLJC4/ref=sr_1_1?s=electronics&ie=UTF8&qid=1417320327&sr=1-1&keywords=200w+printer"));
    }
}
