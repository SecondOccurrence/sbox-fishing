using Sandbox;
using System;
using System.Linq;
using System.Collections.Generic;
using Fishing.FishTypes;

namespace Fishing;

public partial class FishingGame : Sandbox.GameManager
{
	TimeUntil nextSpawn = GetRandomNum(2, 5);
	TimeUntil nextDespawn = GetRandomNum(4, 8);
	List<KeyframeEntity> fish = new();
	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );
		if(Game.IsServer)
		{
			if(nextSpawn)
			{
				var model = new FishBase();
				fish.Add(model);
				nextSpawn = GetRandomNum(2, 5);
			}
			if(nextDespawn)
			{
				int chance = GetRandomNum(1,100);
				if(fish.Count > 1)
				{
					if(chance <= 50)
					{
						var index = GetRandomNum(0, fish.Count);
						fish[index].Delete();
						fish.RemoveAt(index);
					}
				}
				nextDespawn = GetRandomNum(4,8);
			}

			foreach(var fish in Entity.All.OfType<FishBase>())
			{
				fish.Simulate(cl);
			}
		}
	}

	private static int GetRandomNum(int min, int max)
	{
		Random rnd = new();
		int num = rnd.Next(min, max);
		return num;
	}

	public static Vector3 GetSpawnPoint(int bias)
	{
		Vector3 randomSpace = Vector3.Random * bias;
		randomSpace.z = 40; //set z to specific height
		return randomSpace;
	}

	public static Vector3 GetRndWaypoint(int xyLim)
	{
		Random rnd = new();
		int xRnd = rnd.Next(-xyLim, xyLim);
		int yRnd = rnd.Next(-xyLim, xyLim);
		int zRnd = rnd.Next(10, 40);
		Vector3 waypoint = new(xRnd, yRnd, zRnd);
		return waypoint;
	}
}
