//using BLL.Extention;
//using BLL.Service.Interfaces;
//using Domain;
//using Domain.Repository;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BLL.Service
//{
//    public class UpdateDiscount : IUpdateDiscount, IHostedService
//    {
//        private readonly IServiceProvider _serviceProvider;
//        public UpdateDiscount(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }
//        Timer _timmer;
//        public void Discount()
//        {
//            using var scope = _serviceProvider.CreateScope();
//            var context = scope.ServiceProvider.GetRequiredService<MyShopDb>();
//            var option = scope.ServiceProvider.GetRequiredService<IProductRepository>();
//            var uow = scope.ServiceProvider.GetRequiredService<IUOW>();

//            var data = context.Products.GetAll(false);
//            foreach (var item in data)
//            {
//                if (item.StartDiscount >= DateTime.Now && item.EndDiscount < DateTime.Now)
//                {
//                    item.HasDiscount = true;
//                }
//                else
//                {
//                    item.StartDiscount = null;
//                    item.EndDiscount = null;
//                }
//                option.Update(item);
//            }
//                uow.Save();
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            DateTime now = DateTime.UtcNow.AddHours(3);
//            DateTime nextdate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
//            if (now > nextdate)
//            {
//                nextdate=nextdate.AddDays(1);
//            }
//            TimeSpan timeUntilNextRun = nextdate - now;

//            _timmer = new Timer(DoWork, null, timeUntilNextRun, TimeSpan.FromHours(24));

//            return Task.CompletedTask;
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {   
//            _timmer?.Change(Timeout.Infinite, 0);
//            return Task.CompletedTask;
//        }
//        public void Dispose()
//        {
//            _timmer?.Dispose();
//        }
//        public void DoWork(object state)
//        {
//            Discount();
//        }

//    }
//}
