using MediatR;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands;

public class ChangeQuotationCommand : IRequest<int>
{
    public int Id { get; private set; }
    public IEnumerable<QuotationItemsDto> QuotationItems { get; private set; }


    public DateTime StartTime { get; private set; }


    public DateTime EndTime { get; private set; }


    public string Title { get; private set; }


    public string Address { get; private set; }


    public string BankAccount { get; private set; }


    public string OpeningBank { get; private set; }

    public string LinkMan { get; private set; }

    public string Email { get; private set; }

    public string Phone { get; private set; }

    public string Fax { get; private set; }

    public string Description { get; private set; }

    public int TotalData { get; private set; }

    public ChangeQuotationCommand(
        int id,
        IEnumerable<QuotationItemsDto> quotationItems,
        DateTime startTime,
        DateTime endTime,
        string title, string address,
        string bankAccount, string openingBank,
        string linkMan, string email, string phone, string fax,
        string description, int totalData)
    {
        Id = id;
        QuotationItems = quotationItems.ToList();
        StartTime = startTime;
        EndTime = endTime;
        Title = title;
        Address = address;
        BankAccount = bankAccount;
        OpeningBank = openingBank;
        LinkMan = linkMan;
        Email = email;
        Phone = phone;
        Fax = fax;
        Description = description;
        TotalData = totalData;
    }
}