using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Sklave.exe_dcb.Commands
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        private IRole FindRole(IGuild guild, string name)
        {
            var roles = guild.Roles;
            foreach (IRole role in roles)
            {
                if (role.Name.Equals(name))
                    return role;
            }
            return null;
        }

        [Command("ping")]
        public async Task Ping(IGuildUser user = null, [Remainder] string reason = null)
        {

            if (user == null && reason == null)
            {
                await ReplyAsync("Pong! :beer: ");
            }
            else if (user != null)
            {
                await Context.Message.DeleteAsync();
                Console.WriteLine(user + " " + reason);
                await ReplyAsync("Pong " + user.Mention + " " + reason);
            }
        }

        [Command("shout")]
        public async Task Shout(string phrase)
        {
            String str = phrase.ToUpper();
            await ReplyAsync($"" + str);
        }

        [Command("ban")]
        [RequireUserPermission((GuildPermission.BanMembers & GuildPermission.Administrator), ErrorMessage = "You don't have the permission ``ban_member``!")]

        public async Task Ban(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified";

            await Context.Guild.AddBanAsync(user, 1, reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was banned\n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Ban Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);

            ITextChannel logChannel = Context.Client.GetChannel(767130688800817212) as ITextChannel;
            var EmbedBuilderLog = new EmbedBuilder()
                .WithDescription($"{user.Mention} was banned\n**Reason** {reason}\n**Moderator** {Context.User.Mention}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Ban Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embedLog = EmbedBuilderLog.Build();
            await logChannel.SendMessageAsync(embed: embedLog);
        }

        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers, ErrorMessage = "You don't have the permission ``kick_member``!")]

        public async Task Kick(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified";

            await user.KickAsync(reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was kicked\n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User kick Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);

            ITextChannel logChannel = Context.Client.GetChannel(767130688800817212) as ITextChannel;
            var EmbedBuilderLog = new EmbedBuilder()
                .WithDescription($"{user.Mention} was kicked\n**Reason** {reason}\n**Moderator** {Context.User.Mention}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Kick Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embedLog = EmbedBuilderLog.Build();
            await logChannel.SendMessageAsync(embed: embedLog);
        }

        [Command("unban")]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = "You don't have the permission ``Unbann/Ban_member``!")]

        public async Task Unban(ulong userId = 0)
        {
            Console.WriteLine(userId);
            if (userId == null)
            {
                await ReplyAsync("Please specify a userId!");
                return;
            }

            await Context.Guild.RemoveBanAsync(userId, null);

            await ReplyAsync("Unbanned!");
        }

        [Command("softban")]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = "You don't have the permission ``Ban/Unbann_member``!")]

        public async Task Softban(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified";

            await Context.Guild.AddBanAsync(user, 1, reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was Softbanned\n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Softban Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embed = EmbedBuilder.Build();


            await Context.Guild.RemoveBanAsync(user.Id);

            await ReplyAsync(embed: embed);

            ITextChannel logChannel = Context.Client.GetChannel(767130688800817212) as ITextChannel;
            var EmbedBuilderLog = new EmbedBuilder()
                .WithDescription($"{user.Mention} was Softbanned\n**Reason** {reason}\n**Moderator** {Context.User.Mention}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Softban Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embedLog = EmbedBuilderLog.Build();
            await logChannel.SendMessageAsync(embed: embedLog);
        }

        [Command("mute")]
        [RequireUserPermission(GuildPermission.ManageRoles, ErrorMessage = "You don't have the permission ``ManageRoles``!")]

        public async Task Mute(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified";


            ulong roleId = 786655723203330078;
            var role = Context.Guild.GetRole(roleId);
            await ((SocketGuildUser)Context.User).AddRoleAsync(role);
            await user.AddRoleAsync(role);

            var EmbedBuilderP = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was Muted\n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("You have been Muted!");
                });
            Embed embedP = EmbedBuilderP.Build();
            await user.SendMessageAsync(embed: embedP);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was Muted\n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Mute Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embed = EmbedBuilder.Build();

            await ReplyAsync(embed: embed);

            ITextChannel logChannel = Context.Client.GetChannel(767130688800817212) as ITextChannel;
            var EmbedBuilderLog = new EmbedBuilder()
                .WithDescription($"{user.Mention} was Muted\n**Reason** {reason}\n**Moderator** {Context.User.Mention}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Mute Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embedLog = EmbedBuilderLog.Build();
            await logChannel.SendMessageAsync(embed: embedLog);
        }

        [Command("unmute")]
        [RequireUserPermission(GuildPermission.ManageRoles, ErrorMessage = "You don't have the permission ``ManageRoles``!")]

        public async Task Unmute(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified";

            ulong roleId = 786655723203330078;
            var role = Context.Guild.GetRole(roleId);
            await ((SocketGuildUser)Context.User).RemoveRoleAsync(role);
            await user.AddRoleAsync(role);

            var EmbedBuilderP = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was Unmuted\n!")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("You have been Unmuted!");
                });
            Embed embedP = EmbedBuilderP.Build();
            await user.SendMessageAsync(embed: embedP);

            await ReplyAsync("Successfully Unmuted " + user.Mention);

            ITextChannel logChannel = Context.Client.GetChannel(767130688800817212) as ITextChannel;
            var EmbedBuilderLog = new EmbedBuilder()
                .WithDescription($"{user.Mention} was Unmuted\n**Reason** {reason}\n**Moderator** {Context.User.Mention}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Unmute Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embedLog = EmbedBuilderLog.Build();
            await logChannel.SendMessageAsync(embed: embedLog);
        }

        [Command("pin")]
        [RequireUserPermission(GuildPermission.Administrator, ErrorMessage = "You don't have the permission ``ManageRoles``!")]

        public async Task Pin([Remainder] string message = null)
        {
            await Context.Message.DeleteAsync();
            await Context.Message.PinAsync();
        }

        [Command("hack")]
        [RequireUserPermission(GuildPermission.Administrator, ErrorMessage = "You don't have the permission ``Administrator``!")]
        public async Task Hack()
        {
            await Context.Message.DeleteAsync();
            Console.WriteLine(@"Hack Mode activated! Now listening to your commands here. To deactivate write  end  ");
            string answer = Console.ReadLine();

//        Finisch:
            if (answer == "end")
            {
                Console.WriteLine("Press any key to exit hack mode");
//                goto Found;
            }
            else
            {
                await ReplyAsync(answer);
//                goto Finisch;
            }            

//        Found:
            Console.ReadKey();
        }

        [Command("kleiner")]
        public async Task Kleiner([Remainder] string message = null)
        {
            await ReplyAsync("@Christian");
        }



        [Command("afk")]
        public async Task Afk(IGuildUser user)
        {

            var oldNick = user.Username;
            var newNick = "AFK" + " " + oldNick;
            await Context.Message.DeleteAsync();
            await user.ModifyAsync(x => x.Nickname = newNick);
            await ReplyAsync("Set " + user.Mention + "AFK!");
        }

        [Command("unafk")]
        public async Task Unafk(IGuildUser user)
        {

        }

        [Command("roulett")]
        public async Task RussianRoulett(string num = null)
        {
            SocketGuildUser user;
            String reason = "";
            var UpperBounds = Int32.Parse(num);
            UpperBounds = UpperBounds + 1;
            int bullet = new Random().Next(0, UpperBounds);
            if (bullet == 1)
            {

                await ReplyAsync("U died!");
            }
            else
            {
                await ReplyAsync("U Survived!");
            }

        }

        [Command("rps")]
        [Alias("rock paper scissors")]
        public async Task RockRPS(string choice)
        {
            if (choice.Contains("paper") || choice.Contains("scissors") || choice.Contains("rock"))
            {
                int winner = new Random().Next(1, 4);
                if (winner == 1)
                {
                    await ReplyAsync($"You Win!");
                }
                else
                {
                    await ReplyAsync($"You Lost :(");
                }
            }
            else
            {
                await ReplyAsync("Please select 'rock', 'paper' or 'scissors'.");
            }
        }

        [Command("dance")]
        [Alias("tanz")]
        public async Task DanceAsync()
        {
            string user = "You do a *dance*!\n";

            int part1 = new Random().Next(0, 7);

            switch (part1)
            {
                case 0:
                    user += "https://i.imgur.com/qAJfaa5.gif"; ;
                    break;
                case 1:
                    user += "https://thumbs.gfycat.com/AccurateEsteemedEasternnewt-max-1mb.gif";
                    break;
                case 2:
                    user += "https://i.kym-cdn.com/photos/images/newsfeed/001/115/816/936.gif";
                    break;
                case 3:
                    user += "https://66.media.tumblr.com/93a8499e09c26c22990964804c1903eb/tumblr_nfjpkgwTJT1tfilc6o1_400.gif";
                    break;
                case 4:
                    user += "https://media.giphy.com/media/xT9DPhEIWztxt13Otq/giphy.gif";
                    break;
                case 5:
                    user += "https://media.giphy.com/media/dUyHxzWUJX8Ri/giphy.gif";
                    break;
                case 6:
                    user += "https://i.gifer.com/aW2.gif";
                    break;
                case 7:
                    user += "https://media.tenor.com/images/028d5d4019f46a46f03f5bac3902bf40/tenor.gif";
                    break;
            }
            await ReplyAsync(user + "");

        }

        [Command("roast")]
        [Alias("insult", "beleidigung")]
        public async Task RoastsAsync(IGuildUser userA)
        {
            if (userA == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            else
            {
                string user = "" + userA.Mention;

                int part1 = new Random().Next(0, 41);

                
                switch (part1)
                {
                    case 0:
                        user += " has a micropenis.";
                        break;
                    case 1:
                        user += " has an IQ of 6. ";
                        break;
                    case 2:
                        user += " needs serious help.";
                        break;
                    case 3:
                        user += " is awake past his bedtime.";
                        break;
                    case 4:
                        user += " plays Fortnite.";
                        break;
                    case 5:
                        user += " is so cheap he won't even tip his hat.";
                        break;
                    case 6:
                        user += " 's birth certificate is an apology from the condom factory.";
                        break;
                    case 7:
                        user += " uses a sniper rifle in GTA";
                        break;
                    case 8:
                        user += " listens to nightcore unironically. ";
                        break;
                    case 9:
                        user += "'s dad's condom broke. ";
                        break;
                    case 10:
                        user += " eats nachos without any dip. ";
                        break;
                    case 11:
                        user += " has a 3Head.";
                        break;
                    case 12:
                        user += " can't even lift. ";
                        break;
                    case 13:
                        user += " thinks Hitler is cool. ";
                        break;
                    case 14:
                        user += " plays with hot wheels on weekends for fun. ";
                        break;
                    case 15:
                        user += " doesn't look both ways when they cross the street. ";
                        break;
                    case 16:
                        user += " can not count to 10. ";
                        break;
                    case 17:
                        user += " failed preschool. ";
                        break;
                    case 18:
                        user += " plays the Wii without a safety strap. ";
                        break;
                    case 19:
                        user += " plays AP Yasuo support. ";
                        break;
                    case 20:
                        user += " digs straight down. ";
                        break;
                    case 21:
                        user += " leaves angry youtube comments. ";
                        break;
                    case 22:
                        user += " spams 'I NEED HEALING' ";
                        break;
                    case 23:
                        user += " is a teemo main. ";
                        break;
                    case 24:
                        user += " thinks minecraft is scary. ";
                        break;
                    case 25:
                        user += " is so ugly that face scanners can't recognize their face. ";
                        break;
                    case 26:
                        user += " uses Skype instead of Discord. ";
                        break;
                    case 27:
                        user += " prefers light mode. ";
                        break;
                    case 28:
                        user += " blasts their speakers in public on max volume. ";
                        break;
                    case 29:
                        user += " adds an uncomfortable amount of ranch on their pizzas. ";
                        break;
                    case 30:
                        user += " is stuck in the nether ";
                        break;
                    case 31:
                        user += " can't speak our language. They only speak idiot. ";
                        break;
                    case 32:
                        user += " has a 10$ laptop and lags in minesweeper. ";
                        break;
                    case 33:
                        user += " still uses internet explorer. ";
                        break;
                    case 34:
                        user += "'s password is 123. ";
                        break;
                    case 35:
                        user += " has a brain smaller than an ant. ";
                        break;
                    case 36:
                        user += " uses a spongebob night light. ";
                        break;
                    case 37:
                        user += " does not know the alphabet. ";
                        break;
                    case 38:
                        user += " is so dumb they get lost in supermarkets. ";
                        break;
                    case 39:
                        user += " went to the zoo recently, as the animal. ";
                        break;
                    case 40:
                        user += " has the body of a 70 year old, but the brain of a 7 year old. ";
                        break;
                    case 41:
                        user += " uses their hands to wipe their shit. ";
                        break;
                }
                await ReplyAsync(user + "");
            }
        }

        [Command("rickroll")]
        [RequireUserPermission(GuildPermission.ManageRoles, ErrorMessage = "You don't have the permission ``ManageRoles``!")]
        public async Task RickRoll(IGuildUser user)
        {
            await Context.Message.DeleteAsync();
            var EmbedBuilder = new EmbedBuilder()
                .WithDescription("Hey! You won during this Video!! \n https://www.youtube.com/watch?v=xvFZjo5PgG0")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("GG!")
                    .WithIconUrl("https://www.downloadclipart.net/large/reward-png-transparent-image.png");
                });
            Embed embed = EmbedBuilder.Build();

            await user.SendMessageAsync(embed: embed);
            await user.SendMessageAsync("Follow the Video Guide to receive the Gift!");
        }

        [Command("Blacklist")]
        [Alias("blackist", "schwarzeliste")]
        public async Task Blacklist([Remainder] string funktion = null)
        {

        }  

        [Command("say")]
        [Alias("sag", "sagen")]
        public async Task Say([Remainder] string message = null)
        {

        }
        
    }
}
