﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
namespace AjaxDynamicHtmlReader
{
    class AjaxConfigFile
    {
        public string pathToEngine;
        public Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingWhite = new Dictionary<string, MouseOperation.MousePoint>();
        public Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingBlack = new Dictionary<string, MouseOperation.MousePoint>();
        public AjaxConfigFile(string pathToEngine, 
            Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingWhite,Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingBlack)
        {
            this.pathToEngine = pathToEngine;
            this.chessBoardCoordinatePlayingWhite = chessBoardCoordinatePlayingWhite;
            this.chessBoardCoordinatePlayingBlack = chessBoardCoordinatePlayingBlack;

        }
        public AjaxConfigFile()
        {

        }
        public void Save()
        {
            string json=JsonSerializer.Serialize(this);
            File.WriteAllText("AjaxConfig.json", json);
        }
        public void Load()
        {
            string json= File.ReadAllText("AjaxConfig.json");
            AjaxConfigFile loadedConfig=JsonSerializer.Deserialize<AjaxConfigFile>(json);
            this.pathToEngine = loadedConfig.pathToEngine;
            this.chessBoardCoordinatePlayingBlack = loadedConfig.chessBoardCoordinatePlayingBlack;
            this.chessBoardCoordinatePlayingWhite = loadedConfig.chessBoardCoordinatePlayingWhite;
        }
    }
}
