using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Threading.Tasks;

namespace IssueRepro
{
    public class Program
    {
        static void Main(string[] args)
        {
            DbInterception.Add(new Interceptor());
            Task.Run(async () =>
            {
                try
                {
                    var data = await GetLocalDataAsync(1, 0);
                    Console.WriteLine("It worked!!");
                } catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            });
            Console.ReadLine();
            return;
        }

        public delegate IQueryable<TResult> Filter<TModel, TResult>(TModel model, IQueryable<TResult> query);



        public static async Task<List<Person_WithMoreInfo>> GetLocalDataAsync(int companyId, int arg1)
        {
            using (var db = new SimplifiedModelEntities())
            {
                return await FetchAndFilterAsync(GetPersonQueryableForList(db, companyId));
            }
        }

        public static async Task<List<Person_WithMoreInfo>> FetchAndFilterAsync(IQueryable<Person_WithMoreInfo> query)
        {
            return await query
                 .OrderBy(x => x.Name)
                 .ToListAsync();
        }

        private static IQueryable<Person_WithMoreInfo> GetPersonQueryableForList(SimplifiedModelEntities db, int companyId)
        {
            var query = from a in db.Person_WithMoreInfo
                        join b in db.Person_GetForCompany(companyId) on a.Person_Id equals b.Person_Id
                        select a;


            query = query.Where(fs => db.Companies.Any(x => x.Company_Id == fs.Company_Id));

            return query;
        }
    }
}
