using DataBindingFramework;
using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;

	[Serializable]
	public class Challenge
	{
		#region PRIVATE_VARS
		IPropertyManager propertyManager;
		Property<int> movesProperty;
		Property<int> targetTile1Property;
		Property<int> targetTile2Property;
		Property<int> maxTile1Property;
		Property<int> maxTile2Property;
		#endregion

		#region PUBLIC_VARS
		public string description;
		public int moveLimit;
		public int moveUsed;
		public ChallangeStatus status;
		public bool IsMoveLimitExceeded => moveUsed >= moveLimit;
		public ClearTilesData targetTile1;
		public ClearTilesData targetTile2;
		public ClearTilesData progressTile1;
		public ClearTilesData progressTile2;
		public bool skipMove;
		#endregion

		#region PUBLIC_METHODS
		public Challenge( string description, int moveLimit)
		{
			this.description = description;
			this.moveLimit = moveLimit;
			this.moveUsed = 0;
			if (Application.isPlaying)
			{
				CacheProperties();
				movesProperty.Value = moveLimit;
			}
		}

		public void Init()
		{
			CacheProperties();
			status = ChallangeStatus.InProgress;
			moveUsed = 0;
			movesProperty.Value = moveLimit;
			progressTile1.tileName = targetTile1.tileName;
			progressTile1.amount = 0;
			progressTile2.tileName = targetTile2.tileName;
			progressTile2.amount = 0;
			targetTile1Property.Value =  targetTile1.amount;
			targetTile2Property.Value = targetTile2.amount;
		}

		public void UpdatePropertyData()
		{
			CacheProperties();
			targetTile1Property.Value =  targetTile1.amount - progressTile1.amount;
			targetTile2Property.Value = targetTile2.amount - progressTile2.amount;
			maxTile1Property.Value = targetTile1.amount;
			maxTile2Property.Value = targetTile2.amount;
		}

		public virtual void UpdateProgress(TileName TileName, int amount)
		{
			if (targetTile1.tileName == TileName)
			{
				progressTile1.amount += amount;
				progressTile1.amount = Mathf.Clamp(progressTile1.amount,0, targetTile1.amount);
				targetTile1Property.Value =  targetTile1.amount - progressTile1.amount;
			}
			else if (targetTile2.tileName == TileName)
			{
				progressTile2.amount += amount;
				progressTile2.amount = Mathf.Clamp(progressTile2.amount,0, targetTile2.amount);
				targetTile2Property.Value = targetTile2.amount - progressTile2.amount;
			}
		}

		public bool IsCompleted()
		{
			if (progressTile1.amount >= targetTile1.amount && progressTile2.amount >= targetTile2.amount)
			{
				status = ChallangeStatus.Completed;
				return true;
			}
			return false;
		}
		
		public bool IsFailed()
		{
			if (IsMoveLimitExceeded)
			{
				status = ChallangeStatus.Failed;
				return true;
			}
			return false;
		}

		public void UseMove()
		{
			if (!skipMove)
			{
				moveUsed++;
				moveUsed = Mathf.Clamp(moveUsed, 0, moveLimit);
				movesProperty.Value = moveLimit - moveUsed;
			}
			else
			{
				skipMove = false;
			}
		}
		

		public virtual void Reset()
		{
			moveUsed = 0;
			movesProperty.Value = moveLimit;
			progressTile1.amount = 0;
			progressTile2.amount = 0;
			status = ChallangeStatus.NotStarted;
			targetTile1Property.Value = 0;
			targetTile2Property.Value = 0;
		}

		#endregion

		#region PRIVATE_METHODS
		private void CacheProperties()
		{
			propertyManager = ServiceLocator.Current.Get<IPropertyManager>();
			movesProperty = propertyManager.GetOrCreateProperty<int>(Match3Constants.PropertyNameConstants.MovesPropertyName);
			targetTile1Property = propertyManager.GetOrCreateProperty<int>(Match3Constants.PropertyNameConstants.TileToCollect1PropertyName);
			targetTile2Property = propertyManager.GetOrCreateProperty<int>(Match3Constants.PropertyNameConstants.TileToCollect2PropertyName);
			maxTile1Property = propertyManager.GetOrCreateProperty<int>(Match3Constants.PropertyNameConstants.MaxTileToCollect1PropertyName);
			maxTile2Property = propertyManager.GetOrCreateProperty<int>(Match3Constants.PropertyNameConstants.MaxTileToCollect2PropertyName);
		}
		#endregion	

	}
	
	public enum ChallangeStatus
	{
		NotStarted,
		InProgress,
		Completed,
		Failed
	}

	[Serializable]
	public class ClearTilesData
	{
		public TileName tileName;
		public int amount;

		public ClearTilesData(TileName tileName, int amount)
		{
			this.tileName = tileName;
			this.amount = amount;
		}
	}
	
}