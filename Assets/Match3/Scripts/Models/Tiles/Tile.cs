using System.Collections.Generic;
using ServiceLocatorFramework;
using UnityEngine.Serialization;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class Tile : MonoBehaviour
	{
		#region PRIVATE_VARS

		[SerializeField] protected SpriteRenderer tileRenderer;
		[SerializeField] protected Animator tileAnimator;
		private Coroutine moveToPositionCoroutine;
		[SerializeField] protected Collider2D tileCollider;
		#endregion

		#region PUBLIC_VARS

		public string tileID;
		public int x, y;
		public TileType tileType;
		public TileName tileName;
		public float animationTime = 5.0f;
		public List<Tile> neighbours;
		public bool isHidden = false;
		public bool isTileAnimating = false;

		public SpriteRenderer TileRenderer => tileRenderer;

		#endregion

		#region UNITY_CALLBACKS
		

		#endregion

		#region PUBLIC_METHODS
		public virtual void Init(int x, int y,TileType tileType, TileName tileName, Sprite sprite)
		{
			this.x = x;
			this.y = y;
			this.tileType = tileType;
			this.tileName = tileName;
			SetSprite(sprite);
			if (tileName == TileName.Random)
			{
				ServiceLocator.Current.Get<TileManager>().TilePlacementHandlers[TileName.Random].HandlePlacement(x,y,tileType,tileName,this,ServiceLocator.Current.Get<BoardManager>().match3GameConfiguration.tilesData);
			}
		}
		
		public virtual void ShowTile()
		{
			tileAnimator.SetTrigger(Match3Constants.AnimatorConstants.Show);
			isHidden = false;
			tileCollider.enabled = true;
			CoroutineExtension.ExecuteAfterFrame(this, () => { tileAnimator.ResetTrigger(Match3Constants.AnimatorConstants.Show); });
		}
		
		public virtual void HideTile()
		{
			tileAnimator.SetTrigger(Match3Constants.AnimatorConstants.Shrink);
			isHidden = true;
			tileCollider.enabled = false;
			CoroutineExtension.ExecuteAfterFrame(this, () => { tileAnimator.ResetTrigger(Match3Constants.AnimatorConstants.Shrink); });
		}
		
		public void VibrateTile()
		{
			tileAnimator.SetTrigger(Match3Constants.AnimatorConstants.Vibrate);
			CoroutineExtension.ExecuteAfterFrame(this, () => { tileAnimator.ResetTrigger(Match3Constants.AnimatorConstants.Vibrate); });
		}
		
		public void AddNeighbour(Tile tile)
		{
			if (neighbours == null)
			{
				neighbours = new List<Tile>();
			}
			neighbours.Add(tile);
		}

		public void SetPosition(Vector3 position)
		{
			transform.position = position;
		}
		
		public void SetSprite(Sprite sprite)
		{
			tileRenderer.sprite = sprite;
		}
		public void SetGridPosition(int _x, int _y)
		{
			x = _x;
			y = _y;
		}
		
		public Coroutine AnimateToPosition(Vector2 targetPosition, params Action[] actions)
		{
			if (moveToPositionCoroutine != null)
			{
				StopCoroutine(moveToPositionCoroutine);
			}
//			Debug.Log($"Moving Tile {transform.gameObject.name}");
			moveToPositionCoroutine = StartCoroutine(AnimateToPositionRoutine(targetPosition, actions));
			return moveToPositionCoroutine;
		}
		#endregion

		#region PRIVATE_METHODS
		protected IEnumerator AnimateToPositionRoutine(Vector2 targetPosition,params Action[] actions)
		{
			isTileAnimating = true;
			Vector3 startPosition = transform.position;
			float currentAnimationTime = 0;
			while (currentAnimationTime < animationTime)
			{
				currentAnimationTime += Time.deltaTime;
				transform.position = Vector3.Lerp(startPosition, targetPosition, currentAnimationTime / animationTime);
				yield return null;
			}
			// if(transform.localScale != Vector3.one)
			// {
			// 	ShowTile();	
			// }
			SetPosition(targetPosition);
			foreach (Action action in actions)
			{
				action?.Invoke();
			}

			yield return new WaitForEndOfFrame();
			isTileAnimating = false;
		}

		#endregion
	}
}