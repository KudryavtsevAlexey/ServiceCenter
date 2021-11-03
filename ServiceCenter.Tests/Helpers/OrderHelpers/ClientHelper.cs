using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;

namespace ServiceCenter.Tests.Helpers.OrderHelpers
{
    public class ClientHelper
    {
        public static Client GetClient()
		{
			return new Client()
			{
				ClientId = 1,
				FirstName = "ClientFirstName1",
				LastName = "ClientLastName1",
				Email = "ClientEmail1@mail.ru",
			};
		}

        public static ClientViewModel GetClientViewModel()
		{
			return new ClientViewModel()
			{
				FirstName = "ClientFirstName1",
				LastName = "ClientLastName1",
				Email = "ClientEmail1@mail.ru",
			};
		}

        public static List<Client> GetManyClients()
		{
			return new List<Client>()
			{
				new Client()
				{
					ClientId = 2,
					FirstName = "ClientFirstName2",
					LastName = "ClientLastName2",
					Email = "ClientEmail2@mail.ru",
				},

				new Client()
				{
					ClientId = 3,
					FirstName = "ClientFirstName3",
					LastName = "ClientLastName3",
					Email = "ClientEmail3@mail.ru",
				},

				new Client()
				{
					ClientId = 4,
					FirstName = "ClientFirstName4",
					LastName = "ClientLastName4",
					Email = "ClientEmail4@mail.ru",
				},

				new Client()
				{
					ClientId = 5,
					FirstName = "ClientFirstName5",
					LastName = "ClientLastName5",
					Email = "ClientEmail5@mail.ru",
				},

				new Client()
				{
					ClientId = 6,
					FirstName = "ClientFirstName6",
					LastName = "ClientLastName6",
					Email = "ClientEmail6@mail.ru",
				},
			};
		}

		public static List<ClientViewModel> GetManyClientViewModels()
		{
			return new List<ClientViewModel>()
			{
				new ClientViewModel()
				{
					FirstName = "ClientFirstName2",
					LastName = "ClientLastName2",
					Email = "ClientEmail2@mail.ru",
				},

				new ClientViewModel()
				{
					FirstName = "ClientFirstName3",
					LastName = "ClientLastName3",
					Email = "ClientEmail3@mail.ru",
				},

				new ClientViewModel()
				{
					FirstName = "ClientFirstName4",
					LastName = "ClientLastName4",
					Email = "ClientEmail4@mail.ru",
				},

				new ClientViewModel()
				{
					FirstName = "ClientFirstName5",
					LastName = "ClientLastName5",
					Email = "ClientEmail5@mail.ru",
				},

				new ClientViewModel()
				{
					FirstName = "ClientFirstName6",
					LastName = "ClientLastName6",
					Email = "ClientEmail6@mail.ru",
				},
			};
		}
    }
}
