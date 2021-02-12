using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Player : Sprite
{
    float speed = 4f;
    int attacktimer = 0;
    bool attack;
    string facing;
    MyGame _game;

    public enum Direction { TOP, DOWN, RIGHT, LEFT };
    public Direction Facing;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the player
    /// </summary>
    #region Constructor
    public Player() : base("player.png")
    {
        x = 800 / 2;
        y = 600 / 2;
        SetOrigin(width / 2, height / 2);
    }
    #endregion
    public void createGame(MyGame _game)
    {
        this._game = _game;
    }
    //----------------------------------------------------------------------------------------
    //                                        Movement
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Movement of the player
    /// </summary>
    #region Movement

    void Movement()
    {
        float moveX = 0;
        float moveY = 0;

        if (Input.GetKey(Key.A) || Input.GetKey(Key.LEFT))
        {
            Facing = Direction.LEFT;
            moveX = -speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.D) || Input.GetKey(Key.RIGHT))
        {
            Facing = Direction.RIGHT;
            moveX = speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.W) || Input.GetKey(Key.UP))
        {
            Facing = Direction.TOP;
            moveX = 0;
            moveY = -speed;
        }
        if (Input.GetKey(Key.S) || Input.GetKey(Key.DOWN))
        {
            Facing = Direction.DOWN;
            moveX = 0;
            moveY = speed;
        }
        Collision collision = MoveUntilCollision(moveX, moveY); //You can move until collision, for example with Tiled Map
        if (collision != null)
        {
            handleCollision(collision);
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Collision
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Collision of the player
    /// </summary>
    #region Collision

    void handleCollision(Collision col)
    {
        Console.WriteLine(this.name);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Attacks
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Attacks of the player
    /// </summary>
    #region Attack
    void Attack()
    {
        attacktimer++;
        if (Input.GetKey(Key.E) & attack)
        {
            facing = Facing.ToString();
            _game.Attack(facing, this.x, this.y);
            attack = false;
        }
        if (attacktimer > 50)
        {
            attacktimer = 0;
            attack = true;
        }
    }

    #endregion
    void Update()
    {
        Movement();
        Attack();
    }
}
