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
    [SerializeField]
    private Player player;

    private void Update()
    {
            ammotxt.text = player.GetAmmo().ToString();
            healthtxt.text = player.GetHealth().ToString();
            speedtxt.text = player.GetSpeed().ToString();
    }

}
