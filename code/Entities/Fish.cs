using Sandbox;
using System;
using System.Collections.Generic;

namespace Fishing;

public partial class Fish : KeyframeEntity
{
	readonly int bias = 200; //used to determine x and y coordinate spawn area
	Vector3 waypoint;

	public override void Spawn()
	{
        this.SetModel("././models/fish_basic.vmdl");
        this.Tags.Add("fish");
        var spawnPoint = GetSpawnPoint(bias);
		this.Position = spawnPoint;
		//model.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
		waypoint = GetRndWaypoint(bias);
		this.KeyframeTo(new Transform(waypoint), 3.0f);
	}

	public override void Simulate(IClient cl)
	{
		base.Simulate(cl);

		if(this.Position == waypoint)
		{
			waypoint = GetRndWaypoint(bias);
			this.KeyframeTo(new Transform(waypoint), 3.0f);
		}
	}

    private static Vector3 GetSpawnPoint(int bias)
	{
		Vector3 randomSpace = Vector3.Random * bias;
		randomSpace.z = 80; //set z to specific height
		return randomSpace;
	}

	private static Vector3 GetRndWaypoint(int xyLim)
	{
		Random rnd = new();
		int xRnd = rnd.Next(-xyLim, xyLim);
		int yRnd = rnd.Next(-xyLim, xyLim);
		int zRnd = rnd.Next(10, 50);
		Vector3 waypoint = new(xRnd, yRnd, zRnd);
		//DebugOverlay.Sphere(waypoint, 2.0f, Color.Red, duration: 3.0f);
		return waypoint;
	}
}