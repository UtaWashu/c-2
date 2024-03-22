using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class VkladException : Exception
{
    public VkladException(string message) : base(message) { }
}

class KolichestvoException : Exception
{
    public KolichestvoException(string message) : base(message) { }
}

abstract class Vklad
{
    private string fioVkladchika;
    private double summaVklada;
    private string name;
    private string companyName;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string CompanyName
    {
        get { return companyName; }
        set { companyName = value; }
    }

    public string FioVkladchika
    {
        get { return fioVkladchika; }
        set { fioVkladchika = value; }
    }

    public double SummaVklada
    {
        get { return summaVklada; }
        set
        {
            if (value < 0)
            {
                throw new VkladException("Невозможно создать вклад – указана отрицательная сумма вклада: " + value);
            }
            summaVklada = value;
        }
    }

    public abstract double RaschitatSummuVklada(int kolichestvoMesyac);
}

class DolgosrochnyVklad : Vklad
{
    public override double RaschitatSummuVklada(int kolichestvoMesyac)
    {
        try
        {
            if (kolichestvoMesyac < 0)
            {
                throw new KolichestvoException("Отрицательное значение количества месяцев.");
            }

            double result = SummaVklada * Math.Pow(1 + 0.05, kolichestvoMesyac / 12);

            return result;
        }
        catch (KolichestvoException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }

        return 0;
    }
}

class VkladDoVostrebovaniya : Vklad
{
    public override double RaschitatSummuVklada(int kolichestvoMesyac)
    {
        try
        {
            if (kolichestvoMesyac < 0)
            {
                throw new KolichestvoException("Отрицательное значение количества месяцев.");
            }

            double result = SummaVklada * Math.Pow(1 + 0.03, kolichestvoMesyac / 12);

            return result;
        }
        catch (KolichestvoException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }

        return 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            DolgosrochnyVklad vklad1 = new DolgosrochnyVklad();
            vklad1.Name = "Иван Иванов";
            vklad1.CompanyName = "Тинькоф банк";
            vklad1.SummaVklada = 1000;
            double result1 = vklad1.RaschitatSummuVklada(-12);
            Console.WriteLine(vklad1.Name + "\n" + vklad1.CompanyName + "\n" + "Сумма вклада: " + result1);

            Console.WriteLine("\n");
            VkladDoVostrebovaniya vklad2 = new VkladDoVostrebovaniya();
            vklad2.Name = "Иван Иванов";
            vklad2.CompanyName = "Сбербанк";
            vklad2.SummaVklada = 21000;
            double result2 = vklad2.RaschitatSummuVklada(12);
            Console.WriteLine(vklad2.Name + "\n" + vklad2.CompanyName + "\n" + "Сумма вклада: " + result2);
        }
        catch (VkladException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        catch (KolichestvoException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}