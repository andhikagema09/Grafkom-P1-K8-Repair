using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text coinText;
    public int coinCount;
    public static CoinManager instance;

    void Start()
    {
        instance = this;
        coinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coinCount + "";
    }

    public void UpdateCoin()
    {
        coinCount = coinCount + 1;
    }
}
