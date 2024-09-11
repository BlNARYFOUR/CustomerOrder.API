using CustomerOrder.API.Domain.Services;

namespace CustomerOrder.API.Infrastructure.Services;

public class ConsoleMailer : IMailer
{
    private readonly string _mailFrom = "noreply@test.test";

    public void Send(string mailTo, string subject, string message)
    {
        var lines = BuildLines(
            $"From: {_mailFrom}",
            $"To: {mailTo}",
            $"Subject: {subject}",
            $"",
            message
        );

        Console.WriteLine("");
        lines.ForEach(line => Console.WriteLine(line));
        Console.WriteLine("");
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
