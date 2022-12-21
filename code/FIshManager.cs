using Sandbox;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Fishing.Manager;

public partial class MyGame : Sandbox.GameManager
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
				var model = new Fish();
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

			foreach(var fish in Entity.All.OfType<Fish>())
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
}
