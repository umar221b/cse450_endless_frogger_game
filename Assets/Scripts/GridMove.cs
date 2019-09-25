using System;
using System.Collections;
using UnityEngine;

class GridMove : MonoBehaviour {
    private float moveSpeed = 1f;
    private float gridSize = 1f;
    private enum Orientation {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Vertical;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
    private Vector3 startingPosition_initial;
    public void Update() {
        if (!isMoving) {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (!allowDiagonals) {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
                    input.y = 0;
                } else {
                    input.x = 0;
                }
            }
 
            if (input != Vector2.zero) {
                //StartCoroutine(Example());
                StartCoroutine(move(transform));
            if (input == Vector2.zero) {
                    StartCoroutine(Example());
                }
            }
        }
    }
    
    public IEnumerator move(Transform transform) {
        
        startPosition = transform.position;
        //startPosition = Transform.Translate(2, 2, 2);
        startingPosition_initial = startPosition;
        t = 0;
        
        if(gridOrientation == Orientation.Horizontal && startPosition == startingPosition_initial) {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
            yield return new WaitForSeconds(.1f);
        } else if(gridOrientation == Orientation.Vertical && startPosition == startingPosition_initial) 
                {
            
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
            yield return new WaitForSeconds(.1f);

        }
 
        if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
            factor = 0.7071f;
        } else {
            factor = 1f;
        }
 
        while (t < 1f) {
            t += Time.deltaTime * (moveSpeed/gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
           
            yield return null;
        }
 
        isMoving = false;
        yield return 0;
    }
    IEnumerator Example()
    {
        yield return new WaitForSecondsRealtime(1);
    }
    private void WaitForSecondsRealtime(int v)
    {
        throw new NotImplementedException();
    }
}

public class Going
{
}