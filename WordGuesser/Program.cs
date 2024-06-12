namespace WordGuesser
{
    class Program
    {
        static void Main()
        {
            string[] txtFiles = Directory.GetFiles("resources", "*.txt");
            List<string> words = new();

            foreach (string filePath in txtFiles)
            {
                ReadWordsFromFile(filePath, words);
            }

            while (true)
            {
                var input = Console.ReadLine()?.Split(' ');
                if (input == null) continue;
                var word = input[0];
                var contain = input.Length > 1 ? input[1] : "";
                var resultList = new List<string>();
                foreach (var w in words)
                {
                    if (w.Length != word.Length) continue;
                    bool match = true;
                    for (int i = 0; i < contain.Length; i++)
                    {
                        if (!w.Contains(contain[i]))
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match == false) continue;
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == '_') continue;
                        if (word[i] != w[i])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        if (!resultList.Contains(w))
                        {
                            resultList.Add(w);
                            Console.WriteLine(w);
                        }
                    }
                }
            }
        }

        static void ReadWordsFromFile(string filePath, List<string> words)
        {
            try
            {
                using var sr = new StreamReader(filePath);
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        words.Add(parts[0]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("读取文件时出错：" + e.Message);
            }
        }
    }    
}