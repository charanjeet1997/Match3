using System;

namespace Games.Match3Game
{
	[Serializable]
	public class LevelData
	{
		public string levelName;
		public int width;
		public int height;
		public Challenge challenge;
		public TilePlacementData[] tilesPlacementData;
		
		public LevelData(string levelName, int width, int height)
		{
			this.levelName = levelName;
			this.width = width;
			this.height = height;

		}
		
		public LevelData(string levelName, int width, int height,Challenge challenge)
		{
			this.levelName = levelName;
			this.width = width;
			this.height = height;
			this.challenge = challenge;
		}
	}
}