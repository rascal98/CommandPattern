using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Command
{

    public interface Command
    {
        void Execute(); //Ana komut işleme metodumuz
    }
    
    public class Light
    {
        public void On()
        {
            Console.WriteLine("Işık Açıldı");
        }
    }

    public class LightCommand : Command
    {
        private Light _light;

        public LightCommand(Light light)
        {
            _light = light;
        }

        
        public void Execute()
        {

            //Işık açıldığında yapılacak işlemler
            _light.On();
        }
    }

    //Komutları Invoker sınıfını kullanarak işliyoruz
    //Komutun neyi çalıştırdığını bilmez
    public class Invoker
    {
        private Command _command;
        private int sayac = 1;
        CommandSaver saver=new CommandSaver();
        //Ctor
        public Invoker()
        {

        }

        public void SetCommand(Command command)
        {
            
            _command = command;
        }

        public void Commands()
        {
           Console.WriteLine("");
           Console.WriteLine("İşlenen Komutlar");
           foreach (string komut in saver.Commands())
           {
               Console.WriteLine(komut);
           }
        }
       
        public void ButtonClicked()
        {
            
            
            
            _command.Execute();
            Console.WriteLine("Bir Komut işlendi");
            saver.Save("komut" + sayac);

            
            sayac++;
        }

        public void DeleteLast()
        {
            saver.DeleteLast();
        }
    }

    public class CommandSaver
    {
       
        private List<string> komutlar=new List<string>();
        public CommandSaver()
        {
            
        }

        public List<String> Commands()
        {
            return komutlar;
        }
        public void DeleteLast()
        {
            var item = komutlar.LastOrDefault();
            komutlar.Remove(item);
        }
        public void Save(string komut)
        {
            komutlar.Add(komut);
            
           
        }

        
    }



    class Program
    {
        static void Main(string[] args)
        {
            Invoker invoker = new Invoker();
            Light light = new Light();
            LightCommand lightCommand = new LightCommand(light);
            invoker.SetCommand(lightCommand);
            invoker.ButtonClicked();
            invoker.ButtonClicked();
            invoker.ButtonClicked();
            invoker.ButtonClicked();
            invoker.Commands();


            if (Console.ReadLine() == "sil")
            {
                invoker.DeleteLast();
            }

            
            invoker.Commands();

        }
    }
}
