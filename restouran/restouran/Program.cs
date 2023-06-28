using System.Net;
using System.Reflection;
using System.Xml.Linq;
using System.IO;



// базовый класс элемента меню
public class Menu_item{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }

    public Menu_item(string[] line){
        Name = line[0];
        Price = Convert.ToInt32(line[1]);
        Type = line[2];
    }

    public bool is_primary()
    {
        return Type == "А";
    }
}

public class Restaurant{
    public string patch_menu { get; }

    public List<Menu_item> primary_menus = new List<Menu_item>();
    public List<Menu_item> usually_menus = new List<Menu_item>();


    public Restaurant(string Patch){
        patch_menu = Patch;
        
    }

    public void create_menu(){
        string[] pre_str = File.ReadAllLines(patch_menu);

        foreach (string line in pre_str) {
            //Console.WriteLine(line);
            string[] pre_line = line.Split();

            Menu_item curr_menu = new Menu_item(pre_line);

            if(curr_menu.is_primary()){
                primary_menus.Add(curr_menu);
            }
            else{
                usually_menus.Add(curr_menu);
            }
        }
    }

    public void print_menu(string type_m){
        Console.WriteLine(primary_menus[0].Name);
        if(type_m == "А"){
            
            for(int i=0; i< primary_menus.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {primary_menus[i].Name}; {primary_menus[i].Price}");
            }

        }else{
            for (int i = 0; i < usually_menus.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {usually_menus[i].Name}; {usually_menus[i].Price}");
            }
        }

        Console.WriteLine("Завершить заказ - -1");
    }

    public Menu_item get_item_menu(string player_сh, string player_type)
    {        int index_menu = Convert.ToInt32(player_сh);
        if(player_type == "А")
        {
            return primary_menus[index_menu - 1];
        }
        else
        {
            return usually_menus[index_menu - 1];
        }

    }

    public List<Menu_item> get_menu(string type_m)
    {
        if (type_m == "А")
        {
            return primary_menus;
        }
        else
        {
            return usually_menus;
        }

    }

}



internal class Program
{
    static void Main(string[] args)
    {
        Restaurant stolovka = new Restaurant("/Applications/programming/C-net/restouran/menu1.txt");
        stolovka.create_menu();
        Console.WriteLine("------------------------------------");

        // за кого игираем 
        Console.WriteLine("За кого будутет играть - A:Богач или Б:Бедняк?");
        string player_type = Console.ReadLine();

        while (player_type != "А" && player_type != "Б"){
            Console.WriteLine("За кого будутет играть - A:Богач или Б:Бедняк?");
            player_type = Console.ReadLine();
        }



        // СКОЛЬКО У КОГО ДЕНЕГ:
        double plauer_gold = 1000;
        double plauer_usual = 1500;

        // печатать меню
        Console.WriteLine("Что будете заказывать?");
        List<Menu_item> t_menu = stolovka.get_menu(player_type); // получаем меню
        stolovka.print_menu(player_type);





        // делаем заказ
        string player_сh = Console.ReadLine();
        List<Menu_item> current_menu = new List<Menu_item>();

        // обнуляем каунтсы
        int[] counts = new int[stolovka.get_menu(player_type).Count + 2];
        for (int i = 0; i < counts.Count(); i++)
        {
            counts[i] = 0;
        }



        // цикл производства заказа 
        while (player_сh != "-1"){
            Console.WriteLine(stolovka.get_item_menu(player_сh, player_type).Name);

            current_menu.Add(stolovka.get_item_menu(player_сh, player_type));
            counts[Convert.ToInt32(player_сh)-1] += 1;

            Console.WriteLine("Что будете заказывать?");
            player_сh = Console.ReadLine();
        }


        // Считаем стоимость заказа
        Console.WriteLine("Ваш заказ:");
        Console.WriteLine("------------------------------------");

  

        string output = "";
        double sum = 0;
        double cur_sum = 0;
        int cur_count = 0;
        for (int i = 0; i < t_menu.Count; i++){
            cur_count = counts[i];
            cur_sum = 0;
            output = " ";
            if (cur_count > 0)
            {
                output = output + t_menu[i].Name + "x" + cur_count + " ";
            }

            // обработка стоимости
            cur_sum += cur_count * t_menu[i].Price;
            if (cur_count ==2)
            {
                cur_sum = cur_sum * 0.9;
                output = output + "скидка 10%, заказ 2x; ";
            }else if (cur_count == 3)
            {
                cur_sum = cur_sum * 0.8;
                output = output + "скидка 20%, заказ 3x; ";
            }


            // ИТОГО:
            if (cur_count > 0)
            {
                output = output + "-------------" + cur_sum;
                Console.WriteLine(output);
            }
   
            sum += cur_sum;

        }


        Console.WriteLine($"сумма: {sum} ");

        if (sum > 1000){
            Console.WriteLine("скидка 30% на заказ от 1000;");
            sum = sum * 0.7;
        }

         // если багач, то x2
        if (player_type == "А"){
            Console.WriteLine("Вы богач X2;");
            sum = sum * 2;
        }

        Console.WriteLine($"к оплате: {sum} ");


        double diff = 0;
        if (player_type == "А")
        {
            if (sum > plauer_gold)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"У вас не хватает {sum - plauer_gold} денег, но мы вас отпускаем!");

            }

        }
        else
        {
            if (sum > plauer_usual)
            {
                diff = sum - plauer_usual;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"У вас не хватает {diff} денег, вы будете отрабатывать!");
                int days = 0;
                while (diff > 0)
                {
                    diff -= days + 10;
                    days++;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Вам нужно отрабатывать {days} дней!");
            }
        }


        //чтобы консоль не закрывала 
        Console.ReadLine();


    }
}