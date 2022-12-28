using Sandbox;
using System;
using System.Collections.Generic;
using Fishing;

namespace Fishing.FishTypes;

public partial class FishBase : KeyframeEntity
{
	readonly int bias = 200; //used to determine x and y coordinate spawn area
	protected Vector3 waypoint; //the position which the fish is actively moving towards

	public override void Spawn()
	{
		//setup the fish
		base.Spawn();
        Tags.Add("fish");
        var spawnPoint = FishingGame.GetSpawnPoint(bias);
		this.Position = spawnPoint;
		this.SetupPhysicsFromModel( PhysicsMotionType.Static, false );
	}

	public override void Simulate(IClient cl)
	{
		base.Simulate(cl);
	}

	public async void NextMove(double speed)
	{
		var prevWaypoint = waypoint; //store previous waypoint for later use
		waypoint = FishingGame.GetRndWaypoint(bias);

		var distance = waypoint.Distance(Position); //get distance needed to travel to waypoint
		var time = Convert.ToSingle(distance / speed); //get time so movement is constant

		await this.KeyframeTo(new Transform(waypoint), time); // move to waypoint
	}
}