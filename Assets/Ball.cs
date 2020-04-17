using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour {
    public float moveSpeed = 8f;
    public Rigidbody2D rb;
    public Vector2 dir = new Vector2(1, 0);
    public TMP_Text playerScore;
    public TMP_Text enemyScore;
    public AudioSource hitSound;
    public AudioSource goalSound;
    public bool resetingBall = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        transform.Translate(dir.x * Time.fixedDeltaTime * moveSpeed, dir.y * Time.fixedDeltaTime * moveSpeed, 0);

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        CheckIfHitWall(x, y, z);
        CheckIfHitGoal(x);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        GameObject objectCollided = col.gameObject;

        if (col.gameObject.tag == "Bracket") {
            float heightDifference = transform.position.y - objectCollided.transform.position.y;
            if (Math.Abs(heightDifference) <= 0.2) {
                dir = new Vector2(-dir.x, 0);
                EnhanceBallSpeed();
                hitSound.PlayOneShot(hitSound.clip);
            }
            else if (heightDifference > 0.2) {
                dir = new Vector2(-dir.x, 1);
                EnhanceBallSpeed();
                hitSound.PlayOneShot(hitSound.clip);
            }
            else if (heightDifference < 0.2) {
                dir = new Vector2(-dir.x, -1);
                EnhanceBallSpeed();
                hitSound.PlayOneShot(hitSound.clip);
            }
        }
    }

    private void CheckIfHitWall(float x, float y, float z) {
        if (y > 4.8) {
            transform.SetPositionAndRotation(new Vector3(x, Convert.ToSingle(4.8), z), Quaternion.identity);
            dir = new Vector2(dir.x, -dir.y);
            EnhanceBallSpeed();
            hitSound.PlayOneShot(hitSound.clip);
        }
        else if (y < -4.8) {
            transform.SetPositionAndRotation(new Vector3(x, Convert.ToSingle(-4.8), z), Quaternion.identity);
            dir = new Vector2(dir.x, -dir.y);
            EnhanceBallSpeed();
            hitSound.PlayOneShot(hitSound.clip);
        }
    }

    private void CheckIfHitGoal(float x) {
        if (!resetingBall) {
            if (x > 9) {
                resetingBall = true;
                enemyScore.text = Convert.ToString(Convert.ToInt32(enemyScore.text) + 1);
                StartCoroutine(WaitToResetBall(1.5f));
            }
            else if (x < -9) {
                resetingBall = true;
                playerScore.text = Convert.ToString(Convert.ToInt32(playerScore.text) + 1);
                StartCoroutine(WaitToResetBall(1.5f));
            }
        }
    }

    private void EnhanceBallSpeed() {
        moveSpeed += 0.3f;
    }

    private void ResetBall() {
        moveSpeed = 8f;
        transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
    }

    IEnumerator WaitToResetBall(float waitTime) {
        goalSound.Play();
        moveSpeed = 0f;
        yield return new WaitForSeconds(waitTime);
        ResetBall();
        resetingBall = false;
    }
}
