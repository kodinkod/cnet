using System.Net;
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Metrics;


public class mobile_client
{
    public string name{ get; set; }
    public string phone_number { get; set; }
    public bool is_vip { get; set; }
    public bool is_block { get; set; }
    public int region { get; set; }

    public mobile_client(string line)
    {
        string[] pre_line = line.Split();
        name = pre_line[0];
        phone_number = pre_line[1];
        is_vip =  (pre_line[2]=="true");
        is_block = (pre_line[3]=="false");
        region = Convert.ToInt32(pre_line[4]);
    }


}

public class Person{
    public double balance { get; set; } = 10.0;
    public List<mobile_client> self_contact = new List<mobile_client>();
    public List<string> history = new List<string>();



    public void add_contact(List<string> contacts)
    {
        for (int i = 0; i < contacts.Count(); i++){
            self_contact.Add(new mobile_client(contacts[i]));
        }
    }

    public void print_contats()
    {
        for (int i = 0; i < self_contact.Count(); i++)
        {
            Console.WriteLine($"{i + 1}. {self_contact[i].name}, phone number: {self_contact[i].phone_number}");
        }

    }

    public void call_to(string number)
    {
        int target_index = finde_in_contact(number);
        if (target_index == -1)
        {
            Console.WriteLine("Номер не найден!");
        }
        else
        {
            mobile_client target_client = self_contact[target_index];
            if (target_client.is_block)
            {
                Console.WriteLine($"Контакт {target_client.name}: {target_client.phone_number} ЗАБЛОКИРОВАН!");
            }
            else
            {
                double price = count_price(target_client);
                if ((balance - price)>+0.0)
                {
                    add_history(target_client.phone_number, price);
                    Console.WriteLine($"Совершен звонок {target_client.name}, стоимость звонка {price}");
                    balance = balance - price;
                    Console.WriteLine($"Баланс {balance}");
                }
                else
                {
                    Console.WriteLine($"У вас не хватает на звонок; цена звонка: {price}, баланс {balance}");
                }


            }
        }


    }

    public void print_history()
    {
        if (history.Count() == 0)
        {
            Console.WriteLine("нет звонков");
        }
        else
        {
            for (int i = 0; i < history.Count(); i++)
            {
                Console.WriteLine(history[i]);
            }

        }


    }

    public void add_history(string number, double price)
    {
        string his = number + " " + price;
        history.Add(his);
    }


    public int finde_in_contact(string number)
    {
        // return index number in contact list
        for (int i = 0; i < self_contact.Count(); i++)
        {
            if (self_contact[i].phone_number == number)
            {
                return i;
            }
        }

        return -1;

    }

    public double count_price(mobile_client client)
    {
        double price = 0.0;
        if(client.phone_number.Substring(0,4) == "8800")
        {
            return price;
        }
        else if(!client.is_vip)
        {
            price = 3.0 + 0.5*client.region;
        }
        else
        {
            price = (3.0 + 0.5 * client.region)*1.5;
        }
        return price;
    }


}



internal class Program
{
    static void Main(string[] args)
    {
        // create database
        List<string> db= new List<string>();
        db.Add("Денис 89041111111 true false 2");
        db.Add("Алексей 89041113111 false true 4");
        db.Add("Ваня 880041141111 false false 4");
        db.Add("Данил 89041121111 true true 1");
        db.Add("Данил1 88001121411 false true 5");
        db.Add("Данил2 88001121411 true true 2");

        Person Denis = new Person();
        Denis.add_contact(db);

        Console.WriteLine("Contacts:");
        Denis.print_contats();

        // dialog
        string choice = "1";
        string number = "";

        Console.WriteLine("1 - показать баланс; \n 2 - просить позвонить; \n 3 - отжать мобилу; \n");

        while (choice != "3")
        {
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine($"Ваш баланс: {Denis.balance}");
                    break;

                case "2":
                    Console.WriteLine("Введите номер телефона:");
                    number = Console.ReadLine();
                    Denis.call_to(number);
                    break;

                case "3":
                    break;

                case "4":
                    Denis.print_history();
                    break;

            }
        }




    }
}