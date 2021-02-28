using agenda.Data;
using agenda.Models;
using agenda.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agenda.Services
{
    public class ContactService
    {
        private readonly AgendaContext _context;

        public ContactService(AgendaContext context)
        {
            _context = context;
        }

        public List<Contact> FindAll()
        {
            return _context.Contact.ToList();
        }

        public void Insert(Contact obj)
        {
            try
            {
                _context.Add(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public Contact FindById(int id)
        {
            return _context.Contact.FirstOrDefault(obj => obj.Id == id);
        }

        public void Update(Contact obj)
        {
            if (!_context.Contact.Any(c => c.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                var obj = _context.Contact.Find(id);
                _context.Contact.Remove(obj);
                _context.SaveChanges();
            }
            catch (IntegrityException e)
            {
                throw new IntegrityException("Contact can't be deleted");
            }
        }
    }
}
