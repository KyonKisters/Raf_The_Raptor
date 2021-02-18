using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;                                // GXPEngine contains the engine
using TiledMapParser;

//Cleaning up code = CTRL + K + D
//Commenting = CTRL + K + C
//Uncommenting = CTRL + K + U

public class MyGame : Game
{
    Level level;
    int levelnumber = 1;
    bool StartedGame;
    string filename;
    bool delete;
    Sprite Startmenu;
    bool firstCutscencesOver = false;
    int counter = 0;
    int timer=0;
    int gothrough=1;
    bool secondCutscenesOver = false;
    bool thirdCutscenesOver = false;
    Sound background = new Sound("background-music-20min.wav");
    Sprite[] firstLevelCutscenes = new Sprite[4];
    Sprite[] secondLevelCutscenes = new Sprite[4];
    Sprite[] thirdLevelCutscenes = new Sprite[4];
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Game
    /// </summary>
    #region Constructor
    public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        Startmenu = new Sprite("StartScreen.png");
        createStartMenu();
        background.Play(false,0,0.5f);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Load Levels
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Load the levels
    /// </summary>
    #region Load Levels
    public void LoadLevel(string filename, int levelnumber,bool delete)
    {
        timer++;
        this.filename = filename;
        this.levelnumber = levelnumber;
        this.delete = delete;

        if (delete)
        {
            Game.main.GetChildren().ForEach(DestroyGame);
            delete = false;
        }
        this.delete = delete;

        if (levelnumber == 2)
        {
            for (int i = 0; i <= 3; i++)
            {
                secondLevelCutscenes[i] = new Sprite("Level2CutsceneNR" + i + ".png");
            }
            if (Input.GetKey(Key.ENTER) & timer > 10)
            {
                counter++;
                timer = 0;
            }
            if (counter == 0 & gothrough == 1)
            {
                AddChild(secondLevelCutscenes[0]);
                gothrough++;
            }
            if (counter == 1 & gothrough == 2)
            {
                AddChild(secondLevelCutscenes[1]);
                secondLevelCutscenes[0].LateDestroy();
                gothrough++;
            }
            if (counter == 2 & gothrough == 3)
            {
                AddChild(secondLevelCutscenes[2]);
                secondLevelCutscenes[1].LateDestroy();
                gothrough++;
            }
            if (counter == 3 & gothrough == 4)
            {
                AddChild(secondLevelCutscenes[3]);
                secondLevelCutscenes[2].LateDestroy();
                gothrough++;
            }
            if (counter >= 4)
            {
                secondLevelCutscenes[3].LateDestroy();
                secondCutscenesOver = true;
            }
            if (secondCutscenesOver)
            {
                counter = 0;
                gothrough = 1;
                level = new Level(filename, this, levelnumber);
                AddChild(level);
            }
        }

        if (levelnumber == 3)
        {
            for (int i = 0; i <= 3; i++)
            {
                thirdLevelCutscenes[i] = new Sprite("Level3CutsceneNR" + i + ".png");
            }
            if (Input.GetKey(Key.ENTER) & timer > 10)
            {
                counter++;
                timer = 0;
            }
            if (counter == 0 & gothrough == 1)
            {
                AddChild(thirdLevelCutscenes[0]);
                gothrough++;
            }
            if (counter == 1 & gothrough == 2)
            {
                AddChild(thirdLevelCutscenes[1]);
                thirdLevelCutscenes[0].LateDestroy();
                gothrough++;
            }
            if (counter == 2 & gothrough == 3)
            {
                AddChild(thirdLevelCutscenes[2]);
                thirdLevelCutscenes[1].LateDestroy();
                gothrough++;
            }
            if (counter == 3 & gothrough == 4)
            {
                AddChild(thirdLevelCutscenes[3]);
                thirdLevelCutscenes[2].LateDestroy();
                gothrough++;
            }
            if (counter >= 4)
            {
                thirdLevelCutscenes[3].LateDestroy();
                thirdCutscenesOver = true;
            }
            if (thirdCutscenesOver)
            {
                counter = 0;
                gothrough = 1;
                level = new Level(filename, this, levelnumber);
                AddChild(level);
            }

        }
        }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Start Game
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Start Game
    /// </summary>
    #region start screen
    void StartGame()
    {
        timer++;
        for (int i = 0; i <= 3; i++)
        {
            firstLevelCutscenes[i] = new Sprite("Level1CutsceneNR" + i +".png");
        }
        if (Input.GetKey(Key.ENTER) & timer>10)
        {
            counter++;
            timer = 0;
        }
        if (counter==0 & gothrough==1)
        {
            AddChild(firstLevelCutscenes[0]);
            gothrough++;
        }
        if (counter == 1 & gothrough==2)
        {
            AddChild(firstLevelCutscenes[1]);
            firstLevelCutscenes[0].LateDestroy();
            gothrough++;
        }
        if (counter == 2 & gothrough == 3)
        {
            AddChild(firstLevelCutscenes[2]);
            firstLevelCutscenes[1].LateDestroy();
            gothrough++;
        }
        if (counter == 3 & gothrough == 4)
        {
            AddChild(firstLevelCutscenes[3]);
            firstLevelCutscenes[2].LateDestroy();
            gothrough++;
        }
        if (counter>=4)
        {
            firstLevelCutscenes[3].LateDestroy();
            firstCutscencesOver = true;
        }
        if (firstCutscencesOver)
        {
            counter = 0;
            gothrough = 1;
            level = new Level("Level" + levelnumber + ".tmx", this, levelnumber);
            AddChild(level);
        }
    }
    public void createStartMenu()
    {
        AddChild(Startmenu);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Destroys Levels
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Destroys the levels
    /// </summary>
    #region Level destroying
    void DestroyGame(GameObject other)
    {
        other.LateRemove();
        other.LateDestroy();
    }
    #endregion
    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
    void Update()
    {
        if (!StartedGame & Input.GetKey(Key.ENTER))
        {
            StartedGame = true;
            Startmenu.LateDestroy();

        }
        if (!firstCutscencesOver & StartedGame) StartGame();

        if (!secondCutscenesOver & levelnumber == 2)
        {
            LoadLevel(filename, levelnumber, delete);
        }
        if (!thirdCutscenesOver & levelnumber == 3)
        {
            LoadLevel(filename, levelnumber, delete);
        }
    }
}