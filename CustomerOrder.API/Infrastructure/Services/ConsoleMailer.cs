using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Services;

namespace CustomerOrder.API.Infrastructure.Services;

public class ConsoleMailer : IMailer
{
    public string Send(Email email)
    {
        var lines = BuildLines(
            $"From: {email.From}",
            $"To: {email.To}",
            $"Subject: {email.Subject}",
            $"",
            email.Message
        );

        Console.WriteLine("");
        lines.ForEach(Console.WriteLine);
        Console.WriteLine("");

        return Guid.NewGuid().ToString();
    }

    private static List<string> BuildLines(params string[] lines)
    {
        if (0 == lines.Length)
        {
            return [];
        }

        List<string> lineList = ["-----[ MAILER ]-----"];

        foreach (string line in lines)
        {
            lineList.AddRange(line.Split('\n').Select(l => " " + l));
        }

        int longestLineLength = 0;
        foreach (string line in lineList)
        {
            if (longestLineLength < line.Length)
            {
                longestLineLength = line.Length;
            }
        }

        int lengthDifference = longestLineLength - lineList[0].Length;

        if (0 == lengthDifference)
        {
            return lineList;
        }

        var padding = new string('-', (lengthDifference / 2) + 1);

        lineList[0] = padding + lineList[0] + padding;
        lineList.Add(new string('-', lineList[0].Length));

        return lineList;
    }
}
