namespace TradeControl
{
    using Microsoft.AspNetCore.Http;

    public static class FileValidator
    {

        private static readonly Dictionary<string, byte[]> MagicNumbers = new()
        {
            [".pdf"] = new byte[] { 0x25, 0x50, 0x44, 0x46 }, // %PDF
            [".xml"] = new byte[] { 0x3C, 0x3F, 0x78, 0x6D }  // <?xm
        };

        public static bool IsValid(IFormFile file, out string errorMessage)
        {
            errorMessage = string.Empty;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!MagicNumbers.ContainsKey(extension))
            {
                errorMessage = $"Extensão de arquivo não permitida. Permitido: {string.Join(", ", MagicNumbers.Keys)}";
                return false;
            }

            using var stream = file.OpenReadStream();
            var buffer = new byte[4];
            var read = stream.Read(buffer, 0, 4);

            if (read < 4 || !buffer.Take(4).SequenceEqual(MagicNumbers[extension]))
            {
                errorMessage = $"O conteúdo do arquivo não corresponde a um {extension} válido.";
                return false;
            }

            return true;
        }
    }

}
