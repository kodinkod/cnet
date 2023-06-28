

public abstract class Person{
    public string Name { get; }
    public Gender whu { get; }
    public DateTime birthday { get;}
    public Weapon weap { get; set;}


    public int HeadPoint { get;} // максимальное
    public int current_hp { get; set; }
    public int experience_points { get; set; }

    public int X { get; set; }
    public int Y { get; set; }

    public Person(string name, Gender whus, DateTime d_of_b, Weapon weap_you, int hp, int exp){
        Name = name;
        whu = whus;
        birthday = d_of_b;
        weap = weap_you;
        HeadPoint = hp;
        current_hp = HeadPoint;
        experience_points = exp;
        X = 0;
        Y = 0;
    }

    public void Move(int p_X, int p_Y){
        if(current_hp < 0){
            Console.WriteLine($"Персонаж {Name} Мертв!");
            return;
        }

        X += p_X;
        Y += p_Y;
        Console.WriteLine($"Персонаж {Name} сделал ход на позицию X: {X}, Y: {Y}!");
    }

    public void attac(Person on_attac){
        if (current_hp < 0){
            Console.WriteLine($"Персонаж {Name} Мертв!");
            return;
        }

        int old_hp = on_attac.current_hp;
        if (on_attac.X == X && on_attac.Y == Y){
            Console.WriteLine($" СУПППЕР АТАКАААААА на {on_attac.Name} X2 !");
            Console.WriteLine($" ОПЫТ {Name} X2 !");

            on_attac.current_hp -= (weap.dmg*2);
            experience_points += 2; // повышаем опыт
            Console.WriteLine($"Персонаж {Name} нанес домаг {weap.dmg*2}, теперь у  {on_attac.Name} hp: {old_hp} -> {on_attac.current_hp}!");
        }else{
            on_attac.current_hp -= weap.dmg;
            experience_points += 1; // повышаем опыт
            Console.WriteLine($"Персонаж {Name} нанес домаг  {weap.dmg}, теперь у  {on_attac.Name} hp: {old_hp} -> {on_attac.current_hp}!");
        }

        if (on_attac.current_hp < 0){
            Console.WriteLine($"{on_attac.Name} УБИТ");
        }
    }


    public void fit(){
        if (current_hp < 0){
            Console.WriteLine($"Персонаж {Name} Мертв!");
            return;
        }

        experience_points += 10;
        Console.WriteLine($" Тренируем {Name},  exp: {experience_points} !");
    }

}


public class Simple_warrior : Person{
    public Simple_warrior(string name, Gender whus, DateTime d_of_b, Weapon weapYou, int hp, int exp) : base(name, whus, d_of_b, weapYou, hp, exp) { }
}

public class God: Person{
    public God(string name, Gender whus, DateTime d_of_b, Weapon weapYou, int hp, int exp) : base(name, whus, d_of_b, weapYou, hp, exp) { }
}


// базовый класс оружия
public class Weapon{
    public string Name { get; set; }
    public int dmg { get; set; }

    public Weapon(string name, int dmg){
        Name = name;
        this.dmg = dmg;
    }
}

public enum Gender{
    male, female, troll,
}




internal class Program
{
    static void Main(string[] args){
        DateTime data = new DateTime(2002, 7, 20);

        // Создаем оружия
        Weapon onion = new Weapon("Зачарованный лук", 20);
        Weapon axe = new Weapon("Топор", 10);

        // создаем игроков
        Simple_warrior Denis = new Simple_warrior("Denis", Gender.male, data, onion, 100, 200);
        Simple_warrior Warvor = new Simple_warrior("Варвар", Gender.troll, data, axe, 50, 100);

        // Игровые действия
        Denis.attac(Warvor);
        Console.WriteLine("------------------------------------");
        Warvor.attac(Denis);
        Console.WriteLine("------------------------------------");

        Warvor.Move(1, 1);
        Console.WriteLine("------------------------------------");

        Denis.attac(Warvor);
        Console.WriteLine("------------------------------------");

        Warvor.Move(1, 1);
        Console.WriteLine("------------------------------------");

    }
}


