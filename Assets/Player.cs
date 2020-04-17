using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float moveSpeed = 10f;

    void Start() {

    }

    void FixedUpdate() {
        MoveInput();
    }

    public void MoveInput() {
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(0, y * Time.fixedDeltaTime * moveSpeed, 0);

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
}
