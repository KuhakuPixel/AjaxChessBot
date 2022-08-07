using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
namespace AjaxChessBot
{
    class AjaxConfigFile
    {
        public int normalThinkTime = 1000;
        public int randomThinkTimeMin = 1000;
        public int randomThinkTimeMax = 3000;
        public string pathToEngine;
        public bool randomizeThinkingTime = false;
        public bool advisorMode = false;
        public Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingWhite = new Dictionary<string, MouseOperation.MousePoint>();
        public Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingBlack = new Dictionary<string, MouseOperation.MousePoint>();
        public AjaxConfigFile(
            string pathToEngine,
            Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingWhite,
            Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingBlack,
            int normalThinkTime,
            int randomThinkTimeMin,
            int randomThinkTimeMax
            )
        
        {
            this.pathToEngine = pathToEngine;
            this.chessBoardCoordinatePlayingWhite = chessBoardCoordinatePlayingWhite;
            this.chessBoardCoordinatePlayingBlack = chessBoardCoordinatePlayingBlack;
            this.normalThinkTime =normalThinkTime;
            this.randomThinkTimeMin = randomThinkTimeMin;
            this.randomThinkTimeMax = randomThinkTimeMax;
        }
        public AjaxConfigFile()
        {

        }
        public void Save()
        {
         
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText("AjaxConfig.json", json);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saving the program state sucsessfull");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Load()
        {
            string json= File.ReadAllText("AjaxConfig.json");
            AjaxConfigFile loadedConfig = JsonConvert.DeserializeObject<AjaxConfigFile>(json);
            // load
            this.pathToEngine = loadedConfig.pathToEngine;
            this.chessBoardCoordinatePlayingBlack = loadedConfig.chessBoardCoordinatePlayingBlack;
            this.chessBoardCoordinatePlayingWhite = loadedConfig.chessBoardCoordinatePlayingWhite;
            this.normalThinkTime = loadedConfig.normalThinkTime;
            this.randomThinkTimeMin = loadedConfig.randomThinkTimeMin;
            this.randomThinkTimeMax = loadedConfig.randomThinkTimeMax;
            this.randomizeThinkingTime = loadedConfig.randomizeThinkingTime;
            this.advisorMode = loadedConfig.advisorMode;
        }
    }
}
