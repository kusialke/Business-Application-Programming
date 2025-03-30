﻿using SaleKiosk.Domain.Contracts;
using SaleKiosk.Domain.Models;

namespace SaleKiosk.Infrastructure.Repositories
{
    // Implementacja repozytoriów specyficznych
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public HotelRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }

        public int GetMaxId()
        {
            return _kioskDbContext.Hotels.Max(x => x.Id);
        }

        public bool IsInUse(string name)
        {
            var result = _kioskDbContext.Hotels.Any(x => x.Name == name);
            return result;
        }

        public IEnumerable<Hotel> GetAvailableHotels(DateTime startDate, DateTime endDate)
        {
            return _kioskDbContext.Hotels
                .Where(h => !_kioskDbContext.Orders
                    .Any(r => r.HotelId == h.Id && r.StartDate < endDate && r.EndDate > startDate))
                .ToList();
        }
    }

}
