

while (true)
{
    Console.Write("\nПервая строка, exit - выход: ");
    string? s1 = Console.ReadLine();
    if (s1?.ToLower() == "exit") break;

    Console.Write("Вторая строка: ");
    string? s2 = Console.ReadLine();

    if (s1 == null || s2 == null) continue;

    int lev = Levenshtein(s1, s2);

    int dam = DamerauLevenshtein(s1, s2);

    Console.WriteLine("Алгоритм Левенштейна: " + lev + " Алгоритм Дамерау-Левенштейна: " + dam+"\n");
}

static int Levenshtein(string a, string b)
{
    int n = a.Length, m = b.Length;
    int[,] d = new int[n + 1, m + 1];

    for (int i = 0; i <= n; i++) d[i, 0] = i;
    for (int j = 0; j <= m; j++) d[0, j] = j;

    for (int i = 1; i <= n; i++)
        for (int j = 1; j <= m; j++)
        {
            int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
            d[i, j] = Math.Min(
                Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                d[i - 1, j - 1] + cost);
        }

    return d[n, m];
}

static int DamerauLevenshtein(string a, string b)
{
    int n = a.Length, m = b.Length;
    int[,] d = new int[n + 1, m + 1];

    for (int i = 0; i <= n; i++) d[i, 0] = i;
    for (int j = 0; j <= m; j++) d[0, j] = j;

    for (int i = 1; i <= n; i++)
        for (int j = 1; j <= m; j++)
        {
            int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;

            d[i, j] = Math.Min(
                Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                d[i - 1, j - 1] + cost);

            if (i > 1 && j > 1 && a[i - 1] == b[j - 2] && a[i - 2] == b[j - 1])
                d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + 1);
        }

    return d[n, m];
}