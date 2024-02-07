using Newtonsoft.Json;
using System.IO.Compression;

namespace artur_gde_krosi_Vue.Server.Models.moyskladApi
{
    public class getApiRequest<T> where T : InterfaceApi.Root
    {
        public async Task<T> GetApiReqesi(T apiClass, string herf)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage requestGroup;
                if (herf != "https://api.moysklad.ru/api/remap/1.2/report/stock/all")
                {
                    requestGroup = new HttpRequestMessage(HttpMethod.Get, herf +
                    "&offset=0&limit=100");
                }
                else
                {
                    requestGroup = new HttpRequestMessage(HttpMethod.Get, herf);
                }

                requestGroup.Headers.Add("Accept-Encoding", "gzip");
                //request.Headers.Add("Authorization", "Bearer c168b9e54a4a67e4ee6ceb5e14e3d98947f09653");
                requestGroup.Headers.Add("Authorization", "Bearer ad4bc311f51bafdc7357e20ece905d282f6fe448");
                var responseGroup = await client.SendAsync(requestGroup);
                responseGroup.EnsureSuccessStatusCode();

                using (Stream stream = await responseGroup.Content.ReadAsStreamAsync())
                using (var gzip = new GZipStream(stream, CompressionMode.Decompress))
                using (var reader = new StreamReader(gzip))
                {
                    apiClass = (JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync()));
                }
                if (apiClass.Count() <= 101 && herf != "https://api.moysklad.ru/api/remap/1.2/report/stock/all")
                {
                    int offset = 100;
                    while (true)
                    {
                        try
                        {
                            var requestDopGroup = new HttpRequestMessage(HttpMethod.Get, herf +
                                "&offset=" + offset.ToString() + "&limit=100");
                            requestDopGroup.Headers.Add("Accept-Encoding", "gzip");
                            requestDopGroup.Headers.Add("Authorization", "Bearer ad4bc311f51bafdc7357e20ece905d282f6fe448");
                            var responseDopGroup = await client.SendAsync(requestDopGroup);
                            responseDopGroup.EnsureSuccessStatusCode();

                            using (Stream stream = await responseDopGroup.Content.ReadAsStreamAsync())
                            using (var gzip = new GZipStream(stream, CompressionMode.Decompress))
                            using (var reader = new StreamReader(gzip))
                            {
                                T dopClass = apiClass.New() as T;
                                dopClass = (JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync()));
                                if (dopClass.Count() != 0)
                                {
                                    if (dopClass.Count() < 100)
                                    {
                                        apiClass.AddRange<T>(dopClass);
                                        break;
                                    }
                                    else
                                    {
                                        apiClass.AddRange<T>(dopClass);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            offset += 100;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            throw;
                        }

                    }
                }
            }
            return (T)apiClass;
        }
    }
}
