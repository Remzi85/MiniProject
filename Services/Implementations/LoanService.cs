using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.MyException;
using Project___ConsoleApp__Library_Management_Application_.Repostories.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Repostories.Interfaces;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Implementations
{
    public class LoanService : ILoanService
    {
        public void Create(Loan loan)
        {
            if (loan is null) throw new ArgumentNullException("Loan is null");
            if (loan.Id < 1) throw new NotValidException("Id is less than 1");
            if (loan.Borrower is null) throw new ArgumentNullException("Loan is null");

            ILoanRepository loanRepository = new LoanRepository();


            loan.CreatedAt = DateTime.UtcNow.AddHours(4);
            loan.UpdatedAt = DateTime.UtcNow.AddHours(4);

            loanRepository.Add(loan);
            loanRepository.Commit();
        }

        public void Delete(int id)
        {
            ILoanRepository loanRepository = new LoanRepository();
            var data = loanRepository.GetById(id);

            if (data is null) throw new NullReferenceException();
            if (id < 1) throw new NotValidException("Id is less than 1");

            data.IsDeleted = true;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);
            loanRepository.Commit();
        }

        public List<Loan> GetAll()
        {
            ILoanRepository loanRepository = new LoanRepository();

            if (loanRepository.GetAll() is null) throw new NullReferenceException();
            return loanRepository.GetAll();
        }

        public Loan GetById(int id)
        {
            ILoanRepository loanRepository = new LoanRepository();



            if (loanRepository.GetById(id) is null) throw new NotValidException();
            if (id < 1) throw new NotValidException("Id is less than 1");

            return loanRepository.GetById(id);

        }

        public object GetLoansByBorrower(int borrowerId)
        {
            throw new NotImplementedException();
        }

        public object GetOverdueLoans()
        {
            throw new NotImplementedException();
        }

        public bool IsBorrowed(int id)
        {
            throw new NotImplementedException();
        }

        public void ReturnLoan(int loanId, DateTime utcNow)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Loan loan)
        {
            ILoanRepository loanRepository = new LoanRepository();


            var loanUpdate = loanRepository.GetById(id);

            if (loanUpdate is null) throw new NullReferenceException();
            if (id < 1) throw new NotValidException("Idis less than 1");
            if (loan is null) throw new NullReferenceException();
            if (loan.Id < 1) throw new NotValidException();

            loanUpdate.LoanItems = loan.LoanItems;
            loanUpdate.Borrower = loan.Borrower;
            loanUpdate.LoanDate = loan.LoanDate;
            loanUpdate.ReturnDate = loan.ReturnDate;
            loanUpdate.MustReturnDate = loan.MustReturnDate;
            loanUpdate.BorrowerId = loan.BorrowerId;
            loanUpdate.CreatedAt = loan.CreatedAt;
            loanUpdate.UpdatedAt = loan.UpdatedAt;
            loanUpdate.IsDeleted = loan.IsDeleted;


            loanRepository.Commit();
        }
    }
}
