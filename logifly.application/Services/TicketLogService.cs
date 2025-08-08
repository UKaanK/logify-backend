using FluentValidation;
using logifly.application.DTOs;
using logifly.application.Interfaces;
using logifly.application.Validators;
using logifly.domain.Entities;
using logifly.domain.Enums;
using logifly.persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Services
{
    public class TicketLogService:ITicketLogService
    {
        private readonly LogiflyDbContext _context;
        public TicketLogService(LogiflyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TicketLogCreateDto request)
        {
            // 1) FluentValidation ile manuel doğrulama
            var validator = new TicketLogCreateDtoValidator(_context); // _context zaten serviste var
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            // 2) TicketId gerçekten var mı kontrol et
            var ticketExists = await _context.Tickets.AnyAsync(t => t.Id == request.TicketId);
            if (!ticketExists)
                throw new ArgumentException($"TicketId '{request.TicketId}' mevcut değil. Kayıt eklenemedi.");

            // 3) Enum parse kontrolü
            if (!Enum.TryParse<TicketLogType>(request.LogType, true, out var logType))
                throw new ArgumentException($"Geçersiz log türü: {request.LogType}");

            // 4) Log kaydını oluştur
            var log = new TicketLog
            {
                TicketId = request.TicketId,
                LogType = logType,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            };

            try
            {
                _context.TicketLogs.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx) when (dbEx.InnerException != null)
            {
                // Foreign key veya not null constraint gibi veritabanı hataları
                throw new InvalidOperationException(
                    $"Veritabanı hatası: {dbEx.InnerException.Message}", dbEx);
            }
            catch (Exception ex)
            {
                // Diğer beklenmedik hatalar
                throw new Exception("Ticket log eklenirken beklenmeyen bir hata oluştu.", ex);
            }
        }

        public async Task<List<TicketLogResponseDto>> GetAllAsync()
        {
            var logs = await _context.TicketLogs.OrderByDescending(t => t.CreatedAt).ToListAsync();

            return logs.Select(l => new TicketLogResponseDto
            {
                Id = l.Id,
                Content=l.Content,
                LogType=l.LogType.ToString(),
                CreatedAt=l.CreatedAt,
                CreatedBy=l.CreatedBy
            }).ToList();
        }

        public async Task<List<TicketLogResponseDto>> GetByTicketIdAsync(Guid ticketId)
        {
            var logs = await _context.TicketLogs.Where(l => l.TicketId == ticketId).OrderByDescending(l => l.CreatedAt).ToListAsync();

            return logs.Select(l => new TicketLogResponseDto
            {
                Id = l.Id,
                Content = l.Content,
                LogType = l.LogType.ToString(),
                CreatedAt = l.CreatedAt,
                CreatedBy = l.CreatedBy
            }).ToList();
        }
    }
}
