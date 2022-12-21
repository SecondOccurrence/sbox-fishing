using Sandbox;
using Sandbox.UI;
using System;
using System.IO;
using System.Linq;
using UI;
using Player;

namespace Fishing;

public partial class FishingGame : Sandbox.GameManager
{
	public FishingGame()
	{
		if(Game.IsClient)
		{
			Game.RootPanel = new RootPanel();
			Game.RootPanel.AddChild<HUD>();
		}
	}

	public override void ClientJoined( IClient client )
	{
		base.ClientJoined( client );

		var pawn = new Pawn();
		client.Pawn = pawn;

		var spawnpoints = Entity.All.OfType<SpawnPoint>();

		var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();

		if ( randomSpawnPoint != null )
		{
			var tx = randomSpawnPoint.Transform;
			tx.Position += Vector3.Up * 50.0f;
			pawn.Transform = tx;
		}
	}
}
