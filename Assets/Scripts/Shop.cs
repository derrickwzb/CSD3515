using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int selected_id;
    public int cur_pg;
    public int item_in_pg;
    public int maxitem_per_pg;
    public int filter_pg; //0-all, 1-head, 2-body, 3-wrist, 4-food, 5-others

    public List<ItemInfo> items = new List<ItemInfo>();
    public List<ItemInfo> head = new List<ItemInfo>();
    public List<ItemInfo> body = new List<ItemInfo>();
    public List<ItemInfo> wrist = new List<ItemInfo>();
    public List<ItemInfo> food = new List<ItemInfo>();
    public List<ItemInfo> others = new List<ItemInfo>();

    void Awake()
    {
        Barrel barrel = new Barrel();
        Candy candy = new Candy();
        Chocolate chocolate = new Chocolate();
        ChestGuard chestguard = new ChestGuard();
        FancyOveralls fancyoveralls = new FancyOveralls();
        GolemRing golemring = new GolemRing();
        RaccoonCap racconcap = new RaccoonCap();
        QuiltedHood quiltedhood = new QuiltedHood();
        Wristband wristband = new Wristband();
        CupofWishes cupofwishes = new CupofWishes();
        FaerieWalnut faeriewalnut = new FaerieWalnut();
        MedicalHerb medicalHerb = new MedicalHerb();
        RoyalJam royalJam = new RoyalJam();

        //All
        items = new List<ItemInfo>();
        items.Add(new ItemInfo(barrel));
        items.Add(new ItemInfo(candy));
        items.Add(new ItemInfo(chocolate));
        items.Add(new ItemInfo(chestguard));
        items.Add(new ItemInfo(fancyoveralls));
        items.Add(new ItemInfo(golemring));
        items.Add(new ItemInfo(racconcap));
        items.Add(new ItemInfo(quiltedhood));
        items.Add(new ItemInfo(wristband));
        items.Add(new ItemInfo(cupofwishes));
        items.Add(new ItemInfo(faeriewalnut));
        items.Add(new ItemInfo(medicalHerb));
        items.Add(new ItemInfo(royalJam));

        //Head
        head = new List<ItemInfo>();
        head.Add(new ItemInfo(racconcap));
        head.Add(new ItemInfo(quiltedhood));

        //Body
        body.Add(new ItemInfo(chestguard));
        body.Add(new ItemInfo(fancyoveralls));

        //Wrist
        wrist.Add(new ItemInfo(golemring));
        wrist.Add(new ItemInfo(wristband));

        //food
        food.Add(new ItemInfo(candy));
        food.Add(new ItemInfo(chocolate));
        food.Add(new ItemInfo(faeriewalnut));
        food.Add(new ItemInfo(medicalHerb));
        food.Add(new ItemInfo(royalJam));

        //others
        others.Add(new ItemInfo(barrel));
        others.Add(new ItemInfo(cupofwishes));
    }

    // Start is called before the first frame update
    void Start()
    {
        selected_id = 0;
        cur_pg = 0;
        item_in_pg = 8;
        maxitem_per_pg = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class ItemInfo
{
    public Item item;

    public ItemInfo(Item _item)
    {
        item = _item;
    }
}
