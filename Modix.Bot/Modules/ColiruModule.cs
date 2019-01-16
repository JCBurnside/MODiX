using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Modix.Services.Coliru;

namespace Modix.Bot.Modules
{
    [Name("Coliru"), Summary("Sends code to be excuted on coliru.")]
    public class ColiruModule : ModuleBase
    {
        private readonly ColiruService _service;

        public ColiruModule(ColiruService service)
        {
            _service = service;
        }

        [Command("coliru"), Summary("Sends your code to coliru to compile and run.")]
        public async Task Run()
        {
            string result;
            bool success;
            try
            {
                (result, success) = await _service.UploadAndCompileAsync(Context.Message);
            }
            catch(WebException ex)
            {
                await ReplyAsync("Something Went Wrong.\n" + ex.Message);
                return;
            }

            await ReplyAsync(Context.User.Mention, false, _service.BuildEmbed(Context.User, result, success));
            await Context.Message.DeleteAsync();
        }
    }
}
