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
		waypoint = GetRndWaypoint(bias);
		var distance = waypoint.Distance(Position);
		var time = Convert.ToSingle(distance * 0.01);
		this.KeyframeTo(new Transform(waypoint), time);
	}

	public override void Simulate(IClient cl)
	{
		base.Simulate(cl);

		if(this.Position == waypoint)
		{
			waypoint = GetRndWaypoint(bias);
			var distance = waypoint.Distance(Position);
			var time = Convert.ToSingle(distance * 0.01);
			this.KeyframeTo(new Transform(waypoint), time);
		}
	}

    private static Vector3 GetSpawnPoint(int bias)
	{
		Vector3 randomSpace = Vector3.Random * bias;
		randomSpace.z = 45; //set z to specific height
		return randomSpace;
	}

	private static Vector3 GetRndWaypoint(int xyLim)
	{
		Random rnd = new();
		int xRnd = rnd.Next(-xyLim, xyLim);
		int yRnd = rnd.Next(-xyLim, xyLim);
		int zRnd = rnd.Next(10, 45);
		Vector3 waypoint = new(xRnd, yRnd, zRnd);
		//DebugOverlay.Sphere(waypoint, 2.0f, Color.Red, duration: 3.0f);
		return waypoint;
	}
}