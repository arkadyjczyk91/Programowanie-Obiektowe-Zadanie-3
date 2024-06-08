using System;

public class Wektor
{
    private double[] współrzędne;

    public Wektor(byte wymiar)
    {
        współrzędne = new double[wymiar];
    }

    public Wektor(params double[] współrzędne)
    {
        this.współrzędne = współrzędne;
    }

    public double Długość => Math.Sqrt(d: (double)IloczynSkalarny(this, this));

    public byte Wymiar
    {
        get { return (byte)współrzędne.Length; }
    }

    public double this[byte i]
    {
        get { return współrzędne[i]; }
        set { współrzędne[i] = value; }
    }

    public static double? IloczynSkalarny(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
            return double.NaN;

        double suma = 0;
        for (int i = 0; i < V.współrzędne.Length; i++)
        {
            suma += V.współrzędne[i] * W.współrzędne[i];
        }
        return suma;
    }

    public static Wektor Suma(params Wektor[] wektory)
    {
        if (wektory.Length == 0)
            return null;

        byte wymiar = wektory[0].Wymiar;
        foreach (var wektor in wektory)
        {
            if (wektor.Wymiar != wymiar)
                return null; // Można również rzucić wyjątek
        }

        Wektor suma = new Wektor(wymiar);
        for (int i = 0; i < wymiar; i++)
        {
            foreach (var wektor in wektory)
            {
                suma.współrzędne[i] += wektor.współrzędne[i];
            }
        }
        return suma;
    }

    public static Wektor operator +(Wektor V, Wektor W)
    {
        return Suma(V, W);
    }

    public static Wektor operator -(Wektor V, Wektor W)
    {
        Wektor różnica = new Wektor(V.Wymiar);
        for (int i = 0; i < V.Wymiar; i++)
        {
            różnica.współrzędne[i] = V.współrzędne[i] - W.współrzędne[i];
        }
        return różnica;
    }

    public static Wektor operator *(Wektor V, double skalar)
    {
        Wektor wynik = new Wektor(V.Wymiar);
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynik.współrzędne[i] = V.współrzędne[i] * skalar;
        }
        return wynik;
    }

    public static Wektor operator *(double skalar, Wektor V)
    {
        return V * skalar;
    }

    public static Wektor operator /(Wektor V, double skalar)
    {
        if (skalar == 0)
            throw new DivideByZeroException("Nie można dzielić przez zero.");

        Wektor wynik = new Wektor(V.Wymiar);
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynik.współrzędne[i] = V.współrzędne[i] / skalar;
        }
        return wynik;
    }

    class Program
    {
        static void Main()
        {
            Wektor w1 = new Wektor(1, 2, 3);
            Wektor w2 = new Wektor(4, 5, 6);

            Console.WriteLine("Wektor w1: " + string.Join(", ", w1));
            Console.WriteLine("Wektor w2: " + string.Join(", ", w2));
            Console.WriteLine("Długość wektora w1: " + w1.Długość);
            Console.WriteLine("Długość wektora w2: " + w2.Długość);
            Console.WriteLine("Iloczyn skalarny w1 i w2: " + Wektor.IloczynSkalarny(w1, w2));
            Console.WriteLine("Suma wektorów w1 i w2: " + string.Join(", ", w1 + w2));
            Console.WriteLine("Różnica wektorów w1 i w2: " + string.Join(", ", w1 - w2));
            Console.WriteLine("Iloczyn wektora w1 przez 2: " + string.Join(", ", w1 * 2));
            Console.WriteLine("Iloczyn 3 przez wektor w2: " + string.Join(", ", 3 * w2));
            Console.WriteLine("Wektor w1 podzielony przez 2: " + string.Join(", ", w1 / 2));
        }
    }
}