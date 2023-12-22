using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI ammotxt;
    public TextMeshProUGUI healthtxt;
    public TextMeshProUGUI maxHealthTxt;
    public TextMeshProUGUI scoretxt;
    private int score = 0;

    void Awake()
    {
        if(instance != null)
        {
            return ;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateUIHealth(int health)
    {
        healthtxt.text = health.ToString();
    }

    public void UpdateUIMaxHealth(int maxHealth)
    {
        maxHealthTxt.text = maxHealth.ToString();
    }
    
    public void UpdateUIAmmo(int ammo)
    {
        ammotxt.text = ammo.ToString();
    }

    private void UpdateUIScore()
    {
        Debug.Log("update");
        scoretxt.text = score.ToString();
    }

    public void AddScore(int score)
    {
        Debug.Log("add 1 score");
        this.score += score;
        Debug.Log("add 1 score " + score);
        UpdateUIScore();
    }
}
