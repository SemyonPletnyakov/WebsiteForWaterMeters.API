using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteForWaterMeters.API.EFCore;
using WebsiteForWaterMeters.API.EFCore.Tables;

namespace WebsiteForWaterMeters.API.Services
{
    public class MetersService
    {
        ApplicationContext bd;
        public MetersService()
        {
            bd = new ApplicationContext();
        }
        public Person GetMetersById(int id)
        {
            Person person = bd.Persons.FirstOrDefault(c => c.Id == id);
            return person;
        }
        public List<string> GetGoroda()
        {
            List<string> goroda = bd.Persons.Select(p =>  p.Gorod ).Distinct().ToList();
            return goroda;
        }
        public List<string> GetUlici(string gorod)
        {
            List<string> ulici = bd.Persons.Where(p =>p.Gorod==gorod).Select(p => p.Ulica).Distinct().ToList();
            return ulici;
        }
        public List<string> GetDoma(string gorod, string ulica)
        {
            List<string> doma = bd.Persons.Where(p => p.Gorod == gorod && p.Ulica == ulica).Select(p => p.Dom).Distinct().ToList();
            return doma;
        }
        public List<string> GetKvartiry(string gorod, string ulica, string dom)
        {
            List<string> doma = bd.Persons.Where(p => p.Gorod == gorod && p.Ulica == ulica && p.Dom == dom).Select(p => p.Kvartira).Distinct().ToList();
            return doma;
        }
        public Person GetUserByAdres(string gorod, string ulica, string dom, string kvartira)
        {
            if (kvartira != null)
                if (kvartira.Length > 0)
                {
                    Person person = bd.Persons.FirstOrDefault(c => c.Gorod == gorod && c.Ulica == ulica && c.Dom == dom && c.Kvartira == kvartira);
                    return person;
                }
                
            
                Person person1 = bd.Persons.FirstOrDefault(c => c.Gorod == gorod && c.Ulica == ulica && c.Dom == dom && c.Kvartira == "0");
                if(person1!=null) return person1;

                person1 = bd.Persons.FirstOrDefault(c => c.Gorod == gorod && c.Ulica == ulica && c.Dom == dom);
                if (person1 != null) return person1;

                if (person1 != null)
                {
                    person1 = bd.Persons.FirstOrDefault(c => c.Gorod == gorod && c.Ulica == ulica && c.Dom == "0");
                    return person1;
                }

                person1 = bd.Persons.FirstOrDefault(c => c.Gorod == gorod && c.Ulica == ulica);
                return person1;
        }
    }
}
