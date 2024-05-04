using Godot;
using System;

public partial class EntityNameplate : Control
{
	[ExportCategory("Nameplate Info")]
	[Export] private Label nameLabel;
	[Export] private TextureRect icon;
	
	[ExportCategory("Health")]
	[Export] private TextureProgressBar healthBar;
	[Export] private Label currentHealthLabel;
	[Export] private Label maxHealthLabel;
	
	[ExportCategory("Energy")]
	[Export] private TextureProgressBar energyBar;
	[Export] private Label currentEnergyLabel;
	[Export] private Label maxEnergyLabel;
	
	[ExportCategory("Buffs and Debuffs")]
	[Export] private HFlowContainer buffsContainer;
	[Export] private HFlowContainer debuffsContainer;

	public Label NameLabel => nameLabel;
	public TextureRect Icon => icon;
	public TextureProgressBar HealthBar => healthBar;
	public Label CurrentHealthLabel => currentHealthLabel;
	public Label MaxHealthLabel => maxHealthLabel;
	public TextureProgressBar EnergyBar => energyBar;
	public Label CurrentEnergyLabel => currentEnergyLabel;
	public Label MaxEnergyLabel => maxEnergyLabel;
	public HFlowContainer BuffsContainer => buffsContainer;
	public HFlowContainer DebuffsContainer => debuffsContainer;
}