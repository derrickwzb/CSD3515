using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemname;
    public int quantity;
    public float cost;
    public string description;
    public string imgpath = "Art/";

    public bool restricted = false;
    public bool equipable = false;

    //0 - head, 1 - body, 2 - wrist, 3 - food, 4 - others
    public int type;
}

public class Equipable : Item
{
    // 0 - head, 1 - body, 2 - wrist
    public int equiptype = 0;

    // 0 - Randi, 1 - Purim, 2 - Popoi
    // 3 - Randi / Purim
    // 4 - Randi / Popoi
    // 5 - Purim / Popoi
    // 6 - everyone
    public int equipusers = -1;
}

public class Barrel : Item
{
    public Barrel()
    {
        itemname = "Barrel";
        quantity = 5;
        cost = 900;
        description = "A Barrel";
        imgpath += "Inventory/Items/Item_Barrel";
        equipable = false;
        type = 4;
    }
}

public class Candy : Item
{
    public Candy()
    {
        itemname = "Candy";
        quantity = 3;
        cost = 10;
        description = "A simple piece of candy, heals 5 hp";
        imgpath += "Inventory/Items/Item_Candy";
        equipable = false;
        type = 3;
    }
}

public class Chocolate : Item
{
    public Chocolate()
    {
        itemname = "Chocolate";
        quantity = 3;
        cost = 30;
        description = "A chocolate bar, heals 15 hp";
        imgpath += "Inventory/Items/Item_Chocolate";
        equipable = false;
        type = 3;
    }
}

public class ChestGuard : Equipable
{
    public ChestGuard()
    {
        itemname = "Chest Guard";
        quantity = 3;
        cost = 1000;
        description = "A chest piece to protect from damage";
        imgpath += "Inventory/Equip/Body/Equip_Body_ChestGuard";
        equipable = true;
        type = 1;

        equiptype = 1;
        equipusers = 5;
    }
}

public class FancyOveralls : Equipable
{
    public FancyOveralls()
    {
        itemname = "Fancy Overalls";
        quantity = 3;
        cost = 675;
        description = "An impressive piece of overalls";
        imgpath += "Inventory/Equip/Body/Equip_Body_FancyOveralls";
        equipable = true;
        type = 1;

        equiptype = 1;
        equipusers = 6;
    }
}

public class GolemRing : Equipable
{
    public GolemRing()
    {
        itemname = "Golem Ring";
        quantity = 3;
        cost = 750;
        description = "A ring dropped from a golem";
        imgpath += "Inventory/Equip/Wrist/Equip_Wrist_GolemRing";
        equipable = true;
        type = 2;

        equiptype = 2;
        equipusers = 6;
    }
}

public class RaccoonCap : Equipable
{
    public RaccoonCap()
    {
        itemname = "Raccoon Cap";
        quantity = 3;
        cost = 550;
        description = "A cap shapped like a raccoon";
        imgpath += "Inventory/Equip/Head/Equip_Head_RaccoonCap";
        equipable = true;
        type = 0;

        equiptype = 0;
        equipusers = 6;
    }
}

public class QuiltedHood : Equipable
{
    public QuiltedHood()
    {
        itemname = "Quilted Hood";
        quantity = 3;
        cost = 700;
        description = "A well hand made hood";
        imgpath += "Inventory/Equip/Head/Equip_Head_QuiltedHood";
        equipable = true;
        type = 0;

        equiptype = 0;
        equipusers = 5;
    }
}

public class Wristband : Equipable
{
    public Wristband()
    {
        itemname = "Wristband";
        quantity = 3;
        cost = 45;
        description = "A simple wrist band";
        imgpath += "Inventory/Equip/Wrist/Equip_Wrist_Wristband";
        equipable = true;
        type = 2;

        equiptype = 2;
        equipusers = 4;
    }
}

public class CupofWishes : Item
{
    public CupofWishes()
    {
        itemname = "Cup of Wishes";
        quantity = 2;
        cost = 150;
        description = "A cup full of wishes, use for a surprise";
        imgpath += "Inventory/Items/Item_CupOfWishes";
        equipable = false;
        type = 4;
    }
}

public class FaerieWalnut : Item
{
    public FaerieWalnut()
    {
        itemname = "Faerie Walnut";
        quantity = 1;
        cost = 1000;
        description = "A big faerie walnut, heals you to full health";
        imgpath += "Inventory/Items/Item_FaerieWalnut";
        equipable = false;
        type = 3;
    }
}

public class MedicalHerb : Item
{
    public MedicalHerb()
    {
        itemname = "Medical Herb";
        quantity = 1;
        cost = 20;
        description = "A healing herb, heals 10 hp";
        imgpath += "Inventory/Items/Item_MedicalHerb";
        equipable = false;
        type = 3;
    }
}

public class RoyalJam : Item
{
    public RoyalJam()
    {
        itemname = "Royal Jam";
        quantity = 1;
        cost = 350;
        description = "Quality Jam, increase move speed by 5 for 5 mins";
        imgpath += "Inventory/Items/Item_RoyalJam";
        equipable = false;
        type = 3;
    }
}