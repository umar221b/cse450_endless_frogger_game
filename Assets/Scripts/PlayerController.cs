using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovingObject
{

    bool moving = false;

    // Update is called once per frame
    void Update()
    {
        if (!moving) {

          int horizontal = 0;      //Used to store the horizontal move direction.
          int vertical = 0;        //Used to store the vertical move direction.



          //Check if moving horizontally, if so set vertical to zero.
          if (horizontal != 0)
          {
              vertical = 0;
          }

          //Check if we have a non-zero value for horizontal or vertical
          if (horizontal != 0 || vertical != 0)
          {
              moving = true;
              //Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
              //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
              Move (horizontal, vertical);
          }
        }
    }
}
