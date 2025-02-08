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
    public class LoanItemService : ILoanItemService
    {
        public void Create(LoanItem loanItem)
        {
            if (loanItem is null) throw new ArgumentNullException("LoanItem is null");
            if (loanItem.Id < 1) throw new NotValidException("Id is less than 1");
            if (loanItem.Loan is null) throw new ArgumentNullException("Loan is null");

            ILoanItemRepository loanItemRepository = new LoanItemRepository();


            loanItem.CreatedAt = DateTime.UtcNow.AddHours(4);
            loanItem.UpdatedAt = DateTime.UtcNow.AddHours(4);

            loanItemRepository.Add(loanItem);
            loanItemRepository.Commit();
        }

        public void Delete(int id)
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();
            var data = loanItemRepository.GetById(id);

            if (data is null) throw new NullReferenceException();
            if (id < 1) throw new NotValidException("Id is less than 1");

            data.IsDeleted = true;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);
            loanItemRepository.Commit();
        }

        public List<LoanItem> GetAll()
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();

            if (loanItemRepository.GetAll() is null) throw new NullReferenceException();
            return loanItemRepository.GetAll();
        }

        public LoanItem GetById(int id)
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();

            if (loanItemRepository.GetById(id) is null) throw new NotValidException();
            if (id < 1) throw new NotValidException("Id is less than 1");

            return loanItemRepository.GetById(id);

        }

        public void Update(int id, LoanItem loanItem)
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();
            var loanItemUpdate = loanItemRepository.GetById(id);

            if (loanItemUpdate is null) throw new NullReferenceException();
            if (id < 1) throw new NotValidException("Idis less than 1");
            if (loanItem is null) throw new NullReferenceException();
            if (loanItem.Id < 1) throw new NotValidException();


            loanItemUpdate.Loan = loanItem.Loan;
            loanItemUpdate.Book = loanItem.Book;
            loanItemUpdate.UpdatedAt = loanItem.UpdatedAt;
            loanItemUpdate.CreatedAt = loanItem.CreatedAt;
            loanItemUpdate.BookId = loanItem.BookId;
            loanItemUpdate.IsDeleted = loanItem.IsDeleted;

            loanItemRepository.Commit();
        }
    }
    
}
