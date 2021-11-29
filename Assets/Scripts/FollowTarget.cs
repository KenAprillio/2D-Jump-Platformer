using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FollowTarget : MonoBehaviour
{
    public Transform player;
    public Transform background;
    public GameObject startPanel;
    public GameObject losePanel;
    public GameObject playerSelf;
    public GameObject destroyer;
    public Slider distanceSlider;

    private void Awake()
    {
        Data.score = 0;
        distanceSlider.value = 0;
        startPanel.SetActive(true);
        losePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Starting Tutorial
        if (startPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Continue();
        }

        //Losing Scene
        if (losePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ContinueLoosing();
            }
        }

        //This to follow the player whenever they go
        if (player.position.y != transform.position.y && player.position.y > 0 && player.position.y > transform.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.position.y, transform.position.z), 0.1f);
            distanceSlider.value = Mathf.Floor(player.position.y);
        }
        background.transform.position = new Vector2(background.transform.position.x, transform.position.y);

        //The player looses if they fall
        if (player.position.y < (transform.position.y - 6))
        {
            playerSelf.GetComponent<PlayerController>().Dead();
            losePanel.SetActive(true);
        }

        //Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }


        if (player != null)
        {
            destroyer.transform.position = new Vector2(
                destroyer.transform.position.x,
                transform.position.y - 10);
        }
    }

    public void Continue()
    {
        playerSelf.GetComponent<PlayerController>().GameStart();
        startPanel.SetActive(false);
    }

    public void ContinueLoosing()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
