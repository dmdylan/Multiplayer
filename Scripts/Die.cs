using Godot;

public partial class Die : RigidBody3D
{
	[Export] public Sprite3D[] FaceImages { get; private set; }
	[Export] public DieType DieType { get; private set; }
	
	private DieFace[] dieFaces;
	
	public void InitDie(DieInfo dieInfo)
	{
		dieFaces = dieInfo.DieFaces;
		DieType = dieInfo.DieType;
		
		for (int i = 0; i < dieFaces.Length; i++)
		{
			ChangeFaceSprite(i, dieFaces[i].Image);
		}
	}
	
	public void ChangeFaceSprite(int faceIndex, Texture2D image)
	{
		FaceImages[faceIndex].Texture = image;
	}
}
