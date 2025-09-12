using BookTitleManager.Models;
using Newtonsoft.Json;

namespace BookTitleManager.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7001/api/");
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BooksModel>> GetBooksAsync(string? search = null)
        {
            string url = "BooksAPI";
            if (!string.IsNullOrEmpty(search))
                url += $"?search={Uri.EscapeDataString(search)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API call failed: {response.StatusCode}, {error}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<BooksModel>>(content)!;
        }

        public async Task AddBookAsync(BooksModel book)
        {
            var response = await _httpClient.PostAsJsonAsync("BooksAPI", book);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API call failed: {response.StatusCode}, {error}");
            }
        }

    }
}
