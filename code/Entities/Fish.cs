using Sandbox;
using System;

namespace Fishing;

public partial class Fish : ModelEntity
{
	public override void Spawn()
	{
        var model = new ModelEntity();
        model.SetModel("././models/fish_basic.vmdl");
        model.Tags.Add("fish");
        var spawnPoint = GetSpawnPoint();
		model.Position = spawnPoint;
		model.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
	}

    private static Vector3 GetSpawnPoint()
	{
		var bias = 200; //x and y limit
		Vector3 randomSpace = Vector3.Random * bias;
		randomSpace.z = 80; //set z to specific height
		return randomSpace;
	}
}