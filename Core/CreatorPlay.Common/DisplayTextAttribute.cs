namespace CreatorPlay.Common;

public class DisplayTextAttribute : Attribute
{
	public string DisplayText { get; set; }

	public DisplayTextAttribute(string displayText)
	{
		DisplayText = displayText;
	}
}
