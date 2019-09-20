﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;        //Allows us to use SceneManager
/*
//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Character : movement
{
    public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
    public int pointsPerFood = 10;                //Number of points to add to player food points when picking up a food object.
    public int pointsPerSoda = 20;                //Number of points to add to player food points when picking up a soda object.
    public int wallDamage = 1;                    //How much damage a player does to a wall when chopping it.


    private Animator animator;                    //Used to store a reference to the Player's animator component.
    private int food;                            //Used to store player food points total during level.


    //Start overrides the Start function of MovingObject
    protected override void Start()
    {


     

        //Call the Start function of the MovingObject base class.
        base.Start();
    }


    //This function is called when the behaviour becomes disabled or inactive.
    private void OnDisable()
    {
        //When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
    
    }


    private void Update()
    {
        //If it's not the player's turn, exit the function.
        

        int horizontal = 0;      //Used to store the horizontal move direction.
        int vertical = 0;        //Used to store the vertical move direction.


        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Check if moving horizontally, if so set vertical to zero.
        if (horizontal != 0)
        {
            vertical = 0;
        }

        //Check if we have a non-zero value for horizontal or vertical

    }

    //AttemptMove overrides the AttemptMove function in the base class MovingObject
    //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        //Every time player moves, subtract from food points total.
        

        //Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
        base.AttemptMove<T>(xDir, yDir);

        //Hit allows us to reference the result of the Linecast done in Move.

        //If Move returns true, meaning Player was able to move into an empty space.
        if (Move(xDir, yDir, out RaycastHit2D hit))
        {
            //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
        }

      

    }


    //OnCantMove overrides the abstract function OnCantMove in MovingObject.
    //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
    


    //Restart reloads the scene when called.
    private void Restart()
    {
        //Load the last scene loaded, in this case Main, the only scene in the game.
        SceneManager.LoadScene(0);
    }


    //LoseFood is called when an enemy attacks the player.
    //It takes a parameter loss which specifies how many points to lose.
 


    //CheckIfGameOver checks if the player is out of food points and if so, ends the game.
 

    internal override T GetComponent<T>()
    {
        Move(xDir, yDir, out RaycastHit2D hit)
    }

    protected override void OnCantMove<T>(T component)
    {
        throw new System.NotImplementedException();
    }
}
*/