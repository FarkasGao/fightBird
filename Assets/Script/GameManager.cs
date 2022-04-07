using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int sorce1p = 0;
    private int sorce2p = 0;
    private int coin1p = 0;
    private int coin2p = 0;
    public TextMeshProUGUI sorce1pText;
    public TextMeshProUGUI sorce2pText;
    public TextMeshProUGUI coin1pText;
    public TextMeshProUGUI coin2pText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetBird1P()
    {
        coin1p += 2;
        sorce1p += 1;
        sorce1pText.text = "1P Score: " + sorce1p;
        coin1pText.text = "Coin: " + coin1p;
    }
    public void GetBird2P()
    {
        coin2p += 2;
        sorce2p += 1;
        sorce2pText.text = "2P Score: " + sorce2p;
        coin2pText.text = "Coin: " + coin2p;
    }
}
