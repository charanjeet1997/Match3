using UnityEngine;

namespace Games.Match3Game
{
	public abstract class Booster
	{

		#region PRIVATE_VARS
		[SerializeField] protected BoosterType boosterType;
		[SerializeField] protected Sprite boosterIcon;
		#endregion

		#region PUBLIC_VARS

		protected Booster(BoosterType boosterType, Sprite boosterIcon)
		{
			this.boosterType = boosterType;
			this.boosterIcon = boosterIcon;
		}
		
		
		public abstract void UseBooster(Tile tile, Tile[,] tiles);
		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS

		#endregion
		
	}
	
	public enum BoosterType
	{
		Shovel,
		Rake,
		GardeningGloves,
		None
	}
}