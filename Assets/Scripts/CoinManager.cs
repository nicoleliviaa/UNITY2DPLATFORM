using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text cointext;
   public int coincount;
  

    // Update is called once per frame
    void Update()
    {
        cointext.text = coincount.ToString();
        
    }
 
    public int GetCoinCount()
    {
        return coincount;
    }
}
