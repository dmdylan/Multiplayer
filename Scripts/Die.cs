using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Die : RigidBody3D
{
	[Export] public Sprite3D[] FaceImages { get; private set; }
	[Export] public DieType DieType { get; private set; }

	public DieInfo DieInfo { get; private set; }
	public List<DieFace> DieFaces { get; private set; } = new();

	public override void _Ready()
	{
		for (int i = 0; i < DieFaces.Count; i++)
		{
			ChangeFaceSprite(i, DieFaces[i].Image);
		}
	}

	public void InitDie(DieInfo dieInfo)
	{
		DieInfo = dieInfo;
		
		DieFaces = dieInfo.DieFaces.ToList();
		DieType = dieInfo.DieType;
	}

	public void ChangeFaceSprite(int faceIndex, Texture2D image)
	{
		FaceImages[faceIndex].Texture = image;
	}
}
