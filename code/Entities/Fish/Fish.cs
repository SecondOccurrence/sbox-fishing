using Sandbox;
using System;
using System.Collections.Generic;
using Fishing;

namespace Fishing.FishTypes;

public partial class Fish : KeyframeEntity
{
	readonly int bias = 200; //used to determine x and y coordinate spawn area
	Vector3 waypoint; //the position which the fish is actively moving towards
	readonly double speed = 100; //the fish's speed when moving from point to point

	public override void Spawn()
	{
		//setup the fish
        this.SetModel("././models/fish_basic.vmdl");
        Tags.Add("fish");
        var spawnPoint = FishingGame.GetSpawnPoint(bias);
		this.Position = spawnPoint;
		this.SetupPhysicsFromModel( PhysicsMotionType.Static, false );

		//assign its first waypoint
		waypoint = FishingGame.GetRndWaypoint(bias);
		var distance = waypoint.Distance(Position);
		var time = Convert.ToSingle(distance / speed);
		this.KeyframeTo(new Transform(waypoint), time);
	}

	public override void Simulate(IClient cl)
	{
		base.Simulate(cl);

		if(this.Position == waypoint)
		{
			waypoint = FishingGame.GetRndWaypoint(bias);
			var distance = waypoint.Distance(Position);
			var time = Convert.ToSingle(distance / speed);
			this.KeyframeTo(new Transform(waypoint), time);
		}
	}
}