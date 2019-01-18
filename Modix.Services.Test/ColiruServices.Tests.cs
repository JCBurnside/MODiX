using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Modix.Services.Coliru;
using System.Threading.Tasks;
using Discord;

namespace Modix.Services.Test
{
    public class ColiruServices
    {
        private const string format =
@"```{0}
{1}
```";// 0 is language 1 is code.
        private ColiruService service;

        [OneTimeSetUp]
        public void SetUp()
        {
            service = new ColiruService();
        }


        [Test]
        public async Task Coliru_Cpp()
        {
            (string,bool) result = await service.UploadAndCompileAsync(new MockMessage { Content = string.Format(format, "cpp", @"#include <iostream>
            int main() {
                std::cout<<""Hello World!"";
            }") });
            Assert.AreEqual(("Hello World!", true), result);
        }

        [Test]
        public async Task Coliru_py()
        {
            (string, bool) result = await service.UploadAndCompileAsync(new MockMessage { Content = string.Format(format, "py",
@"print('Hello World!')
") });// python is weird
            Assert.AreEqual(("Hello World!\n", true), result);
        }

        // TODO : Add more tests for the other supported langs

        private class MockMessage : Discord.IMessage
        {
            public MessageType Type => throw new NotImplementedException();

            public MessageSource Source => throw new NotImplementedException();

            public bool IsTTS => throw new NotImplementedException();

            public bool IsPinned => throw new NotImplementedException();

            public string Content { get; set; }

            public DateTimeOffset Timestamp => throw new NotImplementedException();

            public DateTimeOffset? EditedTimestamp => throw new NotImplementedException();

            public IMessageChannel Channel => throw new NotImplementedException();

            public IUser Author => throw new NotImplementedException();

            public IReadOnlyCollection<IAttachment> Attachments => throw new NotImplementedException();

            public IReadOnlyCollection<IEmbed> Embeds => throw new NotImplementedException();

            public IReadOnlyCollection<ITag> Tags => throw new NotImplementedException();

            public IReadOnlyCollection<ulong> MentionedChannelIds => throw new NotImplementedException();

            public IReadOnlyCollection<ulong> MentionedRoleIds => throw new NotImplementedException();

            public IReadOnlyCollection<ulong> MentionedUserIds => throw new NotImplementedException();

            public MessageActivity Activity => throw new NotImplementedException();

            public MessageApplication Application => throw new NotImplementedException();

            public DateTimeOffset CreatedAt => throw new NotImplementedException();

            public ulong Id => throw new NotImplementedException();

            public Task DeleteAsync(RequestOptions options = null)
            {
                throw new NotImplementedException();
            }
        }
    }
}
