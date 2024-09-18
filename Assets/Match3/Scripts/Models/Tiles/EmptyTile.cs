using System.Collections.Generic;
using ServiceLocatorFramework;
using UnityEngine.Serialization;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class EmptyTile : Tile
	{
		#region PRIVATE_VARS
		
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region UNITY_CALLBACKS
		

		#endregion

		#region PUBLIC_METHODS
		
		
		#endregion

		#region PRIVATE_METHODS

		#endregion

		public override void Init(int x, int y, TileType tileType, TileName tileName, Sprite sprite)
		{
			this.x = x;
			this.y = y;
			this.tileType = tileType;
			this.tileName = tileName;
		}

		public override void ShowTile()
		{
			tileAnimator.SetTrigger(Match3Constants.AnimatorConstants.Show);
			isHidden = false;
			CoroutineExtension.ExecuteAfterFrame(this, () => { tileAnimator.ResetTrigger(Match3Constants.AnimatorConstants.Show); });
		}

		public override void HideTile()
		{
			tileAnimator.SetTrigger(Match3Constants.AnimatorConstants.Shrink);
			isHidden = true;
			CoroutineExtension.ExecuteAfterFrame(this, () => { tileAnimator.ResetTrigger(Match3Constants.AnimatorConstants.Shrink); });
		}
	}
}