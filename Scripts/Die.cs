using Godot;

public partial class Die : RigidBody3D
{
	[Export] public Sprite3D[] FaceImages { get; private set; }
	[Export] public DieType DieType { get; private set; }
	
	private DieFace[] dieFaces;
	
	public Die(DieInfo dieInfo)
	{
		dieFaces = dieInfo.DieFaces;
		DieType = dieInfo.DieType;
		
		for (int i = 0; i < dieFaces.Length; i++)
		{
			ChangeFaceSprite(i, dieFaces[i].Image);
		}
	}
	
	public void ChangeFaceSprite(int faceIndex, Image image)
	{
		FaceImages[faceIndex].Texture = ImageTexture.CreateFromImage(image);
	}
}
