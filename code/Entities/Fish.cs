using Sandbox;
using System;

namespace Fishing;

public partial class Fish : ModelEntity
{
	public override void Spawn()
	{
		base.Spawn();

        var model = new ModelEntity();
        model.SetModel("models/fish_basic_vmdl");
        model.Tags.Add("fish");
	}
}