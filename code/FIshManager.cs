using Sandbox;
using System;
using System.Linq;

namespace Fishing.Manager;

public partial class MyGame : Sandbox.GameManager
{
	TimeUntil next = 2;
	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );
		if(next)
		{
			var ragdoll = new ModelEntity();
			ragdoll.SetModel( "models/fish_basic.vmdl" );
			var spawnPoint = GetSpawnPoint();
			ragdoll.Position = spawnPoint;
			ragdoll.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
			next = 2;
		}
	}

	private static Vector3 GetSpawnPoint()
	{
		//define spawn area limits
		var bias = 200;
		Vector3 randomSpace = Vector3.Random * bias;
		randomSpace.z = 80;
		return randomSpace;
	}
}
