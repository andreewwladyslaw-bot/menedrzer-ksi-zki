using System;
class Program
{
    static void Main()
    {
        var repo = new RepozytoriumKsiazek();
        var ksiazki = repo.Wczytaj();

        while (true)
        {
            Console.WriteLine("\n1. Dodaj książkę");
            Console.WriteLine("2. Pokaż wszystkie");
            Console.WriteLine("3. Usuń książkę");
            Console.WriteLine("4. Zmień status");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybierz opcję: ");
            var wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    Dodaj(ksiazki);
                    repo.Zapisz(ksiazki);
                    break;

                case "2":
                    Pokaz(ksiazki);
                    break;

                case "3":
                    Usun(ksiazki);
                    repo.Zapisz(ksiazki);
                    break;


                case "0":
                    Console.WriteLine("Dziekujemy za program.");
                    return;
            }
        }
        static void Pokaz(List<Ksiazka> ksiazki)
        {
            foreach (var k in ksiazki)
            {
                Console.WriteLine($"{k.Id}. {k.Tytul} - {k.Autor} {k.Rok} {k.Gatunek} {(k.Przeczytana ? "✔" : "❌")}");
            }
        }
        static void Usun(List<Ksiazka> ksiazki)
        {
            Console.Write("Podaj ID do usunięcia: ");
            int id = int.Parse(Console.ReadLine());

            ksiazki.RemoveAll(k => k.Id == id);
        }
    }
    static void Dodaj(List<Ksiazka> ksiazki)
    {
        var ksiazka = new Ksiazka();

        ksiazka.Id = ksiazki.Count > 0 ? ksiazki.Max(k => k.Id) + 1 : 1;

        Console.Write("Tytuł: ");
        ksiazka.Tytul = Console.ReadLine();

        Console.Write("Autor: ");
        ksiazka.Autor = Console.ReadLine();

        Console.Write("Rok: ");
        ksiazka.Rok = uint.Parse(Console.ReadLine());

        Console.Write("Gatunek: ");
        ksiazka.Gatunek = Console.ReadLine();

        ksiazka.Przeczytana = false;

        ksiazki.Add(ksiazka);
    }
}
class Ksiazka
{
    public int Id { get; set; }
    public string Tytul { get; set; }
    public string Autor { get; set; }
    public uint Rok { get; set; }
    public string Gatunek { get; set; }
    public bool Przeczytana { get; set; }

    public override string ToString()
    {
        return $"{Id},{Tytul},{Autor},{Rok},{Gatunek},{Przeczytana}";
    }

    public static Ksiazka FromString(string linia)
    {
        var dane = linia.Split(',');

        return new Ksiazka
        {
            Id = int.Parse(dane[0]),
            Tytul = dane[1],
            Autor = dane[2],
            Rok = uint.Parse(dane[3]),
            Gatunek = dane[4],
            Przeczytana = bool.Parse(dane[5])
        };
    }
}
class RepozytoriumKsiazek
{
    private string plik = "ksiazki.csv";

    public List<Ksiazka> Wczytaj()
    {
        var lista = new List<Ksiazka>();

        if (!File.Exists(plik))
            return lista;

        foreach (var linia in File.ReadAllLines(plik))
        {
            lista.Add(Ksiazka.FromString(linia));
        }

        return lista;
    }

    public void Zapisz(List<Ksiazka> ksiazki)
    {
        var linie = ksiazki.Select(k => k.ToString());
        File.WriteAllLines(plik, linie);
    }
}
