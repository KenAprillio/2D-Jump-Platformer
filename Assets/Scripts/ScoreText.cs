using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private void FixedUpdate()
    {
        GetComponent<Text>().text = "X " + Data.score.ToString();
    }
}
