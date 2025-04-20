using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Core;
using RealEase.Infrastructure.Exceptions;
using RealEase.Infrastructure.Interfaces;
using RealEase.Persistence.Context;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(RealEaseDbContext context) : base(context) { }

    public async Task<List<Payment>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Payment> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Id == id)
               ?? throw new PaymentException("Pago no encontrado.");
    }

    public async Task<Payment> CreateAsync(Payment payment)
    {
        await _dbSet.AddAsync(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> UpdateAsync(Payment payment)
    {
        var existingPayment = await GetByIdAsync(payment.Id);
        _context.Entry(existingPayment).CurrentValues.SetValues(payment);
        await _context.SaveChangesAsync();
        return existingPayment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var payment = await GetByIdAsync(id);
        if (payment == null) return false;

        _dbSet.Remove(payment);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<Payment>> GetFilteredPaymentsAsync(DateTime? paymentdate, int? contractid, int? clientid)
    {
        var query = _context.Payments.AsQueryable();

        if (paymentdate.HasValue)
            query = query.Where(u => u.PaymentDate.Date == paymentdate.Value.Date);

        if (contractid.HasValue)
            query = query.Where(u => u.ContractId == contractid.Value);

        if (clientid.HasValue)
            query = query.Where(u => u.TenantId == clientid.Value);

        return await query.ToListAsync();
    }

}

    
