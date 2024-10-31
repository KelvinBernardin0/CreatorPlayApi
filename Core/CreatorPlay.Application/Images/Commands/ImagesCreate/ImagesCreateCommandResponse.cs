namespace CreatorPlay.Application.TemplateHistory.Commands.TemplateHistoryCreate;




public class ImagesCreateCommandResponse
{
	public string Message { get; }
	public string ImagePath { get; }
	public string ImageName { get; }

	public ImagesCreateCommandResponse(string message, string imagePath, string imageName)
	{
		Message = message;
		ImagePath = imagePath;
		ImageName = imageName;
	}
}
