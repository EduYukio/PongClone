using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float moveSpeed = 10f;
    public Ball ballObject;
    public int dir = 0;
    public bool waitingToChange = false;

    void Start() {

    }

    void FixedUpdate() {
        float heightDifference = ballObject.transform.position.y - transform.position.y;
        if (!waitingToChange) {
            if (heightDifference > 0.2) {
                dir = 1;
            }
            else if (heightDifference < -0.2) {
                dir = -1;
            }
            else {
                waitingToChange = true;
                StartCoroutine(WaitToChangeDir(0.075f));
            }
        }
        transform.Translate(0, dir * Time.fixedDeltaTime * moveSpeed, 0);

        BlockMovementToFitTheScreen();
    }

    public void BlockMovementToFitTheScreen() {
        float x = transform.position.x;
        float z = transform.position.z;

        if (transform.position.y > 3.7) {
            transform.SetPositionAndRotation(new Vector3(x, Convert.ToSingle(3.7), z), Quaternion.identity);
        }
        else if (transform.position.y < -3.7) {
            transform.SetPositionAndRotation(new Vector3(x, Convert.ToSingle(-3.7), z), Quaternion.identity);
        }
    }

    IEnumerator WaitToChangeDir(float waitTime) {
        dir = UnityEngine.Random.Range(-1, 2);
        yield return new WaitForSeconds(waitTime);
        waitingToChange = false;
    }
}
