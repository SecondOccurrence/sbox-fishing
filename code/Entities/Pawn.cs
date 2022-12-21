using Sandbox;
using System;
using System.Linq;

namespace Player;

partial class Pawn : AnimatedEntity
{
	/// <summary>
	/// Called when the entity is first created 
	/// </summary>
	public override void Spawn()
	{
		base.Spawn();

		//
		// Use a watermelon model
		//
		SetModel( "models/sbox_props/watermelon/watermelon.vmdl" );
		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
	}

	// An example BuildInput method within a player's Pawn class.
	[ClientInput] public Angles ViewAngles { get; set; }

	public override void BuildInput()
	{
		var look = Input.AnalogLook;

		if(ViewAngles.pitch is > 90 or < -90)
		{
			look = look.WithYaw(look.yaw * -1f);
		}

		var viewAngles = ViewAngles;
		viewAngles += look;
		viewAngles.pitch = viewAngles.pitch.Clamp(-70,70);
		ViewAngles = viewAngles.Normal;
	}

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );
		Rotation = ViewAngles.ToRotation();

		// If we're running serverside and Attack1 was just pressed, spawn a ragdoll
		if ( Game.IsServer && Input.Pressed( InputButton.PrimaryAttack ) )
		{
			TraceResult tr = Trace.Ray(AimRay, 10000)
				.WithTag("fish")
				.Run();
			if(tr.Hit)
			{
				Log.Info("hit");
				DebugOverlay.Sphere(tr.EndPosition, 2.0f, Color.Red, duration: 3.0f);
				if(tr.Entity.Tags.Has("hooked"))
				{
					Log.Info("caught");
					tr.Entity.Delete();
				}
				else
				{
					tr.Entity.Tags.Add("hooked");
				}
			}
		}
	}

	/// <summary>
	/// Called every frame on the client
	/// </summary>
	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );

		// Update rotation every frame, to keep things smooth
		Rotation = ViewAngles.ToRotation();

		Camera.Position = Position;
		Camera.Rotation = Rotation;

		// Set field of view to whatever the user chose in options
		Camera.FieldOfView = Screen.CreateVerticalFieldOfView( Game.Preferences.FieldOfView );

		// Set the first person viewer to this, so it won't render our model
		Camera.FirstPersonViewer = this;
	}
}
