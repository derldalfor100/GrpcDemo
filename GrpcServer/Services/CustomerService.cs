using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService: Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            switch (request.UserId)
            {
                case 1:
                    output.FirstName = "Jack";
                    output.LastName = "Kennedy";
                    break;
                case 2:
                    output.FirstName = "Donald";
                    output.LastName = "Trump";
                    break;
                default:
                    output.FirstName = "Ronald";
                    output.LastName = "Reagan";
                    break;
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {

            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Timo",
                    LastName = "Werner",
                    EmailAddress = "timo@gmail.com",
                    Age = 25,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Yekaterina",
                    LastName = "Pavlosha",
                    EmailAddress = "yekPav@gmail.com",
                    Age = 77,
                    IsAlive = false
                },
                new CustomerModel
                {
                    FirstName = "Elizabeth",
                    LastName = "Pierce",
                    EmailAddress = "eliz18@hotmail.com",
                    Age = 64,
                    IsAlive = false
                }
            };

            foreach(var cust in customers)
            {
                await Task.Delay(1000);

                await responseStream.WriteAsync(cust);
            }
        }
    }
}
