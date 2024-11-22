using Core.DTOs.RequestLoan;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class BankMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoanRequest, RequestLoanResponse>()
            .Map(dest => dest.LoanType, src => src.LoanType) 
            .Map(dest => dest.Months, src => src.TermInterestRate.Months)
            .Map(dest => dest.Status, src => "Pending");

        config.NewConfig<RequestLoan, LoanRequest>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.Status, src => "Pending");
    }
}
