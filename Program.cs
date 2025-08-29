namespace PunchlineFactory;

class Program
{
    static Func<string, string> Compose(params Func<string, string>[] steps)
    {
        return x =>
        {
            string cur = x;

            for (int i = 0; i < steps.Length; i++)
            {
                cur = steps[i](cur);
            }

            return cur;
        };
    }

    static int Rate(string text, Func<string, int> scorer)
    {
        return scorer(text);
    }
    
    
    
    static void Main(string[] args)
    {
        string topic = "Колобок повесиля";
        int irony = 8;

        Func<string, int, string> crafter = (t, level) =>
            $"Почему {t}? Потому что иначе кто-то обидется. Уровень иронии: {level} / 10";

        Func<string, string> addEmoji = x => x + " xD";
        Func<string, string> addHashtags = x => x + " #funny";
        Func<string, string> makeAllCaps = x => x.ToUpper();
        
        var pipeline = Compose(addEmoji, addHashtags);

        string joke = pipeline(crafter(topic, irony));

        int score = Rate(joke, x =>
        {
            int baseScore = x.Split(' ').Length / 3;

            if (x.Contains("ci"))
            {
                baseScore += 2;
            }
            
            return Math.Clamp(baseScore, 0, 10);
        });

        Console.WriteLine("Generated joke:");
        
        Console.WriteLine(joke);
        Console.WriteLine($"Оценка: {score} / 10");
    }
}