﻿using SESFIR.DTOs;
using SESFIR.Services.Model.Service;
using SESIFIR.Services.Tests.Setup;
using NUnit.Framework;
using SESFIR.Validators.Model.Validation;
using SESFIR.Mappers;
using Microsoft.AspNetCore.Authorization;
using MySqlX.XDevAPI;
using SESFIR.DataAccess.Data.Domains;
using FluentValidation;
using SESFIR.Utils.Enums;

namespace SESIFIR.Services.Tests
{
    public class AccountsServiceTests
    {

        private AccountsService accountsService;
        private AccountsServiceMock accountsServiceMock;

        [SetUp]
        public void Initialize()
        {
            this.accountsServiceMock = new AccountsServiceMock();

            this.accountsService = new AccountsService(accountsServiceMock.Service, accountsServiceMock.Validator, accountsServiceMock.Mapper);
        }

        [Test]
        public async Task Should_Insert_and_Return_ExpectedValue() 
        {
            // arrange
            var accountDTO = new AccountsDTO()
            {
                AccountId = 0,
                UserName = "Test1",
                Email = "wewe@yahoo.com",
                Password = "m4rinic4#123D",
                PhoneNumber = "1234567890",
                Role = Role.User

            };
            this.accountsServiceMock.SetUpSearchByID(accountDTO.AccountId);
            this.accountsServiceMock.SetUpGetAll();
            this.accountsServiceMock.SetUpInsert(accountDTO);
            this.accountsServiceMock.SetUpFirstOrDefault(x => x.UserName == accountDTO.UserName);

            // act
            var account = await accountsService.InsertAsync(accountDTO);
            var accounts = await accountsService.GetAllAsync();

            // assert
            Assert.IsNotNull(account);
            Assert.IsTrue(accounts.Any(x => x.AccountId == account.AccountId));
        }

        [Test]
        public async Task Should_Insert_and_Throw_ValdiationException()
        {
            // arrange
            var accountDTO = new AccountsDTO()
            {
                AccountId = 0,
                UserName = "Test",
                Password = "23123",
                PhoneNumber = "1234567890",
                Role = Role.User

            };
            this.accountsServiceMock.SetUpSearchByID(accountDTO.AccountId);
            this.accountsServiceMock.SetUpGetAll();
            this.accountsServiceMock.SetUpInsert(accountDTO);
            try
            {
                // act
                await accountsService.InsertAsync(accountDTO);

                await accountsService.GetAllAsync();

                Assert.Fail();
            }
            catch (ValidationException ex) 
            {
                // assert

                Assert.That(ex, Is.Not.Null);

                Assert.That(ex.Message, Is.Not.Empty);  
            }

        }

        [Test]
        public async Task Should_Update_and_Return_ExpectedValue()
        {
            // arrange
            var accountDTO = new AccountsDTO()
            {
                AccountId = 0,
                UserName = "Test",
                Email = "wewe@yahoo.com",
                Password = "m4rinic4#123D",
                PhoneNumber = "1234567890",
                Role = Role.User

            };
            this.accountsServiceMock.SetUpSearchByID(accountDTO.AccountId);
            this.accountsServiceMock.SetUpGetAll();
            this.accountsServiceMock.SetUpUpdate(accountDTO);
            this.accountsServiceMock.SetUpFirstOrDefault(x => x.UserName == accountDTO.UserName);


            // act
            var initialAccount = await accountsService.SearchByIdAsync(accountDTO.AccountId);
            
            var updatedAccount = await accountsService.UpdateAsync(accountDTO);

            // assert
            Assert.IsNotNull(initialAccount);
            Assert.IsNotNull(updatedAccount);

            Assert.IsTrue(initialAccount.AccountId == updatedAccount.AccountId);

            //this test only changes the password
            Assert.AreNotSame(initialAccount.Password, updatedAccount.Password);
        }
    }
}