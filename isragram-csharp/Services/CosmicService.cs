using isragram_csharp.Dtos;
using System.Net.Http.Headers;

namespace isragram_csharp.Services
{
    public class CosmicService
    {
        public string SendImage(ImageDto image)
        {
            Stream imageStream = image.Image.OpenReadStream(); //transform to binary

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "OzYTyOXN7yxULWUpbOSET8x1RYzpPNS0S5SJMjSzZ2MOFQf6YC");

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "file");

            var requestContent = new MultipartFormDataContent
            {
                {new StreamContent(imageStream), "media", image.Name}
            };

            postRequest.Content = requestContent;
            var reqReturn = client.PostAsync("https://workers.cosmicjs.com/v3/buckets/isragram-images/media", postRequest.Content).Result;

            var imageUrl = reqReturn.Content.ReadFromJsonAsync<CosmicResponseDto>() ;

            return imageUrl.Result.media.url;
        }
    }
}
