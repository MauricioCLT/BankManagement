using Core.DTOs.RequestLoan;
using Core.Entities;
using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Infrastructure.Mapping;

public class BankMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoanRequest, RequestLoanResponse>()
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.LoanRequestId, src => src.Id)
            .Map(dest => dest.RequestDate, src => src.RequestDate)
            .Map(dest => dest.Status, src => src.Status);

        config.NewConfig<RequestLoanDTO, LoanRequest>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.Amount, src => src.AmountRequest)
            .Map(dest => dest.RequestDate, src => DateTime.UtcNow);
    }
}
