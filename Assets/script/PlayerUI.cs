using UnityEngine;
using TMPro;


/*
This class handles the display of the player UI
*/
public class PlayerUI : MonoBehaviour
{

    public TextMeshProUGUI ammotxt;
    public TextMeshProUGUI healthtxt;
    public TextMeshProUGUI speedtxt;
    private Player player;
    
    PlayerUI(Player player)
    {
        this.player = player;
    }

    void updateUI()
    {
        ammotxt.text = player.getAmmo().ToString();
        healthtxt.text = player.getHealth().ToString();
        speedtxt.text = player.getSpeed().ToString();
    }
}
