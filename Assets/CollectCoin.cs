using UnityEngine;
using TMPro;

public class CollectCoin : MonoBehaviour
{
    public GameObject tmpScore;
    private int Coins = 0;
    // Start is called before the first frame update

    private void Start()
    {
        tmpScore = GameObject.FindGameObjectWithTag("score");
    }
    void setCoins()
    {
        tmpScore.GetComponent< TextMeshProUGUI>().text = Coins.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "hitTarget")
        {
            bool isDead = collision.gameObject.GetComponent<Move>().getHit();
            if (isDead)
            {
                Coins++;
                setCoins();
            }
            
        }
    }
}
