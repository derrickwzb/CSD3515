using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public enum state
    {
        OUT,
        SHOP,
        INVENTORY,
        SHOPConfirmation,
        SHOPConfirmationNotEnough,
        NONE
    }

    public state menustate;
    public Shop shop;
    public Inventory inv;

    void OnAwake()
    {
        menustate = state.OUT;

        shop = GetComponent<Shop>();
        inv = GetComponent<Inventory>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
