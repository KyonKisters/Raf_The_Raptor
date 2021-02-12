using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
using GXPEngine.Core;


public class Level : GameObject
{
    private Player _player;
    private int _mapWidth;
    private int _mapHeight;
    public Level(string pLevelFilename) : base(false)
    {
        createLevel(pLevelFilename);
    }
    void createLevel(string pLevelFilename)
    {
        TiledLoader tileloader = new TiledLoader(pLevelFilename);

        for (int i = 0; i <tileloader.NumTileLayers;i++)
        {
            //Console.WriteLine(tileloader.map.Layers[i].Name);
            tileloader.addColliders = i == (tileloader.NumTileLayers - 1);
            tileloader.LoadTileLayers(i);
        }

        tileloader.autoInstance = true;
        tileloader.LoadObjectGroups();

        _player = game.FindObjectOfType<Player>();

        _mapWidth = tileloader.map.Width * tileloader.map.TileWidth;
        _mapHeight = tileloader.map.Height * tileloader.map.TileHeight;

    }

    void Update()
    {
        game.x = Mathf.Clamp(-_player.x + game.width / 2, -_mapWidth - game.width, 0);
        game.y = Mathf.Clamp(-_player.y + game.height / 2, -_mapHeight - game.height, 0);
    }
}