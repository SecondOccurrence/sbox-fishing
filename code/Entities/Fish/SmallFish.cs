using Fishing;
using Sandbox;

namespace Fishing.FishTypes;

public class SmallFish : FishBase
{
    readonly double speed = 100; //the fish's speed when moving from point to point

    public override void Spawn()
    {
        base.Spawn();
        this.SetModel("././models/small_fish.vmdl");
        base.NextMove(speed);
    }

	public override void Simulate(IClient cl)
	{
		base.Simulate(cl);

        if(this.Position == waypoint)
		{
			NextMove(speed);
		}
	}
}