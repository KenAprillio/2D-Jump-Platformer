using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowCoins : MonoBehaviour
{
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "Kamu telah mengumpulkan " + Data.score +" coin! Banyak sekali!";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }
    }

}
