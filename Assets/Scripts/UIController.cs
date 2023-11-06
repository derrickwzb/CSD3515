using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine;

public class UIController : MonoBehaviour
{
    Vector2 inputdir;

    [SerializeField]
    GameObject menuobj;
    Menu menu;

    [SerializeField]
    GameObject uidocObj;
    UIDocument uidoc;

    //Visual
    VisualElement root;
    VisualElement usage;
    VisualElement shopfilterheader;
    VisualElement confirmbox;

    //Confirm
    bool confirm;

    //Gold
    Label goldlabel;

    void Start()
    {
        menu = menuobj.GetComponent<Menu>();
        uidoc = uidocObj.GetComponent<UIDocument>();
        root = uidoc.rootVisualElement;
        usage = root.Q<VisualElement>("Party");
        shopfilterheader = root.Q<VisualElement>("TabsContainer");
        confirmbox = root.Q<VisualElement>("ConfirmBox");
        confirm = true;

        //set gold
        goldlabel = root.Q<Label>("Gold");
        goldlabel.text = menu.inv.gold.ToString();

        //Set up
        root.Q<VisualElement>("InteractBox").style.visibility = Visibility.Visible;
        root.Q<VisualElement>("TopDesk").style.visibility = Visibility.Hidden;
        root.Q<VisualElement>("Main").style.visibility = Visibility.Hidden;
        root.Q<VisualElement>("Bot").style.visibility = Visibility.Hidden;
        root.Q<VisualElement>("Confirmation").style.visibility = Visibility.Hidden;
        root.Q<VisualElement>("ConfirmationNotEnough").style.visibility = Visibility.Hidden;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisplayShopItems()
    {
        //Filters
        SetFilter();

        //Arrows
        string uparrow = "UpArrow";
        string downarrow = "DownArrow";
        VisualElement uarrow = root.Q<VisualElement>(uparrow);
        VisualElement darrow = root.Q<VisualElement>(downarrow);

        if (menu.shop.cur_pg == 0)
        {
            uarrow.style.visibility = Visibility.Hidden;
        }
        else
        {
            uarrow.style.visibility = Visibility.Visible;
        }

        if (menu.shop.cur_pg == 1)
        {
            darrow.style.visibility = Visibility.Hidden;
        }
        else
        {
            darrow.style.visibility = Visibility.Visible;
        }

        List<ItemInfo> list;
        if(menu.shop.filter_pg == 0)
        {
            //menu.shop.item_in_pg = 8;
            list = menu.shop.items;
        }
        else if (menu.shop.filter_pg == 1)
        {
            //menu.shop.item_in_pg = menu.shop.head.Count;
            list = menu.shop.head;

            uarrow.style.visibility = Visibility.Hidden;
            darrow.style.visibility = Visibility.Hidden;
        }
        else if (menu.shop.filter_pg == 2)
        {
            //menu.shop.item_in_pg = menu.shop.body.Count;
            list = menu.shop.body;

            uarrow.style.visibility = Visibility.Hidden;
            darrow.style.visibility = Visibility.Hidden;
        }
        else if (menu.shop.filter_pg == 3)
        {
            //menu.shop.item_in_pg = menu.shop.wrist.Count;
            list = menu.shop.wrist;

            uarrow.style.visibility = Visibility.Hidden;
            darrow.style.visibility = Visibility.Hidden;
        }
        else if (menu.shop.filter_pg == 4)
        {
            //menu.shop.item_in_pg = menu.shop.food.Count;
            list = menu.shop.food;

            uarrow.style.visibility = Visibility.Hidden;
            darrow.style.visibility = Visibility.Hidden;
        }
        else //if (menu.shop.filter_pg == 5)
        {
            //menu.shop.item_in_pg = menu.shop.others.Count;
            list = menu.shop.others;

            uarrow.style.visibility = Visibility.Hidden;
            darrow.style.visibility = Visibility.Hidden;
        }

        //Items
        for (int id = 0; id < menu.shop.item_in_pg; ++id)
        {
            string name = "Item";
            name += id.ToString();

            VisualElement tmpRoot = root.Q<VisualElement>(name);

            if (id + menu.shop.cur_pg * menu.shop.maxitem_per_pg < list.Count)
            {
                tmpRoot.style.visibility = Visibility.Visible;

                tmpRoot.Q<Label>("name").text = list[id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.itemname;
                int amt = list[id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.quantity;
                tmpRoot.Q<Label>("Quantity").text = amt.ToString();
                tmpRoot.Q<Label>("Value").text = list[id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.cost.ToString();
                Utils.ChangeImage(tmpRoot.Q<VisualElement>("icon"), list[id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.imgpath);

                if (amt != 0)
                    tmpRoot.Q<VisualElement>("SoldOut").style.visibility = Visibility.Hidden;
                else
                    tmpRoot.Q<VisualElement>("SoldOut").style.visibility = Visibility.Visible;
            }
            else
            {
                tmpRoot.style.visibility = Visibility.Hidden;
                tmpRoot.Q<VisualElement>("SoldOut").style.visibility = StyleKeyword.Null;
            }

            // Display highlight at first selected item
            if (id == menu.shop.selected_id)
            {
                tmpRoot.style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
                SetDescription();
                SetDisplayUsage();
            }
            else
            {
                tmpRoot.style.backgroundColor = new Color(217f / 255f, 217f / 255f, 217f / 255f, 255f);
            }
        }
    }

    private void SetFilter()
    {
        if(menu.shop.filter_pg == 0)
        {
            shopfilterheader.Q<VisualElement>("All").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
            shopfilterheader.Q<VisualElement>("Head").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Body").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Wrist").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Food").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Others").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
        }
        else if (menu.shop.filter_pg == 1)
        {
            shopfilterheader.Q<VisualElement>("All").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Head").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
            shopfilterheader.Q<VisualElement>("Body").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Wrist").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Food").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Others").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
        }
        else if (menu.shop.filter_pg == 2)
        {
            shopfilterheader.Q<VisualElement>("All").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Head").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Body").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
            shopfilterheader.Q<VisualElement>("Wrist").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Food").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Others").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
        }
        else if (menu.shop.filter_pg == 3)
        {
            shopfilterheader.Q<VisualElement>("All").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Head").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Body").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Wrist").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
            shopfilterheader.Q<VisualElement>("Food").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Others").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
        }
        else if (menu.shop.filter_pg == 4)
        {
            shopfilterheader.Q<VisualElement>("All").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Head").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Body").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Wrist").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Food").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
            shopfilterheader.Q<VisualElement>("Others").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
        }
        else if (menu.shop.filter_pg == 5)
        {
            shopfilterheader.Q<VisualElement>("All").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Head").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Body").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Wrist").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Food").style.backgroundColor = new Color(255f/ 255f, 255f/ 255f, 255f/ 255f, 255f);
            shopfilterheader.Q<VisualElement>("Others").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
        }
    }

    private void SetDisplayUsage()
    {
        List<ItemInfo> list;
        if (menu.shop.filter_pg == 0)
        {
            //menu.shop.item_in_pg = 8;
            list = menu.shop.items;
        }
        else if (menu.shop.filter_pg == 1)
        {
            //menu.shop.item_in_pg = menu.shop.head.Count;
            list = menu.shop.head;
        }
        else if (menu.shop.filter_pg == 2)
        {
            //menu.shop.item_in_pg = menu.shop.body.Count;
            list = menu.shop.body;
        }
        else if (menu.shop.filter_pg == 3)
        {
            //menu.shop.item_in_pg = menu.shop.wrist.Count;
            list = menu.shop.wrist;
        }
        else if (menu.shop.filter_pg == 4)
        {
            //menu.shop.item_in_pg = menu.shop.food.Count;
            list = menu.shop.food;
        }
        else //if (menu.shop.filter_pg == 5)
        {
            //menu.shop.item_in_pg = menu.shop.others.Count;
            list = menu.shop.others;
        }

        ItemInfo iteminfo = list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg];

        if (iteminfo.item.equipable)
        {
            Equipable equip = (Equipable)list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item;
            
            if (equip.equipusers == 0)
            {
                usage.Q<VisualElement>("Users0").style.visibility = Visibility.Visible;
                usage.Q<VisualElement>("Users1").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users2").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users3").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users4").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users5").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users6").style.visibility = Visibility.Hidden;
            }
            else if (equip.equipusers == 1)
            {
                usage.Q<VisualElement>("Users0").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users1").style.visibility = Visibility.Visible;
                usage.Q<VisualElement>("Users2").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users3").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users4").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users5").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users6").style.visibility = Visibility.Hidden;
            }
            else if (equip.equipusers == 2)
            {
                usage.Q<VisualElement>("Users0").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users1").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users2").style.visibility = Visibility.Visible;
                usage.Q<VisualElement>("Users3").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users4").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users5").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users6").style.visibility = Visibility.Hidden;
            }
            else if (equip.equipusers == 3)
            {
                usage.Q<VisualElement>("Users0").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users1").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users2").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users3").style.visibility = Visibility.Visible;
                usage.Q<VisualElement>("Users4").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users5").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users6").style.visibility = Visibility.Hidden;
            }
            else if (equip.equipusers == 4)
            {
                usage.Q<VisualElement>("Users0").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users1").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users2").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users3").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users4").style.visibility = Visibility.Visible;
                usage.Q<VisualElement>("Users5").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users6").style.visibility = Visibility.Hidden;
            }
            else if (equip.equipusers == 5)
            {
                usage.Q<VisualElement>("Users0").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users1").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users2").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users3").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users4").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users5").style.visibility = Visibility.Visible;
                usage.Q<VisualElement>("Users6").style.visibility = Visibility.Hidden;
            }
            else if (equip.equipusers == 6)
            {
                usage.Q<VisualElement>("Users0").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users1").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users2").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users3").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users4").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users5").style.visibility = Visibility.Hidden;
                usage.Q<VisualElement>("Users6").style.visibility = Visibility.Visible;
            }
        }
        else
        {
            usage.Q<VisualElement>("Users0").style.visibility = Visibility.Hidden;
            usage.Q<VisualElement>("Users1").style.visibility = Visibility.Hidden;
            usage.Q<VisualElement>("Users2").style.visibility = Visibility.Hidden;
            usage.Q<VisualElement>("Users3").style.visibility = Visibility.Hidden;
            usage.Q<VisualElement>("Users4").style.visibility = Visibility.Hidden;
            usage.Q<VisualElement>("Users5").style.visibility = Visibility.Hidden;
            usage.Q<VisualElement>("Users6").style.visibility = Visibility.Visible;
        }
    }

    List<ItemInfo> GetCurlist()
    {
        List<ItemInfo> list;
        if (menu.shop.filter_pg == 0)
        {
            //menu.shop.item_in_pg = 8;
            list = menu.shop.items;
        }
        else if (menu.shop.filter_pg == 1)
        {
            //menu.shop.item_in_pg = menu.shop.head.Count;
            list = menu.shop.head;
        }
        else if (menu.shop.filter_pg == 2)
        {
            //menu.shop.item_in_pg = menu.shop.body.Count;
            list = menu.shop.body;
        }
        else if (menu.shop.filter_pg == 3)
        {
            //menu.shop.item_in_pg = menu.shop.wrist.Count;
            list = menu.shop.wrist;
        }
        else if (menu.shop.filter_pg == 4)
        {
            //menu.shop.item_in_pg = menu.shop.food.Count;
            list = menu.shop.food;
        }
        else //if (menu.shop.filter_pg == 5)
        {
            //menu.shop.item_in_pg = menu.shop.others.Count;
            list = menu.shop.others;
        }

        return list;
    }

    private void SetDescription()
    {
        List<ItemInfo> list = GetCurlist();

        root.Q<Label>("ItemDescription").text = list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.description;
    }

    private void OnDpadNav(InputValue value)
    { 
        bool up = false;
        bool down = false;
        bool left = false;
        bool right = false;

        inputdir = value.Get<Vector2>();

        if (inputdir == Vector2.up)
        {
            Debug.Log("Up pressed");
            up = true;
        }
        else if (inputdir == Vector2.down)
        {
            Debug.Log("Down pressed");
            down = true;
        }
        else if (inputdir == Vector2.left)
        {
            Debug.Log("Left pressed");
            left = true;
        }
        else if (inputdir == Vector2.right)
        {
            Debug.Log("Right pressed");
            right = true;
        }

        if (menu.menustate == Menu.state.SHOP)
        {
            if (down && menu.shop.selected_id < 7)
            {
                string name = "Item";
                name += menu.shop.selected_id.ToString();
                VisualElement tmpRoot = root.Q<VisualElement>(name);
                
                menu.shop.selected_id++;
                
                string name2 = "Item";
                name2 += menu.shop.selected_id.ToString();
                VisualElement tmpRoot2 = root.Q<VisualElement>(name2);

                //Check if able to go next
                if (tmpRoot2.style.visibility == Visibility.Hidden)
                {
                    menu.shop.selected_id--;
                    return;
                }

                //Unhighlight current
                tmpRoot.style.backgroundColor = new Color(217f / 255f, 217f / 255f, 217f / 255f, 255f);

                //Highlight next                
                tmpRoot2.style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);

                //Change Description
                SetDescription();

                //Set Usage
                SetDisplayUsage();
            }
            else if (down && menu.shop.cur_pg == 0 && menu.shop.selected_id == 7)
            {
                menu.shop.selected_id = 0;
                menu.shop.cur_pg++;

                DisplayShopItems();
            }

            if (up && menu.shop.selected_id > 0)
            {
                //Clear cur selected
                string name = "Item";
                name += menu.shop.selected_id.ToString();
                VisualElement tmpRoot = root.Q<VisualElement>(name);
                tmpRoot.style.backgroundColor = new Color(217f / 255f, 217f / 255f, 217f / 255f, 255f);

                menu.shop.selected_id--;

                //Highlight next
                name = "Item";
                name += menu.shop.selected_id.ToString();
                tmpRoot = root.Q<VisualElement>(name);
                tmpRoot.style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);

                //Change Description
                SetDescription();

                //Set Usage
                SetDisplayUsage();
            }
            else if (up && menu.shop.cur_pg != 0 && menu.shop.selected_id == 0)
            {
                menu.shop.selected_id = 7;
                menu.shop.cur_pg--;

                DisplayShopItems();
            }
        }
        else if (menu.menustate == Menu.state.SHOPConfirmation)
        {
            if (up)
            {
                int quantity = int.Parse(confirmbox.Q<Label>("Quantity").text);
                List<ItemInfo> list = GetCurlist();

                if (list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.quantity <= quantity)
                    return;

                quantity++;
                confirmbox.Q<Label>("Quantity").text = quantity.ToString();

                //Set arrows display
                confirmbox.Q<VisualElement>("q_down").style.visibility = Visibility.Visible;
                if (list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.quantity > quantity)
                    confirmbox.Q<VisualElement>("q_up").style.visibility = Visibility.Visible;
                else
                    confirmbox.Q<VisualElement>("q_up").style.visibility = Visibility.Hidden;
            }
            else if (down)
            {
                int quantity = int.Parse(confirmbox.Q<Label>("Quantity").text);
                List<ItemInfo> list = GetCurlist();

                if (quantity == 1)
                    return;

                quantity--;
                confirmbox.Q<Label>("Quantity").text = quantity.ToString();

                //Set arrows display
                confirmbox.Q<VisualElement>("q_up").style.visibility = Visibility.Visible;
                if (quantity > 1)
                    confirmbox.Q<VisualElement>("q_down").style.visibility = Visibility.Visible;
                else
                    confirmbox.Q<VisualElement>("q_down").style.visibility = Visibility.Hidden;
            }

            if (left)
            {
                confirm = false;
                confirmbox.Q<VisualElement>("Confirm").style.backgroundColor = new Color(217f / 255f, 217f / 255f, 217f / 255f, 255f);
                confirmbox.Q<VisualElement>("Cancel").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
            }
            else if (right)
            {
                confirm = true;
                confirmbox.Q<VisualElement>("Confirm").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
                confirmbox.Q<VisualElement>("Cancel").style.backgroundColor = new Color(217f / 255f, 217f / 255f, 217f / 255f, 255f);
            }
        }
    }

    private void OnButtonA()
    {
        Debug.Log("A pressed");

        if (menu.menustate == Menu.state.SHOP)
        {
            menu.menustate = Menu.state.OUT;

            root.Q<VisualElement>("InteractBox").style.visibility = Visibility.Visible;
            root.Q<VisualElement>("TopDesk").style.visibility = Visibility.Hidden;
            root.Q<VisualElement>("Main").style.visibility = Visibility.Hidden;
            root.Q<VisualElement>("Bot").style.visibility = Visibility.Hidden;

            menu.shop.cur_pg = 0;
            menu.shop.filter_pg = 0;

            //Reset Items
            for (int id = 0; id < menu.shop.item_in_pg; ++id)
            {
                string name = "Item";
                name += id.ToString();

                VisualElement tmpRoot = root.Q<VisualElement>(name);

                if (id + menu.shop.cur_pg * menu.shop.maxitem_per_pg < menu.shop.items.Count)
                {
                    tmpRoot.style.visibility = StyleKeyword.Null;
                    tmpRoot.Q<VisualElement>("SoldOut").style.visibility = StyleKeyword.Null;
                }
            }

            //Reset arrows
            string uparrow = "UpArrow";
            string downarrow = "DownArrow";
            VisualElement uarrow = root.Q<VisualElement>(uparrow);
            VisualElement darrow = root.Q<VisualElement>(downarrow);
            uarrow.style.visibility = StyleKeyword.Null;
            darrow.style.visibility = StyleKeyword.Null;

            //Reset usage
            usage.Q<VisualElement>("Users0").style.visibility = StyleKeyword.Null;
            usage.Q<VisualElement>("Users1").style.visibility = StyleKeyword.Null;
            usage.Q<VisualElement>("Users2").style.visibility = StyleKeyword.Null;
            usage.Q<VisualElement>("Users3").style.visibility = StyleKeyword.Null;
            usage.Q<VisualElement>("Users4").style.visibility = StyleKeyword.Null;
            usage.Q<VisualElement>("Users5").style.visibility = StyleKeyword.Null;
            usage.Q<VisualElement>("Users6").style.visibility = StyleKeyword.Null;
        }
        else if (menu.menustate == Menu.state.SHOPConfirmation)
        {
            menu.menustate = Menu.state.SHOP;

            root.Q<VisualElement>("Confirmation").style.visibility = Visibility.Hidden;
            confirmbox.Q<VisualElement>("q_up").style.visibility = StyleKeyword.Null;
            confirmbox.Q<VisualElement>("q_down").style.visibility = StyleKeyword.Null;
        }
        else if (menu.menustate == Menu.state.SHOPConfirmationNotEnough)
        {
            menu.menustate = Menu.state.SHOP;

            root.Q<VisualElement>("ConfirmationNotEnough").style.visibility = Visibility.Hidden;
        }
    }

    private void OnButtonB()
    {
        Debug.Log("B pressed");

        if (menu.menustate == Menu.state.OUT)
        {
            menu.menustate = Menu.state.SHOP;

            root.Q<VisualElement>("InteractBox").style.visibility = Visibility.Hidden;
            root.Q<VisualElement>("TopDesk").style.visibility = Visibility.Visible;
            root.Q<VisualElement>("Main").style.visibility = Visibility.Visible;
            root.Q<VisualElement>("Bot").style.visibility = Visibility.Visible;

            menu.shop.selected_id = 0;
            menu.shop.filter_pg = 0;
            DisplayShopItems();
        }
        else if (menu.menustate == Menu.state.SHOP)
        {
            List<ItemInfo> list = GetCurlist();

            //Can buy
            if (list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.quantity > 0 && menu.inv.gold >= list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.cost)
            {
                menu.menustate = Menu.state.SHOPConfirmation;
                confirm = true;

                root.Q<VisualElement>("Confirmation").style.visibility = Visibility.Visible;

                confirmbox.Q<Label>("Quantity").text = "1";

                confirmbox.Q<VisualElement>("q_down").style.visibility = Visibility.Hidden;

                if (list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.quantity > 1)
                    confirmbox.Q<VisualElement>("q_up").style.visibility = Visibility.Visible;
                else
                    confirmbox.Q<VisualElement>("q_up").style.visibility = Visibility.Hidden;

                confirmbox.Q<VisualElement>("Confirm").style.backgroundColor = new Color(143f / 255f, 143f / 255f, 143f / 255f, 255f);
                confirmbox.Q<VisualElement>("Cancel").style.backgroundColor = new Color(217f / 255f, 217f / 255f, 217f / 255f, 255f);
            }
            //Cannot buy
            else if (list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.quantity > 0 && menu.inv.gold < list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item.cost)
            {
                menu.menustate = Menu.state.SHOPConfirmationNotEnough;

                root.Q<VisualElement>("ConfirmationNotEnough").style.visibility = Visibility.Visible;
            }
        }
        else if (menu.menustate == Menu.state.SHOPConfirmation)
        {
            if(confirm)
            {
                List<ItemInfo> list = GetCurlist();
                ref Item item = ref list[menu.shop.selected_id + menu.shop.cur_pg * menu.shop.maxitem_per_pg].item;
                float cost = item.cost * int.Parse(confirmbox.Q<Label>("Quantity").text);
                
                if (menu.inv.gold >= cost)
                {
                    menu.menustate = Menu.state.SHOP;

                    menu.inv.gold -= cost;
                    goldlabel.text = menu.inv.gold.ToString();
                    item.quantity -= int.Parse(confirmbox.Q<Label>("Quantity").text);

                    root.Q<VisualElement>("Confirmation").style.visibility = Visibility.Hidden;
                    confirmbox.Q<VisualElement>("q_up").style.visibility = StyleKeyword.Null;
                    confirmbox.Q<VisualElement>("q_down").style.visibility = StyleKeyword.Null;

                    DisplayShopItems();
                }
                else
                {
                    menu.menustate = Menu.state.SHOPConfirmationNotEnough;

                    root.Q<VisualElement>("Confirmation").style.visibility = Visibility.Hidden;
                    confirmbox.Q<VisualElement>("q_up").style.visibility = StyleKeyword.Null;
                    confirmbox.Q<VisualElement>("q_down").style.visibility = StyleKeyword.Null;

                    root.Q<VisualElement>("ConfirmationNotEnough").style.visibility = Visibility.Visible;
                }
            }
            else
            {
                menu.menustate = Menu.state.SHOP;

                root.Q<VisualElement>("Confirmation").style.visibility = Visibility.Hidden;
                confirmbox.Q<VisualElement>("q_up").style.visibility = StyleKeyword.Null;
                confirmbox.Q<VisualElement>("q_down").style.visibility = StyleKeyword.Null;
            }
        }
        else if (menu.menustate == Menu.state.SHOPConfirmationNotEnough)
        {
            menu.menustate = Menu.state.SHOP;

            root.Q<VisualElement>("ConfirmationNotEnough").style.visibility = Visibility.Hidden;
        }
    }

    private void OnButtonX()
    {
        Debug.Log("X pressed");
    }

    private void OnButtonY()
    {
        Debug.Log("Y pressed");
    }

    private void OnButtonLB()
    {
        Debug.Log("LB pressed");

        if (menu.menustate == Menu.state.SHOP)
        {
            menu.shop.filter_pg--;

            if (menu.shop.filter_pg < 0)
                menu.shop.filter_pg = 5;

            menu.shop.selected_id = 0;
            menu.shop.cur_pg = 0;
            DisplayShopItems();
        }
    }

    private void OnButtonRB()
    {
        Debug.Log("RB pressed");

        if (menu.menustate == Menu.state.SHOP)
        {
            menu.shop.filter_pg++;

            if (menu.shop.filter_pg > 5)
                menu.shop.filter_pg = 0;

            menu.shop.selected_id = 0;
            menu.shop.cur_pg = 0;
            DisplayShopItems();
        }
    }
}
