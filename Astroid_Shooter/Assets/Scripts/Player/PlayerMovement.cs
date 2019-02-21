using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerMovement : MonoBehaviour {
    float touchStartPos = 0;
    float playerStartPos = 0;

    GameObject controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        float touchDiffrence;
        float temp;
        if (Advertisement.isShowing == false)
        {
            if (!controller.GetComponent<GameController>().isPaused)
            {
                if (Input.touchCount == 1)
                {

                    if (transform.position.x <= 5.5f && transform.position.x >= -5.5f)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            ResetPositions();
                        }
                        else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
                        {
                            float touchCurrentPosition = ((Input.GetTouch(0).position.x / 64) - 6.25f);
                            touchDiffrence = touchCurrentPosition - touchStartPos;
                            temp = playerStartPos + touchDiffrence;
                            if (temp <= 5.5f && temp >= -5.5f)
                            {
                                this.GetComponent<Rigidbody2D>().MovePosition(new Vector2(temp, -5));
                            }
                            else
                            {
                                ResetPositions();
                            }
                        }
                    }
                }
            }
        }
    }

    void ResetPositions()
    {
        touchStartPos = ((Input.GetTouch(0).position.x / 64) - 6.25f);
        playerStartPos = transform.position.x;
    }
}
