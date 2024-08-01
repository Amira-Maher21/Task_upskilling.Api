

namespace upskillingApi.DTOs
{
	public class ResponseDto
	{
		public bool StatusCode { get; set; }
		public int TypeError { get; set; }
		public string Masssage { get; set; }
		public List<string> Exception { get; set; }
		public object Result { get; set; }
		public ResponseDto()
		{
			StatusCodeMassage(TypeError);
		}
		public string StatusCodeMassage(int typeError)
		{

			switch (typeError)
			{
				case 400:
					Masssage = "You Have Made BadRequest";
					TypeError = 400;
					break;
				case 401:
					Masssage = "You Are Not Authorized";
					TypeError = 401;
					break;
				case 403:
					Masssage = "You Have Made Forbidden";
					TypeError = 403;
					break;
				case 404:
					Masssage = "Resource  was  Not Found ";
					TypeError = 404;
					break;
				case 500:
					Masssage = "Internal server error";
					TypeError = 500;
					break;
				case 302:
					Masssage = "URL is not Found";
					TypeError = 302;
					break;
				default:
					Masssage = "success status response ";
					TypeError = 200;
					break;
			};
			return Masssage;
		}
	}
}
