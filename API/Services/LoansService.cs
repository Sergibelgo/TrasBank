﻿using APITrassBank.Context;
using APITrassBank.Models;
using AutoMapper;
using Entitys.Entity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Services
{
    public interface ILoansService
    {
        Task<Loan> CreateLoan(LoanCreateWorkerDTO model,int typeInterest);
        Task DenyLoan(string id);
        Task<IEnumerable<LoanResponseDTO>> GetAll();
        Task<LoanResponseDTO> GetById(string id);
        Task<IEnumerable<LoanResponseDTO>> GetByUserId(string idSelf);
    }
    public class LoansService : ILoansService
    {
        private readonly ContextDB _contextDB;
        private readonly IMapper _mapper;
        private readonly IEnumsService _enums;
        private readonly IResourcesService _resourcesService;

        public LoansService(ContextDB contextDB, IMapper mapper, IEnumsService enums,IResourcesService resourcesService)
        {
            _contextDB = contextDB;
            _mapper = mapper;
            _enums = enums;
            _resourcesService = resourcesService;
        }

        public async Task<Loan> CreateLoan(LoanCreateWorkerDTO model,int typeInterest)
        {
            var user = _contextDB.Customers.Where(c => c.AppUser.Id == model.CustomerId).FirstOrDefault() ?? throw new Exception("User not valid");
            var newLoan = new Loan()
            {
                Ammount = (decimal)model.Ammount,
                StartDate = DateTime.Now,
                LoanStatusId = 1,
                LoanTypeId = model.LoanTypeId,
                RemainingAmmount = (decimal)model.Ammount,
                TotalInstallments = model.TotalInstallments,
                RemainingInstallments = model.TotalInstallments,
                Customer = user
            };
            newLoan.EndDate = newLoan.StartDate.AddMonths(model.TotalInstallments);
            newLoan.InterestRate = await _resourcesService.GetResource(typeInterest);
            await _contextDB.Loans.AddAsync(newLoan);
            await _contextDB.SaveChangesAsync();
            return newLoan;

        }

        public async Task DenyLoan(string id)
        {
            Loan loan = await _contextDB.Loans.Where(x => x.Id.ToString() == id).FirstOrDefaultAsync()
                        ?? throw new ArgumentOutOfRangeException();
            loan.LoanStatusId = 3;
            await _contextDB.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanResponseDTO>> GetAll()
        {
            return await _contextDB.Loans
                .Include(x => x.Customer)
                .Include(x => x.LoanType)
                .Include(x => x.LoanStatus)
                .Select(x => _mapper.Map<LoanResponseDTO>(x)).ToListAsync();
        }

        public async Task<LoanResponseDTO> GetById(string id)
        {
            return await _contextDB.Loans
                .Include(x => x.Customer)
                .Include(x => x.LoanType)
                .Include(x => x.LoanStatus)
                .Where(x => x.Id.ToString() == id)
                .Select(x => _mapper.Map<LoanResponseDTO>(x)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LoanResponseDTO>> GetByUserId(string idSelf)
        {
            return await _contextDB.Loans
                .Include(x => x.Customer)
                .ThenInclude(x => x.AppUser)
                .Include(x => x.LoanType)
                .Include(x => x.LoanStatus)
                .Where(x => x.Customer.AppUser.Id == idSelf)
                .Select(x => _mapper.Map<LoanResponseDTO>(x)).ToListAsync();
        }

    }
}
