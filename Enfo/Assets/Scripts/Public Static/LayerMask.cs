
public static class LayerNames
{
	public const int Default0 = 0;
	public const int TransparentFX1 = 1;
	public const int IgnoreRaycast2 = 2;
	public const int Blank3 = 3;
	public const int Water4 = 4;
	public const int UI5 = 5;
	public const int Blank6 = 6;
	public const int Blank7 = 7;
	public const int Terrain8 = 8;
	public const int Targetable9 = 9;
	public const int Ally10 = 10;
};

public enum LayerEnum
{
	Default,
	TransparentFX,
	IgnoreRaycast,
	Blank,
	Water,
	UI,
	Blank6,
	Blank7,
	Terrainm,
	Targetable,
	Ally
}

public static class LayerMasks
{
	public const int Default0		= 1 << LayerNames.Default0;
	public const int TransparentFX1	= 1 << LayerNames.TransparentFX1;
	public const int IgnoreRaycast2	= 1 << LayerNames.TransparentFX1;
	public const int Blank3			= 1 << LayerNames.Blank3;
	public const int Water4			= 1 << LayerNames.Water4;
	public const int UI5			= 1 << LayerNames.UI5;
	public const int Blank6			= 1 << LayerNames.Blank6;
	public const int Blank7			= 1 << LayerNames.Blank7;
	public const int Terrain8		= 1 << LayerNames.Terrain8;
	public const int Targetable9	= 1 << LayerNames.Targetable9;
	public const int Ally10			= 1 << LayerNames.Ally10;
};