using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Modix.Services.Utilities;
using Newtonsoft.Json;

namespace Modix.Services.Coliru
{
    public class ColiruService
    {
        private const string coliruMain = "http://coliru.stacked-crooked.com/compile";
        private static readonly Dictionary<SupportedLangs, string> commands = new Dictionary<SupportedLangs, string>{
            { SupportedLangs.c, "mv main.cpp main.c && gcc -std=c11 -O2 -Wall -Wextra -pedantic main.c && ./a.out"},
            { SupportedLangs.cpp, "g++ -std=c++1z -O2 -Wall -Wextra -pedantic -pthread main.cpp -lstdc++fs && ./a.out" },
            { SupportedLangs.py, "python3 main.cpp" },
            { SupportedLangs.haskell, "runhaskell main.cpp" }
        };
        private static readonly HttpClient _client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        public async Task<(string, bool)> UploadAndCompileAsync(IMessage msg)
        {
            string[] codeBlock = msg.Content?.Split(Environment.NewLine);
            if (codeBlock is null
                || !codeBlock.First().StartsWith("```")
                || !codeBlock.Last().EndsWith("```")
                || FormatUtilities.GetCodeLanguage(codeBlock.First()) is null)
            {
                return (@"This command needs a code block with a language (ie
                \`\`\`cpp
                int main() {
                    std::cout<<""Hello World!"";
                }
                \`\`\`).", false);
            }

            if (Enum.TryParse<SupportedLangs>(FormatUtilities.GetCodeLanguage(codeBlock.First()), out var result))
            {
                string code = string.Join(Environment.NewLine, codeBlock.Skip(1));
                code = code.Substring(0, code.LastIndexOf("```") - 3);
                HttpResponseMessage response = await _client.PostAsync(coliruMain, new StringContent(JsonConvert.SerializeObject(new { cmd = commands[result], src = code })));
                return (await response.Content?.ReadAsStringAsync(), response.IsSuccessStatusCode);
            }

            return ("Unsupported Language Detected.", false);
        }

        public Embed BuildEmbed(IUser user, string content, bool compiled)
        {
            return new EmbedBuilder()
                .WithTitle(compiled ? "Executed" : "Failed")
                .WithAuthor(user)
                .WithDescription(content)
                .WithColor(compiled ? Color.Green : Color.Red)
                .Build();
        }
    }
}
