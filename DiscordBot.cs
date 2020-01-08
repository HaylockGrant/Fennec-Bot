using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using DSharpPlus.VoiceNext.Codec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace Fennec
{
    class Program
    {
        static DiscordClient discord;
        public static VoiceNextClient voice;
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[]    args)
        {
            DiscordUser complimentedCutie = null;
            int complimentIteration = 0;
            Random rnd = new Random();
            Stream imgyus = new MemoryStream(Properties.Resources._68964a1);
            DiscordChannel currentvoicechannel = null;
            Console.WriteLine("Starting server...");
            discord = new DiscordClient(new DiscordConfiguration
            {

                Token = "Secret Token",
                TokenType = TokenType.Bot,
            }
            );
            voice = discord.UseVoiceNext();
            Console.WriteLine("Initilizing Client");
            discord.Ready += async e =>
            {
                Console.WriteLine("Server Ready!");
                await discord.UpdateStatusAsync(new DiscordGame("with herself~"));
            };
            voice.Client.MessageCreated += async e =>
            {
                if (e.Message.Content.StartsWith("-"))
                {
                    String messagesent = e.Message.Content.TrimStart('-');
                    String commandsent;
                    if (messagesent.Contains(' '))
                    {
                        commandsent = messagesent.Remove(messagesent.IndexOf(' ')).Trim().ToLower();
                    }
                    else
                    {
                        commandsent = messagesent.Trim().ToLower();
                    }
                    Console.WriteLine();
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Command Information:");
                    Console.WriteLine(" Sender = " + e.Author.Username);
                    Console.WriteLine(" Message = " + e.Message.Content);
                    Console.WriteLine(" Command sent = " + commandsent);
                    switch (commandsent)
                    {
                        case "compliment":
                            Console.WriteLine(" Name = Compliment");
                            switch (e.Author.Username)
                            {
                                case "SpaceCadetKitty":
                                    await e.Message.RespondAsync("<@" + e.Author.Id + "> Who's a cute kitty?");
                                    break;
                                case "WatermelonFennec":
                                    await e.Message.RespondAsync("<@" + e.Author.Id + "> Who's a cute fennec?");
                                    break;
                                case "KingMuskDeer":
                                    await e.Message.RespondAsync("<@" + e.Author.Id + "> Who's a cute frumpy little deer?");
                                    break;
                                case "wizard":
                                    await e.Message.RespondAsync("<@" + e.Author.Id + "> Who's a cutie pie?");
                                    break;
                                default:
                                    await e.Message.RespondAsync("<@" + e.Author.Id + "> I'm sure you have a cute personality!");
                                    break;
                            }
                            complimentedCutie = e.Author;
                            complimentIteration = 3;
                            break;
                        case "lewd":
                            Console.WriteLine(" Name = lewd");
                            switch (e.Author.Username)
                            {
                                case "SpaceCadetKitty":
                                    await e.Message.RespondAsync("*Nibbles your neck* UwU");
                                    break;
                                case "WatermelonFennec":
                                    await e.Message.RespondAsync("*Plays with your thingie* ^w^");
                                    break;
                                case "wizard":
                                    await e.Message.RespondAsync("*Licks lips* So cute!");
                                    break;
                                default:
                                    await e.Message.RespondAsync("UwU what's this?");
                                    break;
                            }
                            break;
                        case "ping":
                            Console.WriteLine(" Name = ping");
                            await e.Message.RespondAsync($"Pong!: My ping is {discord.Ping}ms");
                            break;
                        case "blow":
                            Console.WriteLine(" Name = blow");
                            if (e.MentionedUsers.Count == 1)
                            {
                                Console.WriteLine(" Destination = " + e.MentionedUsers[0].Username);
                                if (e.MentionedUsers[0].IsBot)
                                {
                                    await e.Message.RespondAsync("<@" + e.Author.Id + "> Don't be silly. You can't blow a bot!");
                                }
                                else
                                {
                                    await e.Message.RespondAsync("Hear that <@" + e.MentionedUsers[0].Id + ">? <@" + e.Author.Id + "> wants to blow you!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Exception = invalid user(s)");
                            }
                            break;
                        case "cuddle":
                            List<String> discorduserslist = new List<String>();
                            Console.WriteLine(" Name = cuddle");
                            if (e.MentionedUsers.Count > 0)
                            {
                                Console.WriteLine(" Destinations:");
                                String sendingmessage = "";
                                foreach (DiscordUser usr in e.MentionedUsers)
                                {
                                    if (!discorduserslist.Contains(usr.Username))
                                    {
                                        Console.WriteLine("  " + usr.Username);
                                        sendingmessage += "<@" + usr.Id + ">, ";
                                        discorduserslist.Add(usr.Username);
                                    }
                                }
                                sendingmessage += "<@" + e.Author.Id + "> wants to cuddle up with you!";
                                await e.Message.RespondAsync(sendingmessage);
                            }
                            else
                            {
                                Console.WriteLine("Exception = too few users");
                            }
                            List<String> cuties = new List<String>() { "wizard", "SpaceCadetKitty", "WatermelonFennec", "Newt_Salad" };
                            if (cuties.Contains(e.Author.Username) && !(e.Author.Username.Equals("wizard") && discorduserslist.Contains("WatermelonFennec")))
                            {
                                await e.Message.RespondAsync("So cute!");
                            }
                            break;
                        case "join":
                            Console.WriteLine(" Name = join");
                            //TODO implement later
                            break;
                        case "yus":
                            Console.WriteLine(" Name = yus");
                            await e.Message.RespondWithFileAsync(imgyus,"yus.jpg");
                            break;
                        case "help":
                            Console.WriteLine(" Name = help");
                            await e.Message.RespondAsync("```css\n" +
                                "#The-Fennec-Bot-Commands-are\n" +
                                "[-compliment] The Fennec Bot will pay you a compliment!\n" +
                                "[-lewd] The Fennec Bot will say somthing lewd UwU\n" +
                                "[-ping] The Fennec Bot will tell you its ping\n" +
                                "[-blow @username] You blow someone, so hot!\n" +
                                "[-cuddle @usernames] You cuddle up with someone, or multiple people!\n" +
                                "[-yus] yus\n" +
                                "[-join] New experimental feature that will play bad songs and memes!\n" +
                                "[-help] The Fennec Bot will list off all the commands for you```");
                            break;
                        //TODO add elaberate help command
                        //TODO add hug command
                        //TODO add knock knock command
                        default:
                            Console.WriteLine("Exception = invalid command");
                            break;
                    }
                    Console.WriteLine("------------------------");
                }
                if (e.Message.Author.IsBot && e.Message.Content.Equals("... but you can damn well try! >:3"))
                {
                    Console.WriteLine();
                    Console.WriteLine("------------------------");
                    Console.WriteLine("CODE RED, MADELINE BOT IS BEING LEWD!!!!");
                    switch (rnd.Next(4))
                    {
                        case 0:
                            Console.WriteLine("Fire the torpedoes!");
                            await e.Message.RespondAsync("UwU");
                            break;
                        case 1:
                            Console.WriteLine("Lets get kinky!");
                            await e.Message.RespondAsync("UwU");
                            break;
                        case 2:
                            Console.WriteLine("OH FUCK, OH SHIT, I DON'T KNOW WHAT TO DO!");
                            await e.Message.RespondAsync("OwO");
                            break;
                        case 3:
                            Console.WriteLine("OMG OMG SO CUTE!");
                            await e.Message.RespondAsync("OwO");
                            break;
                        default:
                            Console.WriteLine("Exception = out of bounds");
                            break;
                    }
                    Console.WriteLine("------------------------");
                }
                if(complimentedCutie != null)
                {
                    complimentIteration--;
                    if (e.Author.Equals(complimentedCutie) && e.Message.Content.ToLower().StartsWith("me"))
                    {
                        Console.WriteLine();
                        Console.WriteLine("------------------------");
                        Console.WriteLine("Complimented cutie(" + complimentedCutie.Username + ") is questioning cuteness!");
                        Console.WriteLine("Sending secondary compliment!");
                        Console.WriteLine("------------------------");
                        await e.Message.RespondAsync("<@" + complimentedCutie.Id + "> It is you! You're the cutie!");
                        complimentIteration = 0;
                    }
                    if (complimentIteration == 0)
                    {
                        complimentedCutie = null;
                    }
                }
                //TODO Later if someone tries to message the other bot and its offline, reply with "sorry this bot is offline"
            };
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Discord_SocketOpened1()
        {
            throw new NotImplementedException();
        }

        private static Task Discord_SocketOpened()
        {
            throw new NotImplementedException();
        }
    }
}
